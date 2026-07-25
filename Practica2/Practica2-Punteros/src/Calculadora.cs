public static class Calculadora
{
    /// <summary>
    /// Suma un valor a un acumulador, modificando directamente
    /// la variable original mediante el modificador ref.
    /// </summary>
    /// <param name="acumulador">
    /// Variable inicializada que recibirá el resultado de la suma.
    /// </param>
    /// <param name="sumando">
    /// Cantidad que se agregará al acumulador.
    /// </param>
public static void Sumar(ref int acumulador, int sumando)
{
    acumulador += sumando;
}

   /// <summary>
/// Analiza un arreglo de números y produce su promedio
/// y su valor máximo mediante parámetros out.
/// </summary>
/// <param name="valores">
/// Arreglo de valores que será analizado.
/// </param>
/// <param name="promedio">
/// Promedio calculado de los valores.
/// </param>
/// <param name="maximo">
/// Valor máximo encontrado en el arreglo.
/// </param>
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