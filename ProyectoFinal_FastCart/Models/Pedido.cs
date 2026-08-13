namespace ProyectoFinal_FastCart.Models;

/// <summary>
/// Representa un pedido pendiente dentro del flujo logístico
/// de despacho de FastCart.
/// </summary>
public class Pedido
{
    /// <summary>
    /// Identificador único del pedido.
    /// </summary>
    public int IdPedido { get; set; }

    /// <summary>
    /// SKU del producto solicitado.
    /// </summary>
    public int SKU { get; set; }

    /// <summary>
    /// Cantidad de unidades solicitadas.
    /// </summary>
    public int Cantidad { get; set; }

    /// <summary>
    /// Nombre o referencia del cliente asociado al pedido.
    /// </summary>
    public string Cliente { get; set; }

    /// <summary>
    /// Fecha y hora en que el pedido fue creado.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Inicializa un nuevo pedido logístico.
    /// </summary>
    /// <param name="id">Identificador único del pedido.</param>
    /// <param name="sku">SKU del producto solicitado.</param>
    /// <param name="cantidad">Cantidad solicitada.</param>
    /// <param name="cliente">Cliente asociado al pedido.</param>
    public Pedido(
        int id,
        int sku,
        int cantidad,
        string cliente)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(id),
                "El identificador del pedido debe ser mayor que cero.");
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
                "La cantidad solicitada debe ser mayor que cero.");
        }

        if (string.IsNullOrWhiteSpace(cliente))
        {
            throw new ArgumentException(
                "El cliente no puede estar vacío.",
                nameof(cliente));
        }

        IdPedido = id;
        SKU = sku;
        Cantidad = cantidad;
        Cliente = cliente;
        Timestamp = DateTime.Now;
    }
}