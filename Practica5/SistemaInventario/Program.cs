namespace SistemaInventario;

internal static class Program
{
    /// <summary>
    /// Número máximo de productos permitidos en el arreglo estático.
    /// </summary>
    private const int CapacidadMaxima = 10;

    private static void Main()
    {
        // El arreglo almacena estructuras Producto.
        Producto[] inventario = new Producto[CapacidadMaxima];

        // Controla cuántas posiciones del arreglo contienen productos válidos.
        int totalRegistros = 0;

        Console.Title = "Sistema de Gestión de Inventario";

        Console.WriteLine("==========================================");
        Console.WriteLine("   SISTEMA DE GESTIÓN DE INVENTARIO");
        Console.WriteLine("==========================================");
        Console.WriteLine();
        Console.WriteLine($"Capacidad máxima:       {inventario.Length} productos");
        Console.WriteLine($"Productos registrados: {totalRegistros}");
        Console.WriteLine();
        Console.WriteLine("Modelo Producto y arreglo creados correctamente.");
    }
}