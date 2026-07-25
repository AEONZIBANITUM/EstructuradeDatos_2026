namespace CalculadoraFisica;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        bool continuarPrograma = true;

        while (continuarPrograma)
        {
            MostrarMenu();

            string opcionSeleccionada = Console.ReadLine() ?? "0";

            continuarPrograma = ProcesarOpcion(opcionSeleccionada);
        }
    }

    static void MostrarMenu()
    {
        Console.Clear();

        Console.WriteLine("┌────────────────────────────────┐");
        Console.WriteLine("│ CALCULADORA DE CINEMÁTICA      │");
        Console.WriteLine("├────────────────────────────────┤");
        Console.WriteLine("│ 1. Calcular Velocidad          │");
        Console.WriteLine("│ 2. Calcular Distancia          │");
        Console.WriteLine("│ 3. Calcular Tiempo             │");
        Console.WriteLine("│ 0. Salir                       │");
        Console.WriteLine("└────────────────────────────────┘");

        Console.Write("\n Elige una opción: ");
    }

    static bool ProcesarOpcion(string opcionSeleccionada)
    {
        Console.WriteLine();

        switch (opcionSeleccionada.Trim())
        {
            case "1":
            {
                double distanciaMetros = EntradaUsuario.PedirDouble(
                    " Distancia (m): ");

                double tiempoSegundos = EntradaUsuario.PedirDouble(
                    " Tiempo (s): ");

                double velocidadMetrosPorSegundo =
                    Calculos.CalcularVelocidad(
                        distanciaMetros,
                        tiempoSegundos);

                Console.WriteLine(
                    $"\n Velocidad: {velocidadMetrosPorSegundo:F2} m/s");

                Console.WriteLine(
                    "\n Presiona una tecla para continuar...");

                Console.ReadKey();

                return true;
            }

            case "2":
            {
                double velocidadMetrosPorSegundo =
                    EntradaUsuario.PedirDouble(
                        " Velocidad (m/s): ");

                double tiempoSegundos = EntradaUsuario.PedirDouble(
                    " Tiempo (s): ");

                double distanciaMetros =
                    Calculos.CalcularDistancia(
                        velocidadMetrosPorSegundo,
                        tiempoSegundos);

                Console.WriteLine(
                    $"\n Distancia: {distanciaMetros:F2} m");

                Console.WriteLine(
                    "\n Presiona una tecla para continuar...");

                Console.ReadKey();

                return true;
            }

            case "3":
            {
                double distanciaMetros = EntradaUsuario.PedirDouble(
                    " Distancia (m): ");

                double velocidadMetrosPorSegundo =
                    EntradaUsuario.PedirDouble(
                        " Velocidad (m/s): ");

                double tiempoSegundos =
                    Calculos.CalcularTiempo(
                        distanciaMetros,
                        velocidadMetrosPorSegundo);

                Console.WriteLine(
                    $"\n Tiempo: {tiempoSegundos:F2} s");

                Console.WriteLine(
                    "\n Presiona una tecla para continuar...");

                Console.ReadKey();

                return true;
            }

            case "0":
                Console.WriteLine(" Hasta luego.");

                return false;

            default:
                Console.WriteLine(" Opción no válida.");

                Console.WriteLine(
                    "\n Presiona una tecla para continuar...");

                Console.ReadKey();

                return true;
        }
    }
}