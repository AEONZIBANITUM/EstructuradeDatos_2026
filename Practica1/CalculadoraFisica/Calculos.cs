namespace CalculadoraFisica;

/// <summary>
/// Proporciona las operaciones matemáticas de cinemática.
/// Todas sus funciones son puras: la misma entrada produce la misma salida.
/// </summary>
static class Calculos
{
    /// <summary>
    /// Calcula la velocidad a partir de la distancia recorrida y el tiempo empleado.
    /// </summary>
    /// <param name="distanciaMetros">
    /// Distancia recorrida expresada en metros.
    /// </param>
    /// <param name="tiempoSegundos">
    /// Tiempo empleado expresado en segundos.
    /// </param>
    /// <returns>
    /// Velocidad calculada en metros por segundo.
    /// </returns>
    public static double CalcularVelocidad(
        double distanciaMetros,
        double tiempoSegundos)
    {
        return distanciaMetros / tiempoSegundos;
    }

    /// <summary>
    /// Calcula la distancia recorrida a partir de la velocidad y el tiempo.
    /// </summary>
    /// <param name="velocidadMs">
    /// Velocidad expresada en metros por segundo.
    /// </param>
    /// <param name="tiempoSegundos">
    /// Tiempo empleado expresado en segundos.
    /// </param>
    /// <returns>
    /// Distancia calculada en metros.
    /// </returns>
    public static double CalcularDistancia(
        double velocidadMs,
        double tiempoSegundos)
    {
        return velocidadMs * tiempoSegundos;
    }

    /// <summary>
    /// Calcula el tiempo a partir de la distancia recorrida y la velocidad.
    /// </summary>
    /// <param name="distanciaMetros">
    /// Distancia recorrida expresada en metros.
    /// </param>
    /// <param name="velocidadMs">
    /// Velocidad expresada en metros por segundo.
    /// </param>
    /// <returns>
    /// Tiempo calculado en segundos.
    /// </returns>
    public static double CalcularTiempo(
        double distanciaMetros,
        double velocidadMs)
    {
        return distanciaMetros / velocidadMs;
    }
}