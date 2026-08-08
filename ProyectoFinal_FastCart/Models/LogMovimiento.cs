namespace ProyectoFinal_FastCart.Models;

/// <summary>
/// Representa un registro individual dentro del historial
/// de auditoría de FastCart.
/// </summary>
public struct LogMovimiento
{
    /// <summary>
    /// Tipo de operación registrada en el catálogo.
    /// Ejemplos: INSERT, PRICE_CHANGE, RESTOCK o DELETE.
    /// </summary>
    public string TipoOperacion;

    /// <summary>
    /// Identificador del producto afectado por la operación.
    /// Corresponde al SKU utilizado por FastCart.
    /// </summary>
    public int ProductoId;

    /// <summary>
    /// Descripción legible del movimiento realizado.
    /// Permite conservar una referencia humana del cambio.
    /// </summary>
    public string Referencia;

    /// <summary>
    /// Fecha y hora UTC en que ocurrió la operación.
    /// </summary>
    public DateTime FechaHora;
}