using ProyectoFinal_FastCart.Models;

namespace ProyectoFinal_FastCart.Structures;

/// <summary>
/// Nodo utilizado por la cola FIFO de despacho.
/// Cada nodo almacena un pedido y una referencia
/// hacia el siguiente elemento de la cola.
/// </summary>
public class NodoCola
{
    /// <summary>
    /// Pedido almacenado dentro del nodo.
    /// </summary>
    public Pedido Dato { get; set; }

    /// <summary>
    /// Referencia al siguiente nodo de la cola.
    /// Null indica que este nodo es actualmente el último.
    /// </summary>
    public NodoCola? Siguiente { get; set; }

    /// <summary>
    /// Inicializa un nodo de cola con el pedido especificado.
    /// </summary>
    /// <param name="pedido">Pedido almacenado en el nodo.</param>
    public NodoCola(Pedido pedido)
    {
        ArgumentNullException.ThrowIfNull(pedido);

        Dato = pedido;
        Siguiente = null;
    }
}