using System.Diagnostics;
using Semana4Recursividad;

// ============================================================
// HARNESS DE PRUEBA COMPARATIVA
// ============================================================

int nFactorial = 20;
int nFibonacci = 40;

var cronometro = new Stopwatch();

Console.WriteLine("============================================================");
Console.WriteLine("       COMPARATIVA: MÉTODOS ITERATIVOS Y RECURSIVOS");
Console.WriteLine("============================================================");
Console.WriteLine();

// ------------------------------------------------------------
// FACTORIAL
// ------------------------------------------------------------

Console.WriteLine($"--- FACTORIAL({nFactorial}) ---");

cronometro.Restart();

long factorialIterativo =
    AlgoritmosIterativos.FactorialIterativo(nFactorial);

cronometro.Stop();

double tiempoFactorialIterativoMs =
    cronometro.Elapsed.TotalMilliseconds;

long ticksFactorialIterativo =
    cronometro.ElapsedTicks;

Console.WriteLine(
    $"[Iterativo] Resultado: {factorialIterativo,25}");

Console.WriteLine(
    $"            Tiempo:   {tiempoFactorialIterativoMs:F6} ms");

Console.WriteLine(
    $"            Ticks:    {ticksFactorialIterativo}");

Console.WriteLine();

cronometro.Restart();

long factorialRecursivo =
    AlgoritmosRecursivos.FactorialRecursivo(nFactorial);

cronometro.Stop();

double tiempoFactorialRecursivoMs =
    cronometro.Elapsed.TotalMilliseconds;

long ticksFactorialRecursivo =
    cronometro.ElapsedTicks;

Console.WriteLine(
    $"[Recursivo] Resultado: {factorialRecursivo,25}");

Console.WriteLine(
    $"            Tiempo:   {tiempoFactorialRecursivoMs:F6} ms");

Console.WriteLine(
    $"            Ticks:    {ticksFactorialRecursivo}");

// ------------------------------------------------------------
// FIBONACCI
// ------------------------------------------------------------

Console.WriteLine();
Console.WriteLine($"--- FIBONACCI({nFibonacci}) ---");

cronometro.Restart();

long fibonacciIterativo =
    AlgoritmosIterativos.FibonacciIterativo(nFibonacci);

cronometro.Stop();

double tiempoFibonacciIterativoMs =
    cronometro.Elapsed.TotalMilliseconds;

long ticksFibonacciIterativo =
    cronometro.ElapsedTicks;

Console.WriteLine(
    $"[Iterativo] Resultado: {fibonacciIterativo,25}");

Console.WriteLine(
    $"            Tiempo:   {tiempoFibonacciIterativoMs:F6} ms");

Console.WriteLine(
    $"            Ticks:    {ticksFibonacciIterativo}");

Console.WriteLine();

cronometro.Restart();

long fibonacciRecursivo =
    AlgoritmosRecursivos.FibonacciRecursivo(nFibonacci);

cronometro.Stop();

double tiempoFibonacciRecursivoMs =
    cronometro.Elapsed.TotalMilliseconds;

long ticksFibonacciRecursivo =
    cronometro.ElapsedTicks;

Console.WriteLine(
    $"[Recursivo] Resultado: {fibonacciRecursivo,25}");

Console.WriteLine(
    $"            Tiempo:   {tiempoFibonacciRecursivoMs:F6} ms");

Console.WriteLine(
    $"            Ticks:    {ticksFibonacciRecursivo}");

// ------------------------------------------------------------
// VALIDACIÓN DE RESULTADOS
// ------------------------------------------------------------

bool factorialCoincide =
    factorialIterativo == factorialRecursivo;

bool fibonacciCoincide =
    fibonacciIterativo == fibonacciRecursivo;

Console.WriteLine();
Console.WriteLine("--- VALIDACIÓN ---");

Console.WriteLine(
    $"Factorial iterativo y recursivo coinciden: " +
    $"{(factorialCoincide ? "Sí" : "No")}");

Console.WriteLine(
    $"Fibonacci iterativo y recursivo coinciden: " +
    $"{(fibonacciCoincide ? "Sí" : "No")}");

Console.WriteLine();
Console.WriteLine(
    "[OK] Prueba comparativa completada correctamente.");