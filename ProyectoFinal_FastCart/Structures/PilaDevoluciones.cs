using ProyectoFinal_FastCart.Models;

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
}