namespace SistemaInventario;

/// <summary>
/// Representa un producto almacenado dentro del inventario.
/// Al ser un struct, cada instancia se comporta como un tipo por valor.
/// </summary>
public struct Producto
{
    /// <summary>
    /// Identificador único del producto.
    /// </summary>
    public int ID;

    /// <summary>
    /// Nombre descriptivo del producto.
    /// </summary>
    public string Nombre;

    /// <summary>
    /// Precio unitario del producto.
    /// </summary>
    public double Precio;

    /// <summary>
    /// Cantidad disponible en inventario.
    /// </summary>
    public int Stock;

    /// <summary>
    /// Inicializa todos los datos que componen un producto.
    /// </summary>
    public Producto(int id, string nombre, double precio, int stock)
    {
        ID = id;
        Nombre = nombre;
        Precio = precio;
        Stock = stock;
    }
}