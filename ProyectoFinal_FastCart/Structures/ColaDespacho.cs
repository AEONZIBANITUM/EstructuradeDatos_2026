using ProyectoFinal_FastCart.Models;

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
}