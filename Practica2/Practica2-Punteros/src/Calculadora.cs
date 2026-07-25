public class Calculadora
{
    // 'ref' modifica directamente una variable previamente inicializada.
public static void Sumar(ref int acumulador, int sumando)
{
    acumulador += sumando;
}

   // 'out' produce múltiples resultados.
public static void AnalizarValores(
    int[] valores,
    out double promedio,
    out int maximo)
{
    double suma = 0;
    maximo = valores[0];

    foreach (int valor in valores)
    {
        suma += valor;

        if (valor > maximo)
        {
            maximo = valor;
        }
    }

    promedio = suma / valores.Length;
}
}