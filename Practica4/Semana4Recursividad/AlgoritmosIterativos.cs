namespace Semana4Recursividad;

public static class AlgoritmosIterativos
{
    public static long FactorialIterativo(int numero)
    {
        if (numero < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(numero),
                "El factorial no está definido para números negativos.");
        }

        long resultado = 1;

        for (int i = 2; i <= numero; i++)
        {
            checked
            {
                resultado *= i;
            }
        }

        return resultado;
    }

    public static long FibonacciIterativo(int numero)
    {
        if (numero < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(numero),
                "La sucesión de Fibonacci no está definida para números negativos.");
        }

        if (numero == 0)
        {
            return 0;
        }

        if (numero == 1)
        {
            return 1;
        }

        long anterior = 0;
        long actual = 1;

        for (int i = 2; i <= numero; i++)
        {
            long siguiente;

            checked
            {
                siguiente = anterior + actual;
            }

            anterior = actual;
            actual = siguiente;
        }

        return actual;
    }
}