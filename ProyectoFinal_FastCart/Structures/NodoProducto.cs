using ProyectoFinal_FastCart.Models;

namespace ProyectoFinal_FastCart.Structures;

/// <summary>
/// Representa un nodo individual dentro de la lista simplemente
/// enlazada utilizada por el catálogo dinámico de FastCart.
/// </summary>
public class NodoProducto
{
    /// <summary>
    /// Producto almacenado dentro del nodo.
    /// </summary>
    public Producto Data { get; set; }

    /// <summary>
    /// Referencia al siguiente nodo de la lista.
    /// Un valor null indica que este nodo es el último de la cadena.
    /// </summary>
    public NodoProducto? Siguiente { get; set; }

    /// <summary>
    /// Inicializa un nuevo nodo con el producto especificado.
    /// El nodo se crea inicialmente sin referencia a otro elemento.
    /// </summary>
    /// <param name="producto">
    /// Producto que será almacenado dentro del nodo.
    /// </param>
    public NodoProducto(Producto producto)
    {
        Data = producto;
        Siguiente = null;
    }
}