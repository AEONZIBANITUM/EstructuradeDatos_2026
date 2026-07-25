namespace CalculadoraFisica;

static class EntradaUsuario
{
    /// <summary>
    /// Solicita un número decimal al usuario con validación robusta.
    /// Reintenta indefinidamente hasta recibir un valor válido y positivo.
    /// </summary>
    /// <param name="mensaje">Texto que se muestra como prompt</param>
    /// <param name="soloPositivos">
    /// Si true, rechaza valores cero o negativos
    /// </param>
    /// <returns>Valor double validado introducido por el usuario</returns>
    public static double PedirDouble(
        string mensaje,
        bool soloPositivos = true)
    {
        while (true)
        {
            Console.Write(mensaje);

            string entrada = Console.ReadLine() ?? "";

            if (double.TryParse(
                entrada,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out double resultado))
            {
                if (soloPositivos && resultado <= 0)
                {
                    Console.WriteLine(
                        " Error: el valor debe ser mayor que cero.");

                    continue;
                }

                return resultado;
            }

            Console.WriteLine(
                " Error: introduce un número válido usando punto decimal.");
        }
    }
}