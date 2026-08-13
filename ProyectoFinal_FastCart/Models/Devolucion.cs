namespace ProyectoFinal_FastCart.Models;

/// <summary>
/// Representa una devolución registrada dentro
/// del flujo logístico de FastCart.
/// </summary>
public class Devolucion
{
    /// <summary>
    /// Identificador único de la devolución.
    /// </summary>
    public int IdDevolucion { get; set; }

    /// <summary>
    /// SKU del producto que será reintegrado
    /// al inventario.
    /// </summary>
    public int SKU { get; set; }

    /// <summary>
    /// Cantidad de unidades devueltas.
    /// </summary>
    public int Cantidad { get; set; }

    /// <summary>
    /// Motivo asociado a la devolución.
    /// </summary>
    public string Motivo { get; set; }

    /// <summary>
    /// Fecha y hora en que la devolución
    /// fue registrada.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Inicializa una nueva devolución.
    /// </summary>
    /// <param name="id">
    /// Identificador único de la devolución.
    /// </param>
    /// <param name="sku">
    /// SKU del producto devuelto.
    /// </param>
    /// <param name="cantidad">
    /// Cantidad de unidades que serán reintegradas.
    /// </param>
    /// <param name="motivo">
    /// Motivo de la devolución.
    /// </param>
    public Devolucion(
        int id,
        int sku,
        int cantidad,
        string motivo)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(id),
                "El identificador de la devolución debe ser mayor que cero.");
        }

        if (sku <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(sku),
                "El SKU debe ser mayor que cero.");
        }

        if (cantidad <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(cantidad),
                "La cantidad devuelta debe ser mayor que cero.");
        }

        if (string.IsNullOrWhiteSpace(motivo))
        {
            throw new ArgumentException(
                "El motivo de la devolución no puede estar vacío.",
                nameof(motivo));
        }

        IdDevolucion = id;
        SKU = sku;
        Cantidad = cantidad;
        Motivo = motivo;
        Timestamp = DateTime.Now;
    }
}
