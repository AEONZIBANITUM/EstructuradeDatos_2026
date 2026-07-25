namespace CalculadoraFisica;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

         bool continuar = true;

while (continuar)
{
    MostrarMenu();

    string opcion = Console.ReadLine() ?? "0";

    continuar = ProcesarOpcion(opcion);
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


}

static bool ProcesarOpcion(string opcion)
{
    Console.WriteLine();

    switch (opcion.Trim())
    {
        case "1":
            double d1 = EntradaUsuario.PedirDouble(
                " Distancia (m): ");

            double t1 = EntradaUsuario.PedirDouble(
                " Tiempo (s): ");

            double v = Calculos.CalcularVelocidad(d1, t1);

            Console.WriteLine(
                $"\n Velocidad: {v:F2} m/s");

            Console.WriteLine(
                "\n Presiona una tecla para continuar...");

            Console.ReadKey();

            return true;
case "2":
    double v2 = EntradaUsuario.PedirDouble(
        " Velocidad (m/s): ");

    double t2 = EntradaUsuario.PedirDouble(
        " Tiempo (s): ");

    double d = Calculos.CalcularDistancia(v2, t2);

    Console.WriteLine(
        $"\n Distancia: {d:F2} m");

    Console.WriteLine(
        "\n Presiona una tecla para continuar...");

    Console.ReadKey();

    return true;
 case "3":
    double d3 = EntradaUsuario.PedirDouble(
        " Distancia (m): ");

    double v3 = EntradaUsuario.PedirDouble(
        " Velocidad (m/s): ");

    double t = Calculos.CalcularTiempo(d3, v3);

    Console.WriteLine(
        $"\n Tiempo: {t:F2} s");

    Console.WriteLine(
        "\n Presiona una tecla para continuar...");

    Console.ReadKey();

    return true;
    
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