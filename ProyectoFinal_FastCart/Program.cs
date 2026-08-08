using ProyectoFinal_FastCart.Models;
using ProyectoFinal_FastCart.Structures;

Console.WriteLine("==============================================================");
Console.WriteLine("             FASTCART BACKEND CORE - FASE 2");
Console.WriteLine("==============================================================");
Console.WriteLine("       Arquitectura Dinámica del Catálogo Maestro");
Console.WriteLine("          Lista Simplemente Enlazada en C#");
Console.WriteLine("==============================================================");

InventarioLista inventario = new InventarioLista();

Proveedor proveedor1 = new Proveedor
{
    IdProveedor = 1,
    NombreCorporativo = "FastSupply México"
};

Proveedor proveedor2 = new Proveedor
{
    IdProveedor = 2,
    NombreCorporativo = "TechDistribution"
};

Proveedor proveedor3 = new Proveedor
{
    IdProveedor = 3,
    NombreCorporativo = "Global Hardware"
};

Proveedor proveedor4 = new Proveedor
{
    IdProveedor = 4,
    NombreCorporativo = "LogistiCore"
};

Proveedor proveedor5 = new Proveedor
{
    IdProveedor = 5,
    NombreCorporativo = "Digital Components"
};

Console.WriteLine();
Console.WriteLine("INSERTANDO 15 PRODUCTOS DINÁMICAMENTE...");
Console.WriteLine("Criterio: Precio ASC");
Console.WriteLine("--------------------------------------------------------------");

// Los productos se insertan deliberadamente en precios desordenados.
// InsertarOrdenado() deberá colocarlos automáticamente en la posición correcta.

inventario.InsertarOrdenado(new Producto
{
    SKU = 2001,
    Nombre = "Laptop Empresarial",
    Precio = 18999.99,
    Stock = 40,
    DatosProveedor = proveedor1
});

inventario.InsertarOrdenado(new Producto
{
    SKU = 2002,
    Nombre = "Mouse Inalámbrico",
    Precio = 549.90,
    Stock = 180,
    DatosProveedor = proveedor4
});

inventario.InsertarOrdenado(new Producto
{
    SKU = 2003,
    Nombre = "Monitor Profesional",
    Precio = 7299.50,
    Stock = 65,
    DatosProveedor = proveedor2
});

inventario.InsertarOrdenado(new Producto
{
    SKU = 2004,
    Nombre = "SSD NVMe 1TB",
    Precio = 1599.00,
    Stock = 110,
    DatosProveedor = proveedor5
});

inventario.InsertarOrdenado(new Producto
{
    SKU = 2005,
    Nombre = "Teclado Mecánico",
    Precio = 1299.99,
    Stock = 95,
    DatosProveedor = proveedor3
});

inventario.InsertarOrdenado(new Producto
{
    SKU = 2006,
    Nombre = "Router Empresarial",
    Precio = 4999.00,
    Stock = 35,
    DatosProveedor = proveedor2
});

inventario.InsertarOrdenado(new Producto
{
    SKU = 2007,
    Nombre = "Docking Station",
    Precio = 2899.90,
    Stock = 55,
    DatosProveedor = proveedor5
});

inventario.InsertarOrdenado(new Producto
{
    SKU = 2008,
    Nombre = "Webcam Full HD",
    Precio = 899.00,
    Stock = 140,
    DatosProveedor = proveedor1
});

inventario.InsertarOrdenado(new Producto
{
    SKU = 2009,
    Nombre = "Memoria RAM 32GB",
    Precio = 2199.99,
    Stock = 85,
    DatosProveedor = proveedor3
});

inventario.InsertarOrdenado(new Producto
{
    SKU = 2010,
    Nombre = "Switch Gigabit",
    Precio = 3499.00,
    Stock = 50,
    DatosProveedor = proveedor4
});

inventario.InsertarOrdenado(new Producto
{
    SKU = 2011,
    Nombre = "Adaptador USB-C",
    Precio = 399.50,
    Stock = 220,
    DatosProveedor = proveedor5
});

inventario.InsertarOrdenado(new Producto
{
    SKU = 2012,
    Nombre = "Impresora Láser",
    Precio = 6199.00,
    Stock = 24,
    DatosProveedor = proveedor1
});

inventario.InsertarOrdenado(new Producto
{
    SKU = 2013,
    Nombre = "Audífonos Profesionales",
    Precio = 1799.90,
    Stock = 75,
    DatosProveedor = proveedor2
});

inventario.InsertarOrdenado(new Producto
{
    SKU = 2014,
    Nombre = "Servidor Compacto",
    Precio = 24999.00,
    Stock = 12,
    DatosProveedor = proveedor3
});

inventario.InsertarOrdenado(new Producto
{
    SKU = 2015,
    Nombre = "Cable Ethernet Cat6",
    Precio = 179.90,
    Stock = 350,
    DatosProveedor = proveedor4
});

Console.WriteLine($"Total insertado dinámicamente: {inventario.Contar()} productos");

Console.WriteLine();
Console.WriteLine("CATÁLOGO DINÁMICO ORDENADO POR PRECIO ASC");
Console.WriteLine("--------------------------------------------------------------");

inventario.MostrarTodos();

Console.WriteLine();
Console.WriteLine("PRUEBA DE BÚSQUEDA POR SKU");
Console.WriteLine("--------------------------------------------------------------");

try
{
    Producto encontrado = inventario.BuscarPorSKU(2007);

    Console.WriteLine("Búsqueda exitosa:");
    Console.WriteLine(
        $"SKU: {encontrado.SKU} | " +
        $"Producto: {encontrado.Nombre} | " +
        $"Precio: ${encontrado.Precio:F2}");
}
catch (KeyNotFoundException ex)
{
    Console.WriteLine($"ERROR CONTROLADO: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("PRUEBA DE SKU INEXISTENTE");
Console.WriteLine("--------------------------------------------------------------");

try
{
    inventario.BuscarPorSKU(9999);
}
catch (KeyNotFoundException ex)
{
    Console.WriteLine($"Excepción controlada: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("PRUEBA DE ELIMINACIÓN");
Console.WriteLine("--------------------------------------------------------------");

Console.WriteLine("Eliminando SKU 2005...");

bool eliminado = inventario.EliminarPorSKU(2005);

Console.WriteLine(
    eliminado
        ? "Resultado: producto eliminado correctamente."
        : "Resultado: SKU no encontrado.");

Console.WriteLine($"Productos restantes: {inventario.Contar()}");

Console.WriteLine();
Console.WriteLine("VERIFICACIÓN DEL SKU ELIMINADO");
Console.WriteLine("--------------------------------------------------------------");

try
{
    inventario.BuscarPorSKU(2005);
}
catch (KeyNotFoundException ex)
{
    Console.WriteLine($"Resultado correcto: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("==============================================================");
Console.WriteLine("        PRUEBA FUNCIONAL DE FASE 2 COMPLETADA");
Console.WriteLine("==============================================================");