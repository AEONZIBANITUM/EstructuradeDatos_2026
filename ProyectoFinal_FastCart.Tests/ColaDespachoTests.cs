using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinal_FastCart.Models;
using ProyectoFinal_FastCart.Services;
using ProyectoFinal_FastCart.Structures;

namespace ProyectoFinal_FastCart.Tests;

/// <summary>
/// Pruebas correspondientes a la estructura FIFO
/// ColaDespacho de la Fase 4.
/// </summary>
[TestClass]
public class ColaDespachoTests
{
    /// <summary>
    /// Verifica que una cola recién creada
    /// no contenga pedidos.
    /// </summary>
    [TestMethod]
    public void ColaNueva_IniciaVacia()
    {
        ColaDespacho cola =
            new ColaDespacho();

        Assert.IsTrue(
            cola.EstaVacia());

        Assert.AreEqual(
            0,
            cola.TotalEncolados);
    }

    /// <summary>
    /// Verifica que el primer pedido pueda
    /// incorporarse correctamente a la cola.
    /// </summary>
    [TestMethod]
    public void EncolarPedido_PrimerPedido_TotalUno()
    {
        ColaDespacho cola =
            new ColaDespacho();

        Pedido pedido =
            new Pedido(
                1,
                1001,
                2,
                "Cliente A");

        cola.EncolarPedido(pedido);

        Assert.IsFalse(
            cola.EstaVacia());

        Assert.AreEqual(
            1,
            cola.TotalEncolados);
    }

    /// <summary>
    /// Verifica que varios pedidos puedan
    /// encadenarse dinámicamente sin perder
    /// el conteo lógico de la cola.
    /// </summary>
    [TestMethod]
    public void EncolarPedido_TresPedidos_TotalTres()
    {
        ColaDespacho cola =
            new ColaDespacho();

        Pedido pedido1 =
            new Pedido(
                1,
                1001,
                1,
                "Cliente A");

        Pedido pedido2 =
            new Pedido(
                2,
                1002,
                2,
                "Cliente B");

        Pedido pedido3 =
            new Pedido(
                3,
                1003,
                3,
                "Cliente C");

        cola.EncolarPedido(pedido1);
        cola.EncolarPedido(pedido2);
        cola.EncolarPedido(pedido3);

        Assert.IsFalse(
            cola.EstaVacia());

        Assert.AreEqual(
            3,
            cola.TotalEncolados);
    }

    /// <summary>
    /// Verifica que la estructura rechace
    /// explícitamente un pedido nulo.
    /// </summary>
    [TestMethod]
    public void EncolarPedido_PedidoNulo_LanzaArgumentNullException()
    {
        ColaDespacho cola =
            new ColaDespacho();

        Assert.ThrowsExactly<ArgumentNullException>(
            () => cola.EncolarPedido(null!));
            
    }
  /// <summary>
/// Verifica el comportamiento FIFO real utilizando
/// dos pedidos sobre un mismo producto.
///
/// También comprueba que cada despacho modifica
/// directamente el stock almacenado en InventarioLista.
/// </summary>
[TestMethod]
public void DespacharPedido_DosPedidos_RespetaFIFOYDescuentaStock()
{
    AuditoriaService auditoria =
        new AuditoriaService();

    InventarioLista inventario =
        new InventarioLista(auditoria);

    Producto producto =
        CrearProductoPrueba(
            sku: 7001,
            stock: 10);

    inventario.InsertarOrdenado(producto);

    ColaDespacho cola =
        new ColaDespacho();

    Pedido pedido1 =
        new Pedido(
            101,
            7001,
            2,
            "Cliente FIFO 1");

    Pedido pedido2 =
        new Pedido(
            102,
            7001,
            3,
            "Cliente FIFO 2");

    cola.EncolarPedido(pedido1);
    cola.EncolarPedido(pedido2);

    Assert.AreEqual(
        2,
        cola.TotalEncolados);

    // =============================================
    // PRIMER DESPACHO
    // =============================================

    Pedido? primerDespacho =
        cola.DespacharPedido(
            inventario,
            auditoria);

    Assert.IsNotNull(
        primerDespacho);

    Assert.AreEqual(
        101,
        primerDespacho.IdPedido);

    Assert.AreEqual(
        1,
        cola.TotalEncolados);

    Producto despuesDelPrimero =
        inventario.BuscarPorSKU(7001);

    Assert.AreEqual(
        8,
        despuesDelPrimero.Stock);

    // =============================================
    // SEGUNDO DESPACHO
    // =============================================

    Pedido? segundoDespacho =
        cola.DespacharPedido(
            inventario,
            auditoria);

    Assert.IsNotNull(
        segundoDespacho);

    Assert.AreEqual(
        102,
        segundoDespacho.IdPedido);

    Producto despuesDelSegundo =
        inventario.BuscarPorSKU(7001);

    Assert.AreEqual(
        5,
        despuesDelSegundo.Stock);

    Assert.AreEqual(
        0,
        cola.TotalEncolados);

    Assert.IsTrue(
        cola.EstaVacia());

    // 1 evento INSERT del inventario
    // + 2 eventos DESPACHO_EXITOSO.
    Assert.AreEqual(
        3,
        auditoria.TotalRegistros);
}

/// <summary>
/// Verifica que intentar despachar una cola vacía
/// produzca un resultado nulo y genere evidencia
/// dentro del servicio de auditoría.
/// </summary>
[TestMethod]
public void DespacharPedido_ColaVacia_RegistraFallo()
{
    AuditoriaService auditoria =
        new AuditoriaService();

    InventarioLista inventario =
        new InventarioLista(auditoria);

    ColaDespacho cola =
        new ColaDespacho();

    Pedido? resultado =
        cola.DespacharPedido(
            inventario,
            auditoria);

    Assert.IsNull(
        resultado);

    Assert.IsTrue(
        cola.EstaVacia());

    Assert.AreEqual(
        0,
        cola.TotalEncolados);

    Assert.AreEqual(
        1,
        auditoria.TotalRegistros);
}

/// <summary>
/// Verifica que un pedido cuyo SKU no existe
/// no modifique el inventario y genere un evento
/// de auditoría de fallo.
/// </summary>
[TestMethod]
public void DespacharPedido_SKUInexistente_RegistraFallo()
{
    AuditoriaService auditoria =
        new AuditoriaService();

    InventarioLista inventario =
        new InventarioLista(auditoria);

    ColaDespacho cola =
        new ColaDespacho();

    Pedido pedido =
        new Pedido(
            201,
            9999,
            1,
            "Cliente SKU inexistente");

    cola.EncolarPedido(pedido);

    Pedido? resultado =
        cola.DespacharPedido(
            inventario,
            auditoria);

    Assert.IsNull(
        resultado);

    Assert.AreEqual(
        0,
        inventario.Contar());

    Assert.AreEqual(
        0,
        cola.TotalEncolados);

    Assert.IsTrue(
        cola.EstaVacia());

    Assert.AreEqual(
        1,
        auditoria.TotalRegistros);
}

/// <summary>
/// Verifica que un pedido cuya cantidad exceda
/// el stock disponible no modifique el producto real.
///
/// El pedido se retira de la cola y el intento fallido
/// queda registrado dentro de auditoría.
/// </summary>
[TestMethod]
public void DespacharPedido_StockInsuficiente_NoModificaStock()
{
    AuditoriaService auditoria =
        new AuditoriaService();

    InventarioLista inventario =
        new InventarioLista(auditoria);

    Producto producto =
        CrearProductoPrueba(
            sku: 8001,
            stock: 2);

    inventario.InsertarOrdenado(producto);

    ColaDespacho cola =
        new ColaDespacho();

    Pedido pedido =
        new Pedido(
            301,
            8001,
            5,
            "Cliente Stock Insuficiente");

    cola.EncolarPedido(pedido);

    Pedido? resultado =
        cola.DespacharPedido(
            inventario,
            auditoria);

    Assert.IsNull(
        resultado);

    Producto productoDespues =
        inventario.BuscarPorSKU(8001);

    // Debe permanecer exactamente igual.
    Assert.AreEqual(
        2,
        productoDespues.Stock);

    Assert.AreEqual(
        0,
        cola.TotalEncolados);

    Assert.IsTrue(
        cola.EstaVacia());

    // INSERT + STOCK_INSUFICIENTE
    Assert.AreEqual(
        2,
        auditoria.TotalRegistros);
}  


/// <summary>
/// Construye un producto controlado para las pruebas
/// de integración logística de la Fase 4.
/// </summary>
/// <param name="sku">
/// SKU utilizado por el producto de prueba.
/// </param>
/// <param name="stock">
/// Cantidad inicial disponible en inventario.
/// </param>
/// <returns>
/// Producto configurado para las pruebas automatizadas.
/// </returns>
private static Producto CrearProductoPrueba(
    int sku,
    int stock)
{
    return new Producto
    {
        SKU = sku,
        Nombre =
            $"Producto Test {sku}",

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