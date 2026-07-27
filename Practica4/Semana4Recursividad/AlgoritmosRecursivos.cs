namespace Semana4Recursividad;

public static class AlgoritmosRecursivos
{
    public static long FactorialRecursivo(int numero)
    {
        if (numero < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(numero),
                "El factorial no está definido para números negativos.");
        }

        // Caso base: detiene la cadena de llamadas recursivas.
        if (numero <= 1)
        {
            return 1;
        }

        // Caso recursivo: reduce el problema acercándose al caso base.
        checked
        {
            return numero * FactorialRecursivo(numero - 1);
        }
    }

    public static long FibonacciRecursivo(int numero)
    {
        if (numero < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(numero),
                "La sucesión de Fibonacci no está definida para números negativos.");
        }

        // Primer caso base.
        if (numero == 0)
        {
            return 0;
        }

        // Segundo caso base.
        if (numero == 1)
        {
            return 1;
        }

        // Caso recursivo: genera dos nuevas llamadas.
        checked
        {
            return FibonacciRecursivo(numero - 1)
                 + FibonacciRecursivo(numero - 2);
        }
    }
}