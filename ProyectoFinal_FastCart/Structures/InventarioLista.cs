using ProyectoFinal_FastCart.Models;
using ProyectoFinal_FastCart.Services;

namespace ProyectoFinal_FastCart.Structures;

/// <summary>
/// Administra el catálogo dinámico de FastCart mediante
/// una lista simplemente enlazada de productos.
/// A partir de la Fase 3 incorpora auditoría automática
/// de las operaciones que modifican el inventario.
/// </summary>
public class InventarioLista
{
    private NodoProducto? _cabeza;
    private readonly AuditoriaService _auditoria;

    /// <summary>
    /// Inicializa una lista de inventario utilizando un
    /// servicio interno de auditoría.
    /// Este constructor mantiene compatibilidad con las
    /// pruebas desarrolladas durante la Fase 2.
    /// </summary>
    public InventarioLista()
        : this(new AuditoriaService())
    {
    }

    /// <summary>
    /// Inicializa una lista de inventario utilizando el
    /// servicio de auditoría proporcionado mediante
    /// inyección de dependencia.
    /// </summary>
    /// <param name="auditoria">
    /// Servicio encargado de registrar los movimientos
    /// realizados sobre el catálogo.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce cuando el servicio recibido es null.
    /// </exception>
    public InventarioLista(AuditoriaService auditoria)
    {
        _auditoria = auditoria
            ?? throw new ArgumentNullException(
                nameof(auditoria),
                "AuditoriaService es requerido.");

        _cabeza = null;
    }

    /// <summary>
    /// Indica si la lista se encuentra vacía.
    /// </summary>
    /// <returns>
    /// True cuando no existen productos almacenados.
    /// </returns>
    public bool EstaVacia()
    {
        return _cabeza == null;
    }

    /// <summary>
    /// Inserta un producto al inicio de la lista.
    /// Complejidad temporal O(1).
    /// </summary>
    /// <param name="producto">
    /// Producto que será agregado al inventario.
    /// </param>
    public void InsertarInicio(Producto producto)
    {
        NodoProducto nuevoNodo =
            new NodoProducto(producto);

        nuevoNodo.Siguiente = _cabeza;
        _cabeza = nuevoNodo;

        // La auditoría se registra únicamente
        // después de completar la operación.
        _auditoria.RegistrarEvento(
            "INSERT",
            producto.SKU,
            $"Producto '{producto.Nombre}' agregado al inicio del catálogo.");
    }

    /// <summary>
    /// Inserta un producto manteniendo la lista ordenada
    /// por precio de forma ascendente.
    /// Complejidad temporal O(n).
    /// </summary>
    /// <param name="producto">
    /// Producto que será agregado al inventario.
    /// </param>
    public void InsertarOrdenado(Producto producto)
    {
        NodoProducto nuevoNodo =
            new NodoProducto(producto);

        // Caso 1:
        // Lista vacía o producto con precio menor
        // que el producto ubicado en la cabeza.
        if (_cabeza == null ||
            producto.Precio < _cabeza.Data.Precio)
        {
            nuevoNodo.Siguiente = _cabeza;
            _cabeza = nuevoNodo;

            _auditoria.RegistrarEvento(
                "INSERT",
                producto.SKU,
                $"Producto '{producto.Nombre}' agregado al catálogo.");

            return;
        }

        // Caso 2:
        // Buscar posición correcta dentro de la lista.
        NodoProducto actual = _cabeza;

        while (actual.Siguiente != null &&
               actual.Siguiente.Data.Precio <= producto.Precio)
        {
            actual = actual.Siguiente;
        }

        // Insertar el nodo entre "actual"
        // y el nodo que anteriormente le seguía.
        nuevoNodo.Siguiente = actual.Siguiente;
        actual.Siguiente = nuevoNodo;

        _auditoria.RegistrarEvento(
            "INSERT",
            producto.SKU,
            $"Producto '{producto.Nombre}' agregado al catálogo.");
    }

    /// <summary>
    /// Cuenta la cantidad total de productos almacenados.
    /// Complejidad temporal O(n).
    /// </summary>
    /// <returns>
    /// Número total de nodos existentes.
    /// </returns>
    public int Contar()
    {
        int contador = 0;
        NodoProducto? actual = _cabeza;

        while (actual != null)
        {
            contador++;
            actual = actual.Siguiente;
        }

        return contador;
    }

    /// <summary>
    /// Busca un producto mediante su SKU recorriendo
    /// secuencialmente la lista.
    /// Complejidad temporal O(n).
    /// </summary>
    /// <param name="sku">
    /// Identificador único del producto buscado.
    /// </param>
    /// <returns>
    /// Producto correspondiente al SKU solicitado.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Se produce cuando el SKU no existe.
    /// </exception>
    public Producto BuscarPorSKU(int sku)
    {
        NodoProducto? actual = _cabeza;

        while (actual != null)
        {
            if (actual.Data.SKU == sku)
            {
                return actual.Data;
            }

            actual = actual.Siguiente;
        }

        throw new KeyNotFoundException(
            $"SKU {sku} no encontrado en el inventario.");
    }

    /// <summary>
    /// Actualiza el precio de un producto identificado
    /// mediante su SKU y registra el cambio en auditoría.
    /// </summary>
    /// <param name="sku">
    /// SKU del producto que será actualizado.
    /// </param>
    /// <param name="nuevoPrecio">
    /// Nuevo precio asignado al producto.
    /// </param>
    /// <returns>
    /// True cuando el producto fue localizado y actualizado.
    /// False cuando el SKU no existe.
    /// </returns>
    public bool ActualizarPrecio(
        int sku,
        double nuevoPrecio)
    {
        NodoProducto? actual = _cabeza;

        while (actual != null)
        {
            if (actual.Data.SKU == sku)
            {
                // Producto es tratado como tipo por valor.
                // Se obtiene una copia, se modifica y luego
                // se vuelve a almacenar dentro del nodo.
                Producto productoActualizado =
                    actual.Data;

                double precioAnterior =
                    productoActualizado.Precio;

                productoActualizado.Precio =
                    nuevoPrecio;

                actual.Data =
                    productoActualizado;

                // Auditar únicamente después
                // de realizar la modificación.
                _auditoria.RegistrarEvento(
                    "UPDATE",
                    sku,
                    $"Precio actualizado de " +
                    $"${precioAnterior:F2} a " +
                    $"${nuevoPrecio:F2} para " +
                    $"'{productoActualizado.Nombre}'.");

                return true;
            }

            actual = actual.Siguiente;
        }

        return false;
    }

    /// <summary>
    /// Elimina de la lista el producto correspondiente
    /// al SKU indicado y registra la operación exitosa
    /// dentro de la bitácora.
    /// Complejidad temporal O(n).
    /// </summary>
    /// <param name="sku">
    /// Identificador del producto que será eliminado.
    /// </param>
    /// <returns>
    /// True si el producto fue eliminado.
    /// False si el SKU no existe.
    /// </returns>
    public bool EliminarPorSKU(int sku)
    {
        // Caso 1:
        // Lista vacía.
        if (_cabeza == null)
        {
            return false;
        }

        // Caso 2:
        // El producto se encuentra en la cabeza.
        if (_cabeza.Data.SKU == sku)
        {
            string nombreEliminado =
                _cabeza.Data.Nombre;

            _cabeza =
                _cabeza.Siguiente;

            _auditoria.RegistrarEvento(
                "DELETE",
                sku,
                $"Producto '{nombreEliminado}' eliminado del catálogo.");

            return true;
        }

        // Caso 3:
        // Buscar el nodo anterior
        // al elemento objetivo.
        NodoProducto anterior =
            _cabeza;

        while (anterior.Siguiente != null)
        {
            if (anterior.Siguiente.Data.SKU == sku)
            {
                string nombreEliminado =
                    anterior.Siguiente.Data.Nombre;

                anterior.Siguiente =
                    anterior.Siguiente.Siguiente;

                _auditoria.RegistrarEvento(
                    "DELETE",
                    sku,
                    $"Producto '{nombreEliminado}' eliminado del catálogo.");

                return true;
            }

            anterior =
                anterior.Siguiente;
        }

        // SKU inexistente:
        // no existe modificación y por lo tanto
        // tampoco debe generarse un evento.
        return false;
    }

    /// <summary>
    /// Muestra todos los productos almacenados
    /// recorriendo la lista desde la cabeza hasta
    /// el último nodo.
    /// </summary>
    public void MostrarTodos()
    {
        NodoProducto? actual = _cabeza;

        if (actual == null)
        {
            Console.WriteLine(
                "El inventario está vacío.");

            return;
        }

        while (actual != null)
        {
            Producto producto =
                actual.Data;

            Console.WriteLine(
                $"SKU: {producto.SKU} | " +
                $"Producto: {producto.Nombre} | " +
                $"Precio: ${producto.Precio:F2} | " +
                $"Stock: {producto.Stock} | " +
                $"Proveedor: " +
                $"{producto.DatosProveedor.NombreCorporativo}");

            actual =
                actual.Siguiente;
        }
    }
}