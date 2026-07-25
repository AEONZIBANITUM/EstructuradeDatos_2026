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
/// Intercambia los valores de dos variables existentes
/// mediante parámetros por referencia.
/// </summary>
/// <param name="primero">
/// Primera variable cuyo valor será intercambiado.
/// </param>
/// <param name="segundo">
/// Segunda variable cuyo valor será intercambiado.
/// </param>
public static void Intercambiar(ref int primero, ref int segundo)
{
    int temporal = primero;
    primero = segundo;
    segundo = temporal;
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
/// <summary>
/// Intenta realizar una división entera y produce el cociente
/// y el residuo mediante parámetros de salida.
/// </summary>
/// <param name="dividendo">
/// Número que será dividido.
/// </param>
/// <param name="divisor">
/// Número entre el cual se realizará la división.
/// </param>
/// <param name="cociente">
/// Resultado entero de la división.
/// </param>
/// <param name="residuo">
/// Resto generado por la división entera.
/// </param>
/// <returns>
/// true si la división pudo realizarse; false si el divisor es cero.
/// </returns>
public static bool IntentarDividir(
    int dividendo,
    int divisor,
    out int cociente,
    out int residuo)
{
    if (divisor == 0)
    {
        cociente = 0;
        residuo = dividendo;
        return false;
    }

    cociente = dividendo / divisor;
    residuo = dividendo % divisor;

    return true;
}
}