using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinal_FastCart.Models;
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
}