public class Calculadora
{
    // TODO: refactorizar este método para usar ref.
    // Retorno explícito: el llamador recibe
    // una copia del resultado.
    public static int Sumar(int a, int b)
    {
        return a + b;
    }

    // TODO: reemplazar este método por AnalizarValores usando out.
    public static double Promediar(int[] valores)
    {
        double suma = 0;

        foreach (int valor in valores)
        {
            suma += valor;
        }

        return suma / valores.Length;
    }
}