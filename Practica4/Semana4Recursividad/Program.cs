using Semana4Recursividad;

Console.WriteLine("================================================");
Console.WriteLine(" PRÁCTICA 4 - IMPLEMENTACIÓN SEGURA DE RECURSIÓN");
Console.WriteLine("     COMPARACIÓN FUNCIONAL INICIAL");
Console.WriteLine("================================================");
Console.WriteLine();

int numeroFactorial = 5;
int numeroFibonacci = 10;

long factorialIterativo =
    AlgoritmosIterativos.FactorialIterativo(numeroFactorial);

long factorialRecursivo =
    AlgoritmosRecursivos.FactorialRecursivo(numeroFactorial);

long fibonacciIterativo =
    AlgoritmosIterativos.FibonacciIterativo(numeroFibonacci);

long fibonacciRecursivo =
    AlgoritmosRecursivos.FibonacciRecursivo(numeroFibonacci);

Console.WriteLine($"Factorial iterativo de {numeroFactorial}: {factorialIterativo}");
Console.WriteLine($"Factorial recursivo de {numeroFactorial}: {factorialRecursivo}");
Console.WriteLine();

Console.WriteLine($"Fibonacci iterativo de {numeroFibonacci}: {fibonacciIterativo}");
Console.WriteLine($"Fibonacci recursivo de {numeroFibonacci}: {fibonacciRecursivo}");
Console.WriteLine();

bool factorialCoincide = factorialIterativo == factorialRecursivo;
bool fibonacciCoincide = fibonacciIterativo == fibonacciRecursivo;

Console.WriteLine(
    $"¿Coinciden los resultados de factorial?: " +
    $"{(factorialCoincide ? "Sí" : "No")}");

Console.WriteLine(
    $"¿Coinciden los resultados de Fibonacci?: " +
    $"{(fibonacciCoincide ? "Sí" : "No")}");

Console.WriteLine();
Console.WriteLine("Pruebas funcionales completadas.");