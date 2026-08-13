using ProyectoFinal_FastCart.Models;

namespace ProyectoFinal_FastCart.Services;

public static class CatalogoService
{
    private static readonly string[] NombresProductos =
    {
        "Laptop Empresarial",
        "Monitor Profesional",
        "Teclado Mecánico",
        "Mouse Inalámbrico",
        "SSD NVMe",
        "Memoria RAM",
        "Router Empresarial",
        "Webcam Full HD",
        "Audífonos Profesionales",
        "Docking Station"
    };

    private static readonly string[] NombresProveedores =
    {
        "FastSupply México",
        "TechDistribution",
        "Global Hardware",
        "LogistiCore",
        "Digital Components"
    };

    public static Producto[] GenerarCatalogo(int cantidad)
    {
        if (cantidad <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(cantidad),
                "La cantidad de productos debe ser mayor que cero.");
        }

        Producto[] catalogo = new Producto[cantidad];

        // Semilla fija para obtener datos reproducibles.
        Random random = new Random(2603);

        for (int i = 0; i < cantidad; i++)
        {
            Proveedor proveedor = new Proveedor
            {
                IdProveedor = (i % NombresProveedores.Length) + 1,
                NombreCorporativo =
                    NombresProveedores[i % NombresProveedores.Length]
            };

            double precio = Math.Round(
                10.00 + random.NextDouble() * (9999.99 - 10.00),
                2);

            // Los primeros tres productos comparten precio
            // para comprobar posteriormente el desempate por SKU.
            if (i < 3)
            {
                precio = 1499.99;
            }

            catalogo[i] = new Producto
            {
                SKU = 1001 + i,
                Nombre =
                    $"{NombresProductos[i % NombresProductos.Length]} {i + 1:D2}",
                Precio = precio,
                Stock = random.Next(0, 501),
                DatosProveedor = proveedor
            };
        }

        return catalogo;
    }

    public static void MostrarPrimeros(Producto[] catalogo, int cantidad)
    {
        int limite = Math.Min(cantidad, catalogo.Length);

        for (int i = 0; i < limite; i++)
        {
            Producto producto = catalogo[i];

            Console.WriteLine(
                $"SKU: {producto.SKU} | " +
                $"Producto: {producto.Nombre} | " +
                $"Precio: ${producto.Precio:F2} | " +
                $"Stock: {producto.Stock} | " +
                $"Proveedor: {producto.DatosProveedor.NombreCorporativo}");
        }
    }
    public static void MostrarPorPrecio(Producto[] catalogo, double precio)
{
    for (int i = 0; i < catalogo.Length; i++)
    {
        Producto producto = catalogo[i];

        if (producto.Precio == precio)
        {
            Console.WriteLine(
                $"SKU: {producto.SKU} | " +
                $"Producto: {producto.Nombre} | " +
                $"Precio: ${producto.Precio:F2}");
        }
    }
}
}