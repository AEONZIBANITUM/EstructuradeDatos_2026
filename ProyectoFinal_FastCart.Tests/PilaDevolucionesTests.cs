using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinal_FastCart.Models;
using ProyectoFinal_FastCart.Services;
using ProyectoFinal_FastCart.Structures;

namespace ProyectoFinal_FastCart.Tests;

/// <summary>
/// Pruebas unitarias correspondientes a la pila LIFO
/// utilizada para las devoluciones de FastCart.
/// </summary>
[TestClass]
public class PilaDevolucionesTests
{
    /// <summary>
    /// Verifica que una pila recién creada
    /// comience completamente vacía.
    /// </summary>
    [TestMethod]
    public void PilaNueva_IniciaVacia()
    {
        PilaDevoluciones pila =
            new PilaDevoluciones();

        Assert.IsTrue(
            pila.EstaVacia());

        Assert.AreEqual(
            0,
            pila.TotalDevoluciones);
    }

    /// <summary>
    /// Verifica que una devolución pueda agregarse
    /// correctamente a una pila inicialmente vacía.
    /// </summary>
    [TestMethod]
    public void PushDevolucion_PrimeraDevolucion_TotalUno()
    {
        PilaDevoluciones pila =
            new PilaDevoluciones();

        Devolucion devolucion =
            new Devolucion(
                1,
                7001,
                2,
                "Producto defectuoso");

        pila.PushDevolucion(
            devolucion);

        Assert.IsFalse(
            pila.EstaVacia());

        Assert.AreEqual(
            1,
            pila.TotalDevoluciones);
    }

    /// <summary>
    /// Verifica que varias devoluciones puedan
    /// apilarse dinámicamente.
    /// </summary>
    [TestMethod]
    public void PushDevolucion_TresDevoluciones_TotalTres()
    {
        PilaDevoluciones pila =
            new PilaDevoluciones();

        Devolucion devolucion1 =
            new Devolucion(
                1,
                7001,
                1,
                "Motivo A");

        Devolucion devolucion2 =
            new Devolucion(
                2,
                7002,
                2,
                "Motivo B");

        Devolucion devolucion3 =
            new Devolucion(
                3,
                7003,
                3,
                "Motivo C");

        pila.PushDevolucion(
            devolucion1);

        pila.PushDevolucion(
            devolucion2);

        pila.PushDevolucion(
            devolucion3);

        Assert.IsFalse(
            pila.EstaVacia());

        Assert.AreEqual(
            3,
            pila.TotalDevoluciones);
    }

    /// <summary>
    /// Verifica que una devolución nula
    /// sea rechazada explícitamente.
    /// </summary>
    [TestMethod]
    public void PushDevolucion_DevolucionNula_LanzaArgumentNullException()
    {
        PilaDevoluciones pila =
            new PilaDevoluciones();

        Assert.ThrowsExactly<ArgumentNullException>(
            () => pila.PushDevolucion(null!));
    }
    /// <summary>
/// Comprueba que PopDevolucion respete LIFO
/// y reintegre correctamente el stock real.
/// </summary>
[TestMethod]
public void PopDevolucion_DosElementos_RespetaLIFOYReintegraStock()
{
    AuditoriaService auditoria =
        new AuditoriaService();

    InventarioLista inventario =
        new InventarioLista(auditoria);

    Producto producto =
        CrearProductoPrueba(
            sku: 9001,
            stock: 5);

    inventario.InsertarOrdenado(
        producto);

    PilaDevoluciones pila =
        new PilaDevoluciones();

    Devolucion devolucion1 =
        new Devolucion(
            401,
            9001,
            2,
            "Primera devolución");

    Devolucion devolucion2 =
        new Devolucion(
            402,
            9001,
            3,
            "Segunda devolución");

    pila.PushDevolucion(
        devolucion1);

    pila.PushDevolucion(
        devolucion2);

    Assert.AreEqual(
        2,
        pila.TotalDevoluciones);

    // Primer Pop: debe salir la última agregada.
    Devolucion? primeraProcesada =
        pila.PopDevolucion(
            inventario,
            auditoria);

    Assert.IsNotNull(
        primeraProcesada);

    Assert.AreEqual(
        402,
        primeraProcesada.IdDevolucion);

    Producto despuesDelPrimerPop =
        inventario.BuscarPorSKU(9001);

    Assert.AreEqual(
        8,
        despuesDelPrimerPop.Stock);

    Assert.AreEqual(
        1,
        pila.TotalDevoluciones);

    // Segundo Pop.
    Devolucion? segundaProcesada =
        pila.PopDevolucion(
            inventario,
            auditoria);

    Assert.IsNotNull(
        segundaProcesada);

    Assert.AreEqual(
        401,
        segundaProcesada.IdDevolucion);

    Producto despuesDelSegundoPop =
        inventario.BuscarPorSKU(9001);

    Assert.AreEqual(
        10,
        despuesDelSegundoPop.Stock);

    Assert.AreEqual(
        0,
        pila.TotalDevoluciones);

    Assert.IsTrue(
        pila.EstaVacia());

    // INSERT + 2 DEVOLUCION_EXITOSA
    Assert.AreEqual(
        3,
        auditoria.TotalRegistros);
}

/// <summary>
/// Verifica que procesar una pila vacía
/// genere un fallo controlado y auditado.
/// </summary>
[TestMethod]
public void PopDevolucion_PilaVacia_RegistraFallo()
{
    AuditoriaService auditoria =
        new AuditoriaService();

    InventarioLista inventario =
        new InventarioLista(auditoria);

    PilaDevoluciones pila =
        new PilaDevoluciones();

    Devolucion? resultado =
        pila.PopDevolucion(
            inventario,
            auditoria);

    Assert.IsNull(
        resultado);

    Assert.IsTrue(
        pila.EstaVacia());

    Assert.AreEqual(
        0,
        pila.TotalDevoluciones);

    Assert.AreEqual(
        1,
        auditoria.TotalRegistros);
}

/// <summary>
/// Comprueba el manejo de una devolución
/// cuyo SKU no existe en el catálogo.
/// </summary>
[TestMethod]
public void PopDevolucion_SKUInexistente_RegistraFallo()
{
    AuditoriaService auditoria =
        new AuditoriaService();

    InventarioLista inventario =
        new InventarioLista(auditoria);

    PilaDevoluciones pila =
        new PilaDevoluciones();

    Devolucion devolucion =
        new Devolucion(
            501,
            9999,
            1,
            "SKU inexistente");

    pila.PushDevolucion(
        devolucion);

    Devolucion? resultado =
        pila.PopDevolucion(
            inventario,
            auditoria);

    Assert.IsNull(
        resultado);

    Assert.IsTrue(
        pila.EstaVacia());

    Assert.AreEqual(
        0,
        pila.TotalDevoluciones);

    Assert.AreEqual(
        1,
        auditoria.TotalRegistros);
}

/// <summary>
/// Comprueba que las dependencias obligatorias
/// no puedan ser nulas.
/// </summary>
[TestMethod]
public void PopDevolucion_DependenciasNulas_LanzaArgumentNullException()
{
    AuditoriaService auditoria =
        new AuditoriaService();

    InventarioLista inventario =
        new InventarioLista(auditoria);

    PilaDevoluciones pila =
        new PilaDevoluciones();

    Assert.ThrowsExactly<ArgumentNullException>(
        () => pila.PopDevolucion(
            null!,
            auditoria));

    Assert.ThrowsExactly<ArgumentNullException>(
        () => pila.PopDevolucion(
            inventario,
            null!));
}
/// <summary>
/// Construye un producto controlado para
/// las pruebas de devoluciones.
/// </summary>
private static Producto CrearProductoPrueba(
    int sku,
    int stock)
{
    return new Producto
    {
        SKU = sku,

        Nombre =
            $"Producto Devolución {sku}",

        Precio =
            1000.00,

        Stock =
            stock,

        DatosProveedor =
            new Proveedor
            {
                IdProveedor = 1,

                NombreCorporativo =
                    "Proveedor Test FastCart"
            }
    };
}
}