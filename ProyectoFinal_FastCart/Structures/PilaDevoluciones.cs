using ProyectoFinal_FastCart.Models;
using ProyectoFinal_FastCart.Services;
namespace ProyectoFinal_FastCart.Structures;

/// <summary>
/// Implementa manualmente una pila LIFO para administrar
/// las devoluciones pendientes de procesamiento en FastCart.
/// </summary>
public class PilaDevoluciones
{
    /// <summary>
    /// Nodo ubicado en la parte superior de la pila.
    ///
    /// Representa la última devolución registrada
    /// y, por comportamiento LIFO, será la primera
    /// en procesarse.
    /// </summary>
    private NodoPila? Top;

    /// <summary>
    /// Cantidad total de devoluciones almacenadas
    /// actualmente dentro de la pila.
    /// </summary>
    public int TotalDevoluciones { get; private set; }

    /// <summary>
    /// Inicializa una pila de devoluciones vacía.
    /// </summary>
    public PilaDevoluciones()
    {
        Top = null;
        TotalDevoluciones = 0;
    }

    /// <summary>
    /// Indica si la pila no contiene devoluciones.
    /// </summary>
    /// <returns>
    /// True cuando Top es null; de lo contrario, false.
    /// </returns>
    public bool EstaVacia()
    {
        return Top is null;
    }

    /// <summary>
    /// Inserta una devolución en la parte superior
    /// de la pila respetando el comportamiento LIFO.
    ///
    /// La operación se realiza en tiempo constante O(1),
    /// debido a que no requiere recorrer la estructura.
    /// </summary>
    /// <param name="devolucion">
    /// Devolución que será almacenada.
    /// </param>
    public void PushDevolucion(
        Devolucion devolucion)
    {
        ArgumentNullException.ThrowIfNull(
            devolucion);

        NodoPila nuevoNodo =
            new NodoPila(devolucion);

        nuevoNodo.Siguiente =
            Top;

        Top =
            nuevoNodo;

        TotalDevoluciones++;
 
    }
/// <summary>
/// Extrae la devolución ubicada en Top respetando
/// el comportamiento LIFO y reintegra sus unidades
/// al inventario real.
///
/// Integra:
/// Fase 2 — InventarioLista.
/// Fase 3 — AuditoriaService.
/// Fase 4 — Pila LIFO.
/// </summary>
/// <param name="catalogo">
/// Inventario donde se reintegrará el producto.
/// </param>
/// <param name="auditoria">
/// Servicio utilizado para registrar el resultado.
/// </param>
/// <returns>
/// Devolución procesada cuando la operación es correcta.
/// Null si la pila está vacía o el SKU no existe.
/// </returns>
public Devolucion? PopDevolucion(
    InventarioLista catalogo,
    AuditoriaService auditoria)
{
    ArgumentNullException.ThrowIfNull(
        catalogo);

    ArgumentNullException.ThrowIfNull(
        auditoria);

    // =================================================
    // CASO 1 — PILA VACÍA
    // =================================================

    if (EstaVacia())
    {
        string mensaje =
            "[PILA] ERROR: La pila está vacía. " +
            "No hay devoluciones pendientes.";

        Console.WriteLine(mensaje);

        auditoria.RegistrarEvento(
            "DEVOLUCION_FALLIDA",
            0,
            mensaje);

        return null;
    }

    // =================================================
    // EXTRAER TOP — LIFO
    // =================================================

    Devolucion devolucionProcesada =
        Top!.Dato;

    Top =
        Top.Siguiente;

    TotalDevoluciones--;

    // =================================================
    // VALIDAR SKU
    // =================================================

    Producto producto;

    try
    {
        producto =
            catalogo.BuscarPorSKU(
                devolucionProcesada.SKU);
    }
    catch (KeyNotFoundException)
    {
        string mensaje =
            $"[ERROR] SKU {devolucionProcesada.SKU} " +
            "no encontrado para devolución.";

        Console.WriteLine(mensaje);

        auditoria.RegistrarEvento(
            "DEVOLUCION_FALLIDA",
            devolucionProcesada.SKU,
            mensaje);

        return null;
    }

    // =================================================
    // REINTEGRAR STOCK REAL
    // =================================================

    bool stockActualizado =
        catalogo.ReintegrarStock(
            devolucionProcesada.SKU,
            devolucionProcesada.Cantidad);

    if (!stockActualizado)
    {
        string mensaje =
            $"[ERROR] No fue posible reintegrar stock " +
            $"para SKU {devolucionProcesada.SKU}.";

        Console.WriteLine(mensaje);

        auditoria.RegistrarEvento(
            "DEVOLUCION_FALLIDA",
            devolucionProcesada.SKU,
            mensaje);

        return null;
    }

    int stockFinal =
        producto.Stock +
        devolucionProcesada.Cantidad;

    // =================================================
    // AUDITORÍA EXITOSA
    // =================================================

    string logExito =
        $"Devolución #{devolucionProcesada.IdDevolucion} procesada. " +
        $"SKU: {devolucionProcesada.SKU}, " +
        $"Cantidad reintegrada: {devolucionProcesada.Cantidad}, " +
        $"Stock final: {stockFinal}, " +
        $"Motivo: {devolucionProcesada.Motivo}.";

    auditoria.RegistrarEvento(
        "DEVOLUCION_EXITOSA",
        devolucionProcesada.SKU,
        logExito);

    Console.WriteLine(
        $"[PILA] {logExito}");

    return devolucionProcesada;
}
}
