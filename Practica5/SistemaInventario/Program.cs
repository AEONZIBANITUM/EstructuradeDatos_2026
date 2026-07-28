namespace SistemaInventario;

internal static class Program
{
    /// <summary>
    /// Número máximo de productos permitidos en el arreglo estático.
    /// </summary>
    private const int CapacidadMaxima = 10;

    private static void Main()
    {
        Producto[] inventario = new Producto[CapacidadMaxima];
        int totalRegistros = 0;
        int opcion;

        Console.Title = "Sistema de Gestión de Inventario";

        do
        {
            Console.Clear();

            MostrarMenu(totalRegistros, inventario.Length);
            opcion = LeerOpcionMenu();

            Console.Clear();

            switch (opcion)
            {
                case 1:
                    MostrarModuloPendiente("REGISTRAR PRODUCTO");
                    break;

                case 2:
                    MostrarModuloPendiente("MOSTRAR INVENTARIO");
                    break;

                case 3:
                    MostrarModuloPendiente("BUSCAR PRODUCTO POR ID");
                    break;

                case 4:
                    MostrarModuloPendiente("ACTUALIZAR STOCK");
                    break;

                case 5:
                    MostrarModuloPendiente("GUARDAR INVENTARIO EN CSV");
                    break;

                case 6:
                    MostrarModuloPendiente("CARGAR INVENTARIO DESDE CSV");
                    break;

                case 7:
                    MostrarDespedida();
                    break;
            }

            if (opcion != 7)
            {
                Pausar();
            }

        } while (opcion != 7);
    }

    /// <summary>
    /// Muestra las opciones disponibles y el estado actual del inventario.
    /// </summary>
    private static void MostrarMenu(int totalRegistros, int capacidad)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("       SISTEMA DE GESTIÓN DE INVENTARIO");
        Console.WriteLine("==================================================");
        Console.WriteLine($"Productos registrados: {totalRegistros}/{capacidad}");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("1. Registrar producto");
        Console.WriteLine("2. Mostrar inventario");
        Console.WriteLine("3. Buscar producto por ID");
        Console.WriteLine("4. Actualizar stock");
        Console.WriteLine("5. Guardar inventario en archivo CSV");
        Console.WriteLine("6. Cargar inventario desde archivo CSV");
        Console.WriteLine("7. Salir");
        Console.WriteLine("--------------------------------------------------");
    }

    /// <summary>
    /// Solicita y valida una opción numérica comprendida entre 1 y 7.
    /// </summary>
    private static int LeerOpcionMenu()
    {
        int opcion;

        while (true)
        {
            Console.Write("Selecciona una opción: ");
            string? entrada = Console.ReadLine();

            if (int.TryParse(entrada, out opcion) &&
                opcion >= 1 &&
                opcion <= 7)
            {
                return opcion;
            }

            Console.WriteLine();
            Console.WriteLine("Entrada no válida. Escribe un número del 1 al 7.");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Muestra temporalmente una sección que será implementada después.
    /// </summary>
    private static void MostrarModuloPendiente(string titulo)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine($"   {titulo}");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.WriteLine("Módulo preparado. Su lógica será implementada");
        Console.WriteLine("en la siguiente etapa de la práctica.");
    }

    /// <summary>
    /// Muestra el mensaje final al cerrar correctamente la aplicación.
    /// </summary>
    private static void MostrarDespedida()
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("       SISTEMA DE GESTIÓN DE INVENTARIO");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.WriteLine("El sistema se cerró correctamente.");
        Console.WriteLine("Gracias por utilizar la aplicación.");
        Console.WriteLine();
    }

    /// <summary>
    /// Detiene temporalmente el programa antes de regresar al menú.
    /// </summary>
    private static void Pausar()
    {
        Console.WriteLine();
        Console.WriteLine("Presiona ENTER para volver al menú...");
        Console.ReadLine();
    }
}