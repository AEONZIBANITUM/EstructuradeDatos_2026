namespace ProyectoFinal_FastCart.Models;

/// <summary>
/// Representa un nodo individual dentro de la lista
/// doblemente enlazada utilizada por el historial
/// de auditoría de FastCart.
/// </summary>
public class NodoAuditoria
{
    /// <summary>
    /// Registro de auditoría almacenado en el nodo.
    /// </summary>
    public LogMovimiento Dato { get; }

    /// <summary>
    /// Referencia al siguiente evento del historial.
    /// Un valor null indica que el nodo corresponde a la cola.
    /// </summary>
    public NodoAuditoria? Siguiente { get; set; }

    /// <summary>
    /// Referencia al evento anterior del historial.
    /// Un valor null indica que el nodo corresponde a la cabeza.
    /// </summary>
    public NodoAuditoria? Anterior { get; set; }

    /// <summary>
    /// Inicializa un nodo de auditoría con el movimiento especificado.
    /// Los enlaces bidireccionales comienzan sin referencias.
    /// </summary>
    /// <param name="dato">
    /// Movimiento de auditoría almacenado por el nodo.
    /// </param>
    public NodoAuditoria(LogMovimiento dato)
    {
        Dato = dato;
        Siguiente = null;
        Anterior = null;
    }
}