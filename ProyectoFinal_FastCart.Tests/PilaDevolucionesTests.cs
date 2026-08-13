using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinal_FastCart.Models;
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
}