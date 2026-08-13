using ProyectoFinal_FastCart.Models;
using ProyectoFinal_FastCart.Structures;

namespace ProyectoFinal_FastCart.Services;

/// <summary>
/// Interfaz principal de consola que integra
/// las cuatro fases del proyecto FastCart Backend Core.
/// </summary>
public class MenuMaestro
{
    private readonly AuditoriaService _auditoria;
    private readonly InventarioLista _inventario;
    private readonly ColaDespacho _cola;
    private readonly PilaDevoluciones _pila;

    /// <summary>
    /// Inicializa el menú maestro utilizando las
    /// estructuras compartidas del sistema.
    /// </summary>
    public MenuMaestro(
        AuditoriaService auditoria,
        InventarioLista inventario,
        ColaDespacho cola,
        PilaDevoluciones pila)
    {
        _auditoria =
            auditoria ?? throw new ArgumentNullException(
                nameof(auditoria));

        _inventario =
            inventario ?? throw new ArgumentNullException(
                nameof(inventario));

        _cola =
            cola ?? throw new ArgumentNullException(
                nameof(cola));

        _pila =
            pila ?? throw new ArgumentNullException(
                nameof(pila));
    }

    /// <summary>
    /// Ejecuta el ciclo principal del sistema.
    /// </summary>
    public void Ejecutar()
    {
        bool ejecutando = true;

        while (ejecutando)
        {
            

            MostrarEncabezado();
            MostrarOpciones();

            Console.Write(
                "Seleccione una opción: ");

            string? entrada =
                Console.ReadLine();

            Console.WriteLine();

            if (!int.TryParse(
                    entrada,
                    out int opcion))
            {
                MostrarMensaje(
                    "Entrada inválida. Debe introducir un número.");

                continue;
            }

            switch (opcion)
            {
                case 0:
                    ejecutando = false;
                    break;

                case 1:
                    MostrarArquitectura();
                    break;

                case 2:
                    AgregarProducto();
                    break;

                case 3:
                    BuscarProductoPorSKU();
                    break;

                case 4:
                    EliminarProducto();
                    break;

                case 5:
                    MostrarCatalogo();
                    break;

                // ============================================
                // FASE 3
                // Pendiente de integración en el siguiente
                // bloque de desarrollo del Menú Maestro.
                // ============================================

                case 6:
                MostrarHistorialCronologico();
                    break;
                case 7:
                    MostrarHistorialInverso();
                    break;

                // ============================================
                // FASE 4
                // Pendiente de integración en los siguientes
                // bloques del Menú Maestro.
                // ============================================

                case 8:
                    EncolarNuevoPedido();
                break;
                case 9:
                    DespacharPedidoFIFO();
                break;
                case 10:
                 RegistrarDevolucion();
                break;
                case 11:
                ProcesarDevolucionLIFO();
                break;
case 12:
    MostrarEstadoLogistico();
    break;

                default:
                    MostrarMensaje(
                        "La opción seleccionada no existe.");
                    break;
            }
        }

        Console.WriteLine();

        Console.WriteLine(
            "FastCart Backend Core finalizado correctamente.");
    }

    // ============================================================
    // ENCABEZADO
    // ============================================================

    private static void MostrarEncabezado()
    {
        Console.WriteLine(
            "==============================================================");

        Console.WriteLine(
            "                 FASTCART BACKEND CORE");

        Console.WriteLine(
            "              MOTOR DE DESPACHO LOGÍSTICO v4.0");

        Console.WriteLine(
            "==============================================================");
    }

    // ============================================================
    // OPCIONES DEL MENÚ
    // ============================================================

    private static void MostrarOpciones()
    {
        Console.WriteLine();

        Console.WriteLine(
            "FASE 1 — FUNDAMENTOS DEL CATÁLOGO");

        Console.WriteLine(
            "[1] Ver información de arquitectura");

        Console.WriteLine();

        Console.WriteLine(
            "FASE 2 — CATÁLOGO DINÁMICO");

        Console.WriteLine(
            "[2] Agregar producto al catálogo");

        Console.WriteLine(
            "[3] Buscar producto por SKU");

        Console.WriteLine(
            "[4] Eliminar producto del catálogo");

        Console.WriteLine(
            "[5] Mostrar catálogo completo");

        Console.WriteLine();

        Console.WriteLine(
            "FASE 3 — AUDITORÍA");

        Console.WriteLine(
            "[6] Ver historial cronológico");

        Console.WriteLine(
            "[7] Ver historial inverso");

        Console.WriteLine();

        Console.WriteLine(
            "FASE 4 — MOTOR LOGÍSTICO");

        Console.WriteLine(
            "[8] Encolar nuevo pedido");

        Console.WriteLine(
            "[9] Despachar pedido (FIFO)");

        Console.WriteLine(
            "[10] Registrar devolución (LIFO)");

        Console.WriteLine(
            "[11] Procesar devolución del Top");

        Console.WriteLine(
            "[12] Ver estado de cola y pila");

        Console.WriteLine();

        Console.WriteLine(
            "[0] Salir");

        Console.WriteLine();

        Console.WriteLine(
            "--------------------------------------------------------------");
    }

    // ============================================================
    // OPCIÓN 1
    // INFORMACIÓN DE ARQUITECTURA
    // ============================================================

    private static void MostrarArquitectura()
    {
        Console.WriteLine(
            "==============================================================");

        Console.WriteLine(
            "                ARQUITECTURA FASTCART");

        Console.WriteLine(
            "==============================================================");

        Console.WriteLine();

        Console.WriteLine(
            "FASE 1 - Catálogo base y ordenamiento");

        Console.WriteLine(
            "  Producto / Proveedor / ShellSort");

        Console.WriteLine();

        Console.WriteLine(
            "FASE 2 - Catálogo dinámico");

        Console.WriteLine(
            "  Lista simplemente enlazada");

        Console.WriteLine(
            "  NodoProducto -> Siguiente");

        Console.WriteLine();

        Console.WriteLine(
            "FASE 3 - Auditoría bidireccional");

        Console.WriteLine(
            "  NodoAuditoria");

        Console.WriteLine(
            "  Anterior <-> Siguiente");

        Console.WriteLine();

        Console.WriteLine(
            "FASE 4 - Motor logístico");

        Console.WriteLine(
            "  ColaDespacho -> FIFO");

        Console.WriteLine(
            "  PilaDevoluciones -> LIFO");

        Console.WriteLine();

        Console.WriteLine(
            "Integración:");

        Console.WriteLine(
            "  InventarioLista");

        Console.WriteLine(
            "       |");

        Console.WriteLine(
            "       +--> AuditoriaService");

        Console.WriteLine(
            "       |");

        Console.WriteLine(
            "       +--> ColaDespacho");

        Console.WriteLine(
            "       |");

        Console.WriteLine(
            "       +--> PilaDevoluciones");

        MostrarMensaje(
            "Arquitectura cargada correctamente.");
    }

    // ============================================================
    // OPCIÓN 2
    // AGREGAR PRODUCTO
    // ============================================================

    private void AgregarProducto()
    {
        Console.WriteLine(
            "==============================================================");

        Console.WriteLine(
            "                  AGREGAR PRODUCTO");

        Console.WriteLine(
            "==============================================================");

        Console.WriteLine();

        int sku =
            LeerEnteroPositivo(
                "SKU: ");

        // --------------------------------------------------------
        // Validar que el SKU no exista previamente.
        // --------------------------------------------------------

        try
        {
            _inventario.BuscarPorSKU(
                sku);

            MostrarMensaje(
                $"ERROR: Ya existe un producto con SKU {sku}.");

            return;
        }
        catch (KeyNotFoundException)
        {
            // El SKU no existe.
            // Por lo tanto puede utilizarse.
        }

        string nombre =
            LeerTexto(
                "Nombre del producto: ");

        double precio =
            LeerDoublePositivo(
                "Precio: $");

        int stock =
            LeerEnteroNoNegativo(
                "Stock inicial: ");

        int idProveedor =
            LeerEnteroPositivo(
                "ID del proveedor: ");

        string nombreProveedor =
            LeerTexto(
                "Nombre corporativo del proveedor: ");

        // --------------------------------------------------------
        // Crear proveedor.
        // --------------------------------------------------------

        Proveedor proveedor =
            new Proveedor
            {
                IdProveedor =
                    idProveedor,

                NombreCorporativo =
                    nombreProveedor
            };

        // --------------------------------------------------------
        // Crear producto.
        // --------------------------------------------------------

        Producto producto =
            new Producto
            {
                SKU =
                    sku,

                Nombre =
                    nombre,

                Precio =
                    precio,

                Stock =
                    stock,

                DatosProveedor =
                    proveedor
            };

        // --------------------------------------------------------
        // Insertar manteniendo el orden por precio.
        // La operación genera auditoría automática.
        // --------------------------------------------------------

        _inventario.InsertarOrdenado(
            producto);

        MostrarMensaje(
            $"Producto '{nombre}' agregado correctamente. " +
            $"SKU: {sku}.");
    }

    // ============================================================
    // OPCIÓN 3
    // BUSCAR PRODUCTO POR SKU
    // ============================================================

    private void BuscarProductoPorSKU()
    {
        Console.WriteLine(
            "==============================================================");

        Console.WriteLine(
            "                  BÚSQUEDA POR SKU");

        Console.WriteLine(
            "==============================================================");

        Console.WriteLine();

        int sku =
            LeerEnteroPositivo(
                "SKU a buscar: ");

        try
        {
            Producto producto =
                _inventario.BuscarPorSKU(
                    sku);

            Console.WriteLine();

            Console.WriteLine(
                "Producto encontrado:");

            Console.WriteLine();

            Console.WriteLine(
                $"SKU       : {producto.SKU}");

            Console.WriteLine(
                $"Producto  : {producto.Nombre}");

            Console.WriteLine(
                $"Precio    : ${producto.Precio:F2}");

            Console.WriteLine(
                $"Stock     : {producto.Stock}");

            Console.WriteLine(
                $"Proveedor : " +
                $"{producto.DatosProveedor.NombreCorporativo}");

            MostrarMensaje(
                "Búsqueda completada.");
        }
        catch (KeyNotFoundException ex)
        {
            MostrarMensaje(
                ex.Message);
        }
    }

    // ============================================================
    // OPCIÓN 4
    // ELIMINAR PRODUCTO
    // ============================================================

    private void EliminarProducto()
    {
        Console.WriteLine(
            "==============================================================");

        Console.WriteLine(
            "                 ELIMINAR PRODUCTO");

        Console.WriteLine(
            "==============================================================");

        Console.WriteLine();

        int sku =
            LeerEnteroPositivo(
                "SKU a eliminar: ");

        bool eliminado =
            _inventario.EliminarPorSKU(
                sku);

        if (eliminado)
        {
            MostrarMensaje(
                $"Producto con SKU {sku} eliminado correctamente.");
        }
        else
        {
            MostrarMensaje(
                $"No existe un producto con SKU {sku}.");
        }
    }

    // ============================================================
    // OPCIÓN 5
    // MOSTRAR CATÁLOGO
    // ============================================================

    private void MostrarCatalogo()
    {
        Console.WriteLine(
            "==============================================================");

        Console.WriteLine(
            "                  CATÁLOGO FASTCART");

        Console.WriteLine(
            "==============================================================");

        Console.WriteLine();

        Console.WriteLine(
            $"Productos registrados: {_inventario.Contar()}");

        Console.WriteLine();

        _inventario.MostrarTodos();

        MostrarMensaje(
            "Consulta de catálogo completada.");
    }
// ============================================================
// OPCIÓN 6
// HISTORIAL CRONOLÓGICO
// ============================================================

private void MostrarHistorialCronologico()
{
    Console.WriteLine(
        "==============================================================");

    Console.WriteLine(
        "              HISTORIAL DE AUDITORÍA");

    Console.WriteLine(
        "               ORDEN CRONOLÓGICO");

    Console.WriteLine(
        "==============================================================");

    Console.WriteLine();

    _auditoria.ImprimirHistorial();

    Console.WriteLine();

    Console.WriteLine(
        $"Registros almacenados: {_auditoria.TotalRegistros}");

    MostrarMensaje(
        "Consulta cronológica completada.");
}


// ============================================================
// OPCIÓN 7
// HISTORIAL INVERSO
// ============================================================

private void MostrarHistorialInverso()
{
    Console.WriteLine(
        "==============================================================");

    Console.WriteLine(
        "              HISTORIAL DE AUDITORÍA");

    Console.WriteLine(
        "                 ORDEN INVERSO");

    Console.WriteLine(
        "==============================================================");

    Console.WriteLine();

    _auditoria.ImprimirHistorialInverso();

    Console.WriteLine();

    Console.WriteLine(
        $"Registros almacenados: {_auditoria.TotalRegistros}");

    MostrarMensaje(
        "Consulta inversa completada.");
}
// ============================================================
// OPCIÓN 8
// ENCOLAR NUEVO PEDIDO
// ============================================================
// ============================================================
// OPCIÓN 9
// DESPACHAR PEDIDO FIFO
// ============================================================

private void DespacharPedidoFIFO()
{
    Console.WriteLine(
        "==============================================================");

    Console.WriteLine(
        "                  DESPACHAR PEDIDO");

    Console.WriteLine(
        "                     MODO FIFO");

    Console.WriteLine(
        "==============================================================");

    Console.WriteLine();

    Console.WriteLine(
        $"Pedidos pendientes antes del despacho: {_cola.TotalEncolados}");

    Console.WriteLine();

    Pedido? pedidoDespachado =
        _cola.DespacharPedido(
            _inventario,
            _auditoria);

    if (pedidoDespachado is null)
    {
        MostrarMensaje(
            "El pedido no pudo ser despachado. " +
            "Revise la auditoría para conocer el motivo.");

        return;
    }

    Console.WriteLine();

    Console.WriteLine(
        "Pedido procesado correctamente:");

    Console.WriteLine(
        $"ID pedido : {pedidoDespachado.IdPedido}");

    Console.WriteLine(
        $"SKU       : {pedidoDespachado.SKU}");

    Console.WriteLine(
        $"Cantidad  : {pedidoDespachado.Cantidad}");

    Console.WriteLine(
        $"Cliente   : {pedidoDespachado.Cliente}");

    try
    {
        Producto productoActualizado =
            _inventario.BuscarPorSKU(
                pedidoDespachado.SKU);

        Console.WriteLine(
            $"Stock restante: {productoActualizado.Stock}");
    }
    catch (KeyNotFoundException)
    {
        Console.WriteLine(
            "No fue posible consultar el stock posterior al despacho.");
    }

    Console.WriteLine();

    Console.WriteLine(
        $"Pedidos pendientes: {_cola.TotalEncolados}");

    MostrarMensaje(
        "Despacho FIFO completado.");
}
private void EncolarNuevoPedido()
{
    Console.WriteLine(
        "==============================================================");

    Console.WriteLine(
        "                 ENCOLAR NUEVO PEDIDO");

    Console.WriteLine(
        "                     COLA FIFO");

    Console.WriteLine(
        "==============================================================");

    Console.WriteLine();

    int idPedido =
        LeerEnteroPositivo(
            "ID del pedido: ");

    int sku =
        LeerEnteroPositivo(
            "SKU solicitado: ");

    int cantidad =
        LeerEnteroPositivo(
            "Cantidad solicitada: ");

    string cliente =
        LeerTexto(
            "Cliente: ");

    Pedido pedido =
        new Pedido(
            idPedido,
            sku,
            cantidad,
            cliente);

    _cola.EncolarPedido(
        pedido);

    MostrarMensaje(
        $"Pedido #{idPedido} encolado correctamente. " +
        $"SKU: {sku}, Cantidad: {cantidad}. " +
        $"Pedidos pendientes: {_cola.TotalEncolados}.");
}
// ============================================================
// OPCIÓN 10
// REGISTRAR DEVOLUCIÓN LIFO
// ============================================================

private void RegistrarDevolucion()
{
    Console.WriteLine(
        "==============================================================");

    Console.WriteLine(
        "                REGISTRAR DEVOLUCIÓN");

    Console.WriteLine(
        "                     PILA LIFO");

    Console.WriteLine(
        "==============================================================");

    Console.WriteLine();

    int idDevolucion =
        LeerEnteroPositivo(
            "ID de devolución: ");

    int sku =
        LeerEnteroPositivo(
            "SKU devuelto: ");

    int cantidad =
        LeerEnteroPositivo(
            "Cantidad devuelta: ");

    string motivo =
        LeerTexto(
            "Motivo de devolución: ");

    Devolucion devolucion =
        new Devolucion(
            idDevolucion,
            sku,
            cantidad,
            motivo);

    _pila.PushDevolucion(
        devolucion);

    MostrarMensaje(
        $"Devolución #{idDevolucion} registrada correctamente. " +
        $"SKU: {sku}, Cantidad: {cantidad}. " +
        $"Devoluciones pendientes: {_pila.TotalDevoluciones}.");
}


// ============================================================
// OPCIÓN 11
// PROCESAR DEVOLUCIÓN LIFO
// ============================================================

private void ProcesarDevolucionLIFO()
{
    Console.WriteLine(
        "==============================================================");

    Console.WriteLine(
        "                PROCESAR DEVOLUCIÓN");

    Console.WriteLine(
        "                     MODO LIFO");

    Console.WriteLine(
        "==============================================================");

    Console.WriteLine();

    Console.WriteLine(
        $"Devoluciones pendientes antes del proceso: " +
        $"{_pila.TotalDevoluciones}");

    Console.WriteLine();

    Devolucion? devolucionProcesada =
        _pila.PopDevolucion(
            _inventario,
            _auditoria);

    if (devolucionProcesada is null)
    {
        MostrarMensaje(
            "La devolución no pudo procesarse. " +
            "Revise la auditoría para conocer el motivo.");

        return;
    }

    Console.WriteLine();

    Console.WriteLine(
        "Devolución procesada correctamente:");

    Console.WriteLine(
        $"ID devolución : {devolucionProcesada.IdDevolucion}");

    Console.WriteLine(
        $"SKU           : {devolucionProcesada.SKU}");

    Console.WriteLine(
        $"Cantidad      : {devolucionProcesada.Cantidad}");

    Console.WriteLine(
        $"Motivo        : {devolucionProcesada.Motivo}");

    try
    {
        Producto productoActualizado =
            _inventario.BuscarPorSKU(
                devolucionProcesada.SKU);

        Console.WriteLine(
            $"Stock actualizado: {productoActualizado.Stock}");
    }
    catch (KeyNotFoundException)
    {
        Console.WriteLine(
            "No fue posible consultar el stock posterior.");
    }

    Console.WriteLine();

    Console.WriteLine(
        $"Devoluciones pendientes: {_pila.TotalDevoluciones}");

    MostrarMensaje(
        "Procesamiento LIFO completado.");
}
// ============================================================
// OPCIÓN 12
// ESTADO GENERAL DE COLA Y PILA
// ============================================================

private void MostrarEstadoLogistico()
{
    Console.WriteLine(
        "==============================================================");

    Console.WriteLine(
        "                ESTADO LOGÍSTICO FASTCART");

    Console.WriteLine(
        "==============================================================");

    Console.WriteLine();

    Console.WriteLine(
        "COLA DE DESPACHO — FIFO");

    Console.WriteLine(
        "--------------------------------------------------------------");

    Console.WriteLine(
        $"Pedidos pendientes : {_cola.TotalEncolados}");

    Console.WriteLine(
        $"Estado             : " +
        $"{(_cola.EstaVacia() ? "VACÍA" : "CON PEDIDOS")}");

    Console.WriteLine();

    Console.WriteLine(
        "PILA DE DEVOLUCIONES — LIFO");

    Console.WriteLine(
        "--------------------------------------------------------------");

    Console.WriteLine(
        $"Devoluciones pendientes : {_pila.TotalDevoluciones}");

    Console.WriteLine(
        $"Estado                  : " +
        $"{(_pila.EstaVacia() ? "VACÍA" : "CON DEVOLUCIONES")}");

    Console.WriteLine();

    Console.WriteLine(
        "AUDITORÍA");

    Console.WriteLine(
        "--------------------------------------------------------------");

    Console.WriteLine(
        $"Eventos registrados : {_auditoria.TotalRegistros}");

    Console.WriteLine();

    Console.WriteLine(
        "INVENTARIO");

    Console.WriteLine(
        "--------------------------------------------------------------");

    Console.WriteLine(
        $"Productos registrados : {_inventario.Contar()}");

    MostrarMensaje(
        "Estado logístico consultado correctamente.");
}
    // ============================================================
    // MÉTODOS AUXILIARES DE ENTRADA
    // ============================================================

    /// <summary>
    /// Lee un número entero estrictamente mayor que cero.
    /// </summary>
    private static int LeerEnteroPositivo(
        string mensaje)
    {
        while (true)
        {
            Console.Write(
                mensaje);

            string? entrada =
                Console.ReadLine();

            if (int.TryParse(
                    entrada,
                    out int valor) &&
                valor > 0)
            {
                return valor;
            }

            Console.WriteLine(
                "Valor inválido. Introduzca un entero mayor que cero.");
        }
    }

    /// <summary>
    /// Lee un número entero igual o mayor que cero.
    /// </summary>
    private static int LeerEnteroNoNegativo(
        string mensaje)
    {
        while (true)
        {
            Console.Write(
                mensaje);

            string? entrada =
                Console.ReadLine();

            if (int.TryParse(
                    entrada,
                    out int valor) &&
                valor >= 0)
            {
                return valor;
            }

            Console.WriteLine(
                "Valor inválido. Introduzca un entero igual o mayor que cero.");
        }
    }

    /// <summary>
    /// Lee un número decimal estrictamente mayor que cero.
    /// </summary>
    private static double LeerDoublePositivo(
        string mensaje)
    {
        while (true)
        {
            Console.Write(
                mensaje);

            string? entrada =
                Console.ReadLine();

            if (double.TryParse(
                    entrada,
                    out double valor) &&
                valor > 0)
            {
                return valor;
            }

            Console.WriteLine(
                "Valor inválido. Introduzca un número mayor que cero.");
        }
    }

    /// <summary>
    /// Lee texto obligatorio y elimina espacios
    /// innecesarios en los extremos.
    /// </summary>
    private static string LeerTexto(
        string mensaje)
    {
        while (true)
        {
            Console.Write(
                mensaje);

            string? valor =
                Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(
                    valor))
            {
                return valor.Trim();
            }

            Console.WriteLine(
                "El texto no puede estar vacío.");
        }
    }

    // ============================================================
    // MENSAJE Y PAUSA
    // ============================================================

    private static void MostrarMensaje(
        string mensaje)
    {
        Console.WriteLine();

        Console.WriteLine(
            mensaje);

        Console.WriteLine();

        Console.Write(
            "Presione ENTER para continuar...");

        Console.ReadLine();
    }
}