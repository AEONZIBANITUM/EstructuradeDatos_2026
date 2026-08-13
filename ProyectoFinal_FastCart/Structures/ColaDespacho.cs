using ProyectoFinal_FastCart.Models;
using ProyectoFinal_FastCart.Services;
namespace ProyectoFinal_FastCart.Structures;

/// <summary>
/// Implementa manualmente una cola FIFO para administrar
/// los pedidos pendientes de despacho de FastCart.
/// </summary>
public class ColaDespacho
{
    /// <summary>
    /// Primer nodo de la cola.
    /// Representa el próximo pedido que deberá ser despachado.
    /// </summary>
    private NodoCola? Frente;

    /// <summary>
    /// Último nodo de la cola.
    /// Permite insertar nuevos pedidos en tiempo constante O(1).
    /// </summary>
    private NodoCola? Fin;

    /// <summary>
    /// Cantidad total de pedidos actualmente almacenados
    /// dentro de la cola.
    /// </summary>
    public int TotalEncolados { get; private set; }

    /// <summary>
    /// Inicializa una cola de despacho vacía.
    /// </summary>
    public ColaDespacho()
    {
        Frente = null;
        Fin = null;
        TotalEncolados = 0;
    }

    /// <summary>
    /// Indica si actualmente no existen pedidos
    /// pendientes dentro de la cola.
    /// </summary>
    /// <returns>
    /// True cuando la cola está vacía; de lo contrario, false.
    /// </returns>
    public bool EstaVacia()
    {
        return Frente is null;
    }

    /// <summary>
    /// Inserta un nuevo pedido al final de la cola
    /// respetando el comportamiento FIFO.
    ///
    /// La operación se realiza en O(1) porque la estructura
    /// conserva una referencia directa al nodo Fin.
    /// </summary>
    /// <param name="pedido">
    /// Pedido que será agregado a la cola.
    /// </param>
    public void EncolarPedido(Pedido pedido)
    {
        ArgumentNullException.ThrowIfNull(pedido);

        NodoCola nuevoNodo =
            new NodoCola(pedido);

        // Caso 1:
        // La cola se encuentra vacía.
        if (EstaVacia())
        {
            Frente = nuevoNodo;
            Fin = nuevoNodo;
        }
        else
        {
            // Caso 2:
            // Ya existen elementos.
            //
            // El nodo actual de Fin enlaza al nuevo nodo
            // y posteriormente Fin avanza hacia él.
            Fin!.Siguiente = nuevoNodo;
            Fin = nuevoNodo;
        }

        TotalEncolados++;
    }
    /// <summary>
/// Extrae el pedido ubicado en el Frente de la cola
/// y ejecuta su despacho contra el inventario real.
///
/// La operación integra:
/// Fase 2 — InventarioLista.
/// Fase 3 — AuditoriaService.
/// Fase 4 — Cola FIFO.
///
/// Complejidad temporal O(n) debido a la búsqueda
/// del SKU dentro de la lista enlazada.
/// </summary>
/// <param name="catalogo">
/// Inventario dinámico sobre el cual se realizará
/// la operación de despacho.
/// </param>
/// <param name="auditoria">
/// Servicio utilizado para registrar el resultado
/// de la operación.
/// </param>
/// <returns>
/// Pedido despachado cuando la operación es correcta.
/// Null cuando la cola está vacía, el SKU no existe
/// o existe stock insuficiente.
/// </returns>
public Pedido? DespacharPedido(
    InventarioLista catalogo,
    AuditoriaService auditoria)
{
    ArgumentNullException.ThrowIfNull(catalogo);
    ArgumentNullException.ThrowIfNull(auditoria);

    // =================================================
    // CASO 1 — COLA VACÍA
    // =================================================

    if (EstaVacia())
    {
        string mensaje =
            "[COLA] ERROR: La cola está vacía. " +
            "No hay pedidos para despachar.";

        Console.WriteLine(mensaje);

        auditoria.RegistrarEvento(
            "DESPACHO_FALLIDO",
            0,
            mensaje);

        return null;
    }

    // =================================================
    // EXTRAER PEDIDO DEL FRENTE — FIFO
    // =================================================

    Pedido pedidoDespachado =
        Frente!.Dato;

    Frente =
        Frente.Siguiente;

    // Si se retiró el único nodo existente,
    // ambos extremos deben quedar nuevamente vacíos.
    if (Frente == null)
    {
        Fin = null;
    }

    TotalEncolados--;

    // =================================================
    // CASO 2 — VALIDAR SKU
    // =================================================

    Producto producto;

    try
    {
        producto =
            catalogo.BuscarPorSKU(
                pedidoDespachado.SKU);
    }
    catch (KeyNotFoundException)
    {
        string mensaje =
            $"[ERROR] SKU {pedidoDespachado.SKU} " +
            "no encontrado en catálogo.";

        Console.WriteLine(mensaje);

        auditoria.RegistrarEvento(
            "DESPACHO_FALLIDO",
            pedidoDespachado.SKU,
            mensaje);

        return null;
    }

    // =================================================
    // CASO 3 — VALIDAR STOCK
    // =================================================

    if (producto.Stock <
        pedidoDespachado.Cantidad)
    {
        string mensaje =
            $"[ERROR] Stock insuficiente para SKU " +
            $"{pedidoDespachado.SKU}. " +
            $"Disponible: {producto.Stock}, " +
            $"Requerido: {pedidoDespachado.Cantidad}.";

        Console.WriteLine(mensaje);

        auditoria.RegistrarEvento(
            "STOCK_INSUFICIENTE",
            pedidoDespachado.SKU,
            mensaje);

        return null;
    }

    // =================================================
    // MODIFICAR STOCK REAL
    // =================================================

    bool stockActualizado =
        catalogo.DescontarStock(
            pedidoDespachado.SKU,
            pedidoDespachado.Cantidad);

    if (!stockActualizado)
    {
        string mensaje =
            $"[ERROR] No fue posible actualizar el stock " +
            $"del SKU {pedidoDespachado.SKU}.";

        Console.WriteLine(mensaje);

        auditoria.RegistrarEvento(
            "DESPACHO_FALLIDO",
            pedidoDespachado.SKU,
            mensaje);

        return null;
    }

    int stockRestante =
        producto.Stock -
        pedidoDespachado.Cantidad;

    // =================================================
    // AUDITORÍA DEL DESPACHO EXITOSO
    // =================================================

    string logExito =
        $"Pedido #{pedidoDespachado.IdPedido} despachado. " +
        $"SKU: {pedidoDespachado.SKU}, " +
        $"Cantidad: {pedidoDespachado.Cantidad}, " +
        $"Stock restante: {stockRestante}, " +
        $"Cliente: {pedidoDespachado.Cliente}.";

    auditoria.RegistrarEvento(
        "DESPACHO_EXITOSO",
        pedidoDespachado.SKU,
        logExito);

    Console.WriteLine(
        $"[COLA] {logExito}");

    return pedidoDespachado;
}
}