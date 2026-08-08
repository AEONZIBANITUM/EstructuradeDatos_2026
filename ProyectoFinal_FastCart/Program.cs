using System.Diagnostics;
using ProyectoFinal_FastCart.Models;
using ProyectoFinal_FastCart.Services;

Console.WriteLine("==============================================================");
Console.WriteLine("             FASTCART BACKEND CORE - FASE 1");
Console.WriteLine("==============================================================");
Console.WriteLine("          Módulo de Inteligencia de Precios");
Console.WriteLine("          Proyecto Final - Estructura de Datos");
Console.WriteLine("==============================================================");

#if DEBUG
const string modoCompilacion = "Debug";
#else
const string modoCompilacion = "Release";
#endif

const int cantidadProductos = 500;

Console.WriteLine();
Console.WriteLine($"Modo de compilación: {modoCompilacion}");
Console.WriteLine($"Total de productos: {cantidadProductos}");

// Warm-up para reducir el impacto inicial de la compilación JIT.
Producto[] catalogoWarmUp = CatalogoService.GenerarCatalogo(100);
OrdenamientoService.ShellSort(catalogoWarmUp);

// Catálogo real utilizado para la medición.
Producto[] catalogo = CatalogoService.GenerarCatalogo(cantidadProductos);

Console.WriteLine();
Console.WriteLine("CATÁLOGO ANTES DEL ORDENAMIENTO");
Console.WriteLine("--------------------------------------------------------------");

CatalogoService.MostrarPrimeros(catalogo, 5);

Console.WriteLine();
Console.WriteLine("Ejecutando ShellSort...");
Console.WriteLine("Criterio: Precio DESC -> SKU ASC");
Console.WriteLine();

// Medición exclusiva del algoritmo ShellSort.
Stopwatch cronometro = Stopwatch.StartNew();

OrdenamientoService.ShellSort(catalogo);

cronometro.Stop();

Console.WriteLine("CATÁLOGO DESPUÉS DEL ORDENAMIENTO");
Console.WriteLine("--------------------------------------------------------------");

CatalogoService.MostrarPrimeros(catalogo, 5);

Console.WriteLine();
Console.WriteLine("VALIDACIÓN DEL ORDENAMIENTO");
Console.WriteLine("--------------------------------------------------------------");

bool ordenCorrecto =
    OrdenamientoService.EstaCorrectamenteOrdenado(catalogo);

Console.WriteLine(
    ordenCorrecto
        ? "Resultado: CORRECTO - Precio DESC / SKU ASC"
        : "Resultado: ERROR EN EL ORDENAMIENTO");

Console.WriteLine();
Console.WriteLine("PRUEBA DE DESEMPATE POR SKU");
Console.WriteLine("--------------------------------------------------------------");
Console.WriteLine("Productos con precio idéntico de $1499.99:");
Console.WriteLine();

CatalogoService.MostrarPorPrecio(catalogo, 1499.99);

Console.WriteLine();
Console.WriteLine("MÉTRICAS DE RENDIMIENTO - SHELLSORT");
Console.WriteLine("--------------------------------------------------------------");
Console.WriteLine($"Productos procesados: {catalogo.Length}");
Console.WriteLine($"Tiempo: {cronometro.ElapsedMilliseconds} ms");
Console.WriteLine($"Tiempo: {cronometro.Elapsed.TotalMicroseconds:F2} µs");
Console.WriteLine($"Ticks:  {cronometro.ElapsedTicks}");
Console.WriteLine("--------------------------------------------------------------");