using ProyectoFinal_FastCart.Models;

namespace ProyectoFinal_FastCart.Structures;

/// <summary>
/// Nodo utilizado por la pila LIFO de devoluciones.
///
/// Cada nodo almacena una devolución y una referencia
/// hacia el elemento que se encuentra debajo de él.
/// </summary>
public class NodoPila
{
    /// <summary>
    /// Devolución almacenada dentro del nodo.
    /// </summary>
    public Devolucion Dato { get; set; }

    /// <summary>
    /// Referencia al siguiente nodo dentro de la pila.
    ///
    /// En una estructura LIFO esta referencia apunta
    /// hacia el elemento inmediatamente inferior.
    /// </summary>
    public NodoPila? Siguiente { get; set; }

    /// <summary>
    /// Inicializa un nuevo nodo de pila.
    /// </summary>
    /// <param name="devolucion">
    /// Devolución que será almacenada.
    /// </param>
    public NodoPila(
        Devolucion devolucion)
    {
        ArgumentNullException.ThrowIfNull(
            devolucion);

        Dato = devolucion;
        Siguiente = null;
    }
}