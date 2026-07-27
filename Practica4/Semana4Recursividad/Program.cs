using Semana4Recursividad;

Console.WriteLine("==============================================");
Console.WriteLine(" PRÁCTICA 4 - IMPLEMENTACIÓN DE RECURSIVIDAD");
Console.WriteLine("        PRUEBAS DE MÉTODOS ITERATIVOS");
Console.WriteLine("==============================================");
Console.WriteLine();

int numeroFactorial = 5;
int numeroFibonacci = 10;

long factorial = AlgoritmosIterativos.FactorialIterativo(numeroFactorial);
long fibonacci = AlgoritmosIterativos.FibonacciIterativo(numeroFibonacci);

Console.WriteLine($"Factorial iterativo de {numeroFactorial}: {factorial}");
Console.WriteLine($"Fibonacci iterativo de {numeroFibonacci}: {fibonacci}");

Console.WriteLine();
Console.WriteLine("Pruebas iterativas completadas correctamente.");