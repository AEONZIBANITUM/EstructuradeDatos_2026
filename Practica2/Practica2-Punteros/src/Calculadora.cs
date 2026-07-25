public class Calculadora
{
    // 'ref' modifica directamente una variable previamente inicializada.
public static void Sumar(ref int acumulador, int sumando)
{
    acumulador += sumando;
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