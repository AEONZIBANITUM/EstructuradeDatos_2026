namespace CalculadoraFisica;

/// <summary>
/// Módulo de cálculos físicos de cinemática.
/// Todas las funciones son puras: mismo input = mismo output.
/// </summary>
static class Calculos
{

    public static double CalcularVelocidad(
        double distanciaMetros,
        double tiempoSegundos)
    {
        return distanciaMetros / tiempoSegundos;
    }

    /// <summary>Calcula distancia: d = v * t</summary>
    public static double CalcularDistancia(
        double velocidadMs,
        double tiempoSegundos)
    {
        return velocidadMs * tiempoSegundos;
    }

    /// <summary>Calcula tiempo: t = d / v</summary>
    public static double CalcularTiempo(
        double distanciaMetros,
        double velocidadMs)
    {
        return distanciaMetros / velocidadMs;
    }
}