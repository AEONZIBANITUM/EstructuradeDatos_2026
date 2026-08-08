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
Console.WriteLine("CATÁLOGO DE PRUEBA GENERADO");
Console.WriteLine("--------------------------------------------------------------");
Console.WriteLine($"Total de productos: {catalogo.Length}");
Console.WriteLine();
Console.WriteLine("Primeros 5 productos:");
Console.WriteLine("--------------------------------------------------------------");

CatalogoService.MostrarPrimeros(catalogo, 5);