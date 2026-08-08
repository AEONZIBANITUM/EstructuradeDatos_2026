using ProyectoFinal_FastCart.Models;

namespace ProyectoFinal_FastCart.Structures;

/// <summary>
/// Administra el catálogo dinámico de FastCart mediante una
/// lista simplemente enlazada de productos.
/// </summary>
public class InventarioLista
{
    private NodoProducto? _cabeza;

    /// <summary>
    /// Inicializa una lista de inventario vacía.
    /// </summary>
    public InventarioLista()
    {
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
        NodoProducto nuevoNodo = new NodoProducto(producto);

        nuevoNodo.Siguiente = _cabeza;
        _cabeza = nuevoNodo;
    }

    /// <summary>
    /// Cuenta la cantidad total de productos almacenados.
    /// Complejidad temporal O(n).
    /// </summary>
    /// <returns>
    /// Número de nodos existentes en la lista.
    /// </returns>
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
    NodoProducto nuevoNodo = new NodoProducto(producto);

    // Caso 1: lista vacía o nuevo producto con precio
    // menor que el producto almacenado en la cabeza.
    if (_cabeza == null ||
        producto.Precio < _cabeza.Data.Precio)
    {
        nuevoNodo.Siguiente = _cabeza;
        _cabeza = nuevoNodo;
        return;
    }

    // Caso 2: buscar la posición correcta dentro de la lista.
    NodoProducto actual = _cabeza;

    while (actual.Siguiente != null &&
           actual.Siguiente.Data.Precio <= producto.Precio)
    {
        actual = actual.Siguiente;
    }

    // Insertar el nodo entre "actual" y su antiguo sucesor.
    nuevoNodo.Siguiente = actual.Siguiente;
    actual.Siguiente = nuevoNodo;
}
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
    /// Muestra todos los productos almacenados recorriendo
    /// la lista desde la cabeza hasta el último nodo.
    /// </summary>
    /// <summary>
/// Busca un producto mediante su SKU recorriendo
/// secuencialmente la lista desde la cabeza.
/// Complejidad temporal O(n).
/// </summary>
/// <param name="sku">
/// Identificador único del producto buscado.
/// </param>
/// <returns>
/// Producto cuyo SKU coincide con el valor solicitado.
/// </returns>
/// <exception cref="KeyNotFoundException">
/// Se produce cuando el SKU no existe dentro del inventario.
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
/// Elimina de la lista el producto correspondiente al SKU indicado.
/// La operación conserva la continuidad de la cadena mediante
/// el reenlace de referencias.
/// Complejidad temporal O(n).
/// </summary>
/// <param name="sku">
/// Identificador único del producto que será eliminado.
/// </param>
/// <returns>
/// True si el producto fue localizado y eliminado.
/// False cuando el SKU no existe en el inventario.
/// </returns>
public bool EliminarPorSKU(int sku)
{
    // Caso 1: lista vacía.
    if (_cabeza == null)
    {
        return false;
    }

    // Caso 2: el producto se encuentra en la cabeza.
    if (_cabeza.Data.SKU == sku)
    {
        _cabeza = _cabeza.Siguiente;
        return true;
    }

    // Caso 3: buscar el nodo anterior al elemento objetivo.
    NodoProducto anterior = _cabeza;

    while (anterior.Siguiente != null)
    {
        if (anterior.Siguiente.Data.SKU == sku)
        {
            anterior.Siguiente =
                anterior.Siguiente.Siguiente;

            return true;
        }

        anterior = anterior.Siguiente;
    }

    // El SKU no existe.
    return false;
}
    public void MostrarTodos()
    {
        NodoProducto? actual = _cabeza;

        if (actual == null)
        {
            Console.WriteLine("El inventario está vacío.");
            return;
        }

        while (actual != null)
        {
            Producto producto = actual.Data;

            Console.WriteLine(
                $"SKU: {producto.SKU} | " +
                $"Producto: {producto.Nombre} | " +
                $"Precio: ${producto.Precio:F2} | " +
                $"Stock: {producto.Stock} | " +
                $"Proveedor: {producto.DatosProveedor.NombreCorporativo}");

            actual = actual.Siguiente;
        }
    }
}