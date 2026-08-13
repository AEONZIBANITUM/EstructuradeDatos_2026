using ProyectoFinal_FastCart.Models;

namespace ProyectoFinal_FastCart.Services;

/// <summary>
/// Administra el historial de auditoría de FastCart mediante
/// una lista doblemente enlazada de movimientos.
/// </summary>
public class AuditoriaService
{
    /// <summary>
    /// Referencia al evento más antiguo registrado.
    /// </summary>
    private NodoAuditoria? _cabeza;

    /// <summary>
    /// Referencia al evento más reciente registrado.
    /// </summary>
    private NodoAuditoria? _cola;

    private int _totalRegistros;

    /// <summary>
    /// Obtiene la cantidad total de movimientos almacenados
    /// actualmente en la bitácora.
    /// </summary>
    public int TotalRegistros => _totalRegistros;

    /// <summary>
    /// Inicializa una bitácora de auditoría vacía.
    /// </summary>
    public AuditoriaService()
    {
        _cabeza = null;
        _cola = null;
        _totalRegistros = 0;
    }

    /// <summary>
    /// Registra un nuevo evento al final de la bitácora,
    /// preservando el orden cronológico.
    /// Complejidad temporal O(1).
    /// </summary>
    /// <param name="tipo">Tipo de operación realizada.</param>
    /// <param name="productoId">SKU del producto afectado.</param>
    /// <param name="referencia">
    /// Descripción legible del movimiento realizado.
    /// </param>
    public void RegistrarEvento(
        string tipo,
        int productoId,
        string referencia)
    {
        ArgumentNullException.ThrowIfNull(tipo);
        ArgumentNullException.ThrowIfNull(referencia);

        LogMovimiento movimiento = new LogMovimiento
        {
            TipoOperacion = tipo,
            ProductoId = productoId,
            Referencia = referencia,
            FechaHora = DateTime.UtcNow
        };

        NodoAuditoria nuevoNodo = new NodoAuditoria(movimiento);

        // Caso especial: primer evento.
        if (_cola == null)
        {
            _cabeza = nuevoNodo;
            _cola = nuevoNodo;
            _totalRegistros = 1;
            return;
        }

        // Orden obligatorio de enlace:
        // 1. Cola anterior -> nuevo nodo.
        // 2. Nuevo nodo -> Cola anterior.
        // 3. Avanzar el puntero Cola.
        _cola.Siguiente = nuevoNodo;
        nuevoNodo.Anterior = _cola;
        _cola = nuevoNodo;

        _totalRegistros++;
    }

    /// <summary>
    /// Imprime el historial desde el evento más antiguo
    /// hasta el más reciente.
    /// Complejidad temporal O(n).
    /// </summary>
    public void ImprimirHistorial()
    {
        if (_cabeza == null)
        {
            Console.WriteLine(
                "[Bitácora vacía - no se han registrado eventos]");
            return;
        }

        Console.WriteLine(
            "=== HISTORIAL CRONOLÓGICO (Antiguo -> Reciente) ===");

        NodoAuditoria? actual = _cabeza;
        int contador = 1;

        while (actual != null)
        {
            Console.WriteLine(
                $"[{contador}] " +
                $"{actual.Dato.FechaHora:yyyy-MM-dd HH:mm:ss.fff} UTC");

            Console.WriteLine(
                $"Operación : {actual.Dato.TipoOperacion}");

            Console.WriteLine(
                $"SKU       : {actual.Dato.ProductoId}");

            Console.WriteLine(
                $"Detalle   : {actual.Dato.Referencia}");

            Console.WriteLine();

            actual = actual.Siguiente;
            contador++;
        }

        Console.WriteLine($"Total de eventos: {contador - 1}");
    }

    /// <summary>
    /// Imprime el historial desde el evento más reciente
    /// hasta el más antiguo utilizando las referencias Anterior.
    /// Complejidad temporal O(n).
    /// </summary>
    public void ImprimirHistorialInverso()
    {
        if (_cola == null)
        {
            Console.WriteLine(
                "[Bitácora vacía - no se han registrado eventos]");
            return;
        }

        if (!ValidarIntegridad())
        {
            throw new InvalidOperationException(
                "La lista de auditoría presenta inconsistencias " +
                "en sus enlaces bidireccionales.");
        }

        Console.WriteLine(
            "=== HISTORIAL INVERSO (Reciente -> Antiguo) ===");

        NodoAuditoria? actual = _cola;
        int contador = 1;

        while (actual != null)
        {
            Console.WriteLine(
                $"[{contador}] " +
                $"{actual.Dato.FechaHora:yyyy-MM-dd HH:mm:ss.fff} UTC");

            Console.WriteLine(
                $"Operación : {actual.Dato.TipoOperacion}");

            Console.WriteLine(
                $"SKU       : {actual.Dato.ProductoId}");

            Console.WriteLine(
                $"Detalle   : {actual.Dato.Referencia}");

            Console.WriteLine();

            actual = actual.Anterior;
            contador++;
        }

        Console.WriteLine($"Total de eventos: {contador - 1}");
    }

    /// <summary>
    /// Verifica internamente que los recorridos hacia adelante
    /// y hacia atrás contengan exactamente los mismos nodos.
    /// </summary>
    internal bool ValidarIntegridad()
    {
        if (_totalRegistros == 0)
        {
            return _cabeza == null && _cola == null;
        }

        if (_cabeza == null || _cola == null)
        {
            return false;
        }

        if (_cabeza.Anterior != null ||
            _cola.Siguiente != null)
        {
            return false;
        }

        int haciaAdelante = 0;
        NodoAuditoria? actual = _cabeza;

        while (actual != null)
        {
            haciaAdelante++;
            actual = actual.Siguiente;
        }

        int haciaAtras = 0;
        actual = _cola;

        while (actual != null)
        {
            haciaAtras++;
            actual = actual.Anterior;
        }

        return haciaAdelante == haciaAtras &&
               haciaAdelante == _totalRegistros;
    }
}