using ProyectoFinal_FastCart.Models;
using ProyectoFinal_FastCart.Services;

Console.WriteLine("==============================================================");
Console.WriteLine("             FASTCART BACKEND CORE - FASE 1");
Console.WriteLine("==============================================================");
Console.WriteLine("          Módulo de Inteligencia de Precios");
Console.WriteLine("          Proyecto Final - Estructura de Datos");
Console.WriteLine("==============================================================");

Producto[] catalogo = CatalogoService.GenerarCatalogo(50);

Console.WriteLine();
Console.WriteLine("CATÁLOGO ANTES DEL ORDENAMIENTO");
Console.WriteLine("--------------------------------------------------------------");
Console.WriteLine($"Total de productos: {catalogo.Length}");
Console.WriteLine();

CatalogoService.MostrarPrimeros(catalogo, 5);

Console.WriteLine();
Console.WriteLine("Ejecutando ShellSort...");
Console.WriteLine("Criterio: Precio DESC -> SKU ASC");
Console.WriteLine();

OrdenamientoService.ShellSort(catalogo);

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