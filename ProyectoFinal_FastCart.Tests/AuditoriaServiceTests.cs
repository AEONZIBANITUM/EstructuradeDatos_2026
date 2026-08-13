using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinal_FastCart.Models;
using ProyectoFinal_FastCart.Services;
using ProyectoFinal_FastCart.Structures;

namespace ProyectoFinal_FastCart.Tests;

/// <summary>
/// Suite de pruebas unitarias para el motor de auditoría
/// bidireccional y su integración con InventarioLista.
/// </summary>
[TestClass]
[DoNotParallelize]
public class AuditoriaServiceTests
{
    // =========================================================
    // AUDITORIASERVICE
    // =========================================================

    [TestMethod]
    public void RegistrarEvento_ListaVacia_CreaPrimerRegistro()
    {
        AuditoriaService auditoria = new AuditoriaService();

        auditoria.RegistrarEvento(
            "INSERT",
            1001,
            "Primer producto.");

        Assert.AreEqual(1, auditoria.TotalRegistros);
        Assert.IsTrue(auditoria.ValidarIntegridad());
    }

    [TestMethod]
    public void RegistrarEvento_MultiplesEventos_MantieneIntegridad()
    {
        AuditoriaService auditoria = CrearAuditoriaDePrueba();

        Assert.AreEqual(3, auditoria.TotalRegistros);
        Assert.IsTrue(auditoria.ValidarIntegridad());
    }

    [TestMethod]
    public void RegistrarEvento_TipoNulo_LanzaArgumentNullException()
    {
        AuditoriaService auditoria = new AuditoriaService();

        Assert.ThrowsExactly<ArgumentNullException>(
            () => auditoria.RegistrarEvento(
                null!,
                1001,
                "Referencia válida"));
    }

    [TestMethod]
    public void RegistrarEvento_ReferenciaNula_LanzaArgumentNullException()
    {
        AuditoriaService auditoria = new AuditoriaService();

        Assert.ThrowsExactly<ArgumentNullException>(
            () => auditoria.RegistrarEvento(
                "INSERT",
                1001,
                null!));
    }

    [TestMethod]
    public void ImprimirHistorial_ListaVacia_MuestraMensaje()
    {
        AuditoriaService auditoria = new AuditoriaService();

        string salida = CapturarSalida(
            auditoria.ImprimirHistorial);

        StringAssert.Contains(
            salida,
            "Bitácora vacía");
    }

    [TestMethod]
    public void ImprimirHistorialInverso_ListaVacia_MuestraMensaje()
    {
        AuditoriaService auditoria = new AuditoriaService();

        string salida = CapturarSalida(
            auditoria.ImprimirHistorialInverso);

        StringAssert.Contains(
            salida,
            "Bitácora vacía");
    }

    [TestMethod]
    public void ImprimirHistorial_RecorridoCronologico_RespetaOrden()
    {
        AuditoriaService auditoria = CrearAuditoriaDePrueba();

        string salida = CapturarSalida(
            auditoria.ImprimirHistorial);

        string operaciones =
            ExtraerOperaciones(salida);

        Assert.AreEqual(
            "INSERT|UPDATE|DELETE",
            operaciones);
    }

    [TestMethod]
    public void ImprimirHistorialInverso_RecorridoInverso_InvierteOrden()
    {
        AuditoriaService auditoria = CrearAuditoriaDePrueba();

        string salida = CapturarSalida(
            auditoria.ImprimirHistorialInverso);

        string operaciones =
            ExtraerOperaciones(salida);

        Assert.AreEqual(
            "DELETE|UPDATE|INSERT",
            operaciones);
    }

    [TestMethod]
    public void ValidarIntegridad_ListaVacia_RetornaTrue()
    {
        AuditoriaService auditoria = new AuditoriaService();

        Assert.AreEqual(0, auditoria.TotalRegistros);
        Assert.IsTrue(auditoria.ValidarIntegridad());
    }

    [TestMethod]
    public void ValidarIntegridad_MultiplesEventos_RetornaTrue()
    {
        AuditoriaService auditoria = CrearAuditoriaDePrueba();

        Assert.IsTrue(auditoria.ValidarIntegridad());
        Assert.AreEqual(3, auditoria.TotalRegistros);
    }

    // =========================================================
    // CONSTRUCTORES E INYECCION DE DEPENDENCIA
    // =========================================================

    [TestMethod]
    public void InventarioLista_AuditoriaNula_LanzaArgumentNullException()
    {
        Assert.ThrowsExactly<ArgumentNullException>(
            () => new InventarioLista(null!));
    }

    [TestMethod]
    public void InventarioLista_ConstructorPredeterminado_IniciaVacio()
    {
        InventarioLista inventario = new InventarioLista();

        Assert.IsTrue(inventario.EstaVacia());
        Assert.AreEqual(0, inventario.Contar());
    }

    // =========================================================
    // INSERTAR INICIO
    // =========================================================

    [TestMethod]
    public void InsertarInicio_ListaVacia_AgregaProductoYAudita()
    {
        AuditoriaService auditoria = new AuditoriaService();
        InventarioLista inventario =
            new InventarioLista(auditoria);

        Producto producto =
            CrearProducto(
                2001,
                "Producto Inicio",
                500.00);

        inventario.InsertarInicio(producto);

        Assert.IsFalse(inventario.EstaVacia());
        Assert.AreEqual(1, inventario.Contar());
        Assert.AreEqual(1, auditoria.TotalRegistros);

        Producto encontrado =
            inventario.BuscarPorSKU(2001);

        Assert.AreEqual(
            "Producto Inicio",
            encontrado.Nombre);
    }

    // =========================================================
    // INSERTAR ORDENADO
    // =========================================================

    [TestMethod]
    public void InsertarOrdenado_ListaVacia_AgregaPrimerNodo()
    {
        AuditoriaService auditoria = new AuditoriaService();
        InventarioLista inventario =
            new InventarioLista(auditoria);

        inventario.InsertarOrdenado(
            CrearProducto(
                2101,
                "Producto Único",
                300.00));

        Assert.AreEqual(1, inventario.Contar());
        Assert.AreEqual(1, auditoria.TotalRegistros);
        Assert.IsTrue(auditoria.ValidarIntegridad());
    }

    [TestMethod]
    public void InsertarOrdenado_PrecioMenor_ColocaProductoAlInicio()
    {
        InventarioLista inventario =
            new InventarioLista();

        inventario.InsertarOrdenado(
            CrearProducto(
                2201,
                "Producto Alto",
                900.00));

        inventario.InsertarOrdenado(
            CrearProducto(
                2202,
                "Producto Bajo",
                100.00));

        string salida =
            CapturarSalida(
                inventario.MostrarTodos);

        int bajo =
            salida.IndexOf(
                "Producto Bajo",
                StringComparison.Ordinal);

        int alto =
            salida.IndexOf(
                "Producto Alto",
                StringComparison.Ordinal);

        Assert.IsGreaterThanOrEqualTo(0, bajo);
        Assert.IsGreaterThanOrEqualTo(0, alto);
        Assert.IsLessThan(alto, bajo);
    }

    [TestMethod]
    public void InsertarOrdenado_TresPrecios_ConservaOrdenAscendente()
    {
        InventarioLista inventario =
            new InventarioLista();

        inventario.InsertarOrdenado(
            CrearProducto(
                2301,
                "Producto Bajo",
                100.00));

        inventario.InsertarOrdenado(
            CrearProducto(
                2302,
                "Producto Alto",
                900.00));

        inventario.InsertarOrdenado(
            CrearProducto(
                2303,
                "Producto Medio",
                500.00));

        string salida =
            CapturarSalida(
                inventario.MostrarTodos);

        int bajo =
            salida.IndexOf(
                "Producto Bajo",
                StringComparison.Ordinal);

        int medio =
            salida.IndexOf(
                "Producto Medio",
                StringComparison.Ordinal);

        int alto =
            salida.IndexOf(
                "Producto Alto",
                StringComparison.Ordinal);

Assert.IsLessThan(medio, bajo);
Assert.IsLessThan(alto, medio);
Assert.AreEqual(3, inventario.Contar());
    }

    [TestMethod]
    public void InsertarOrdenado_PreciosIguales_InsertaAmbosProductos()
    {
        InventarioLista inventario =
            new InventarioLista();

        inventario.InsertarOrdenado(
            CrearProducto(
                2401,
                "Producto A",
                400.00));

        inventario.InsertarOrdenado(
            CrearProducto(
                2402,
                "Producto B",
                400.00));

        Assert.AreEqual(2, inventario.Contar());

        Assert.AreEqual(
            2401,
            inventario.BuscarPorSKU(2401).SKU);

        Assert.AreEqual(
            2402,
            inventario.BuscarPorSKU(2402).SKU);
    }

    // =========================================================
    // CONTAR Y BUSCAR
    // =========================================================

    [TestMethod]
    public void Contar_TresProductos_RetornaTres()
    {
        InventarioLista inventario =
            CrearInventarioConTresProductos();

        Assert.AreEqual(
            3,
            inventario.Contar());
    }

    [TestMethod]
    public void BuscarPorSKU_SKUExistente_RetornaProductoCorrecto()
    {
        InventarioLista inventario =
            CrearInventarioConTresProductos();

        Producto encontrado =
            inventario.BuscarPorSKU(3002);

        Assert.AreEqual(
            3002,
            encontrado.SKU);

        Assert.AreEqual(
            "Producto Medio",
            encontrado.Nombre);
    }

    [TestMethod]
    public void BuscarPorSKU_SKUInexistente_LanzaKeyNotFoundException()
    {
        InventarioLista inventario =
            CrearInventarioConTresProductos();

        Assert.ThrowsExactly<KeyNotFoundException>(
            () => inventario.BuscarPorSKU(9999));
    }

    // =========================================================
    // ACTUALIZAR PRECIO
    // =========================================================

    [TestMethod]
    public void ActualizarPrecio_SKUExistente_ModificaPrecioYAudita()
    {
        AuditoriaService auditoria = new AuditoriaService();
        InventarioLista inventario =
            new InventarioLista(auditoria);

        inventario.InsertarOrdenado(
            CrearProducto(
                4001,
                "Producto Actualizable",
                100.00));

        int eventosAntes =
            auditoria.TotalRegistros;

        bool resultado =
            inventario.ActualizarPrecio(
                4001,
                250.00);

        Producto actualizado =
            inventario.BuscarPorSKU(4001);

        Assert.IsTrue(resultado);
        Assert.AreEqual(
            250.00,
            actualizado.Precio);

        Assert.AreEqual(
            eventosAntes + 1,
            auditoria.TotalRegistros);
    }

    [TestMethod]
    public void ActualizarPrecio_SKUInexistente_RetornaFalseSinAuditar()
    {
        AuditoriaService auditoria = new AuditoriaService();
        InventarioLista inventario =
            new InventarioLista(auditoria);

        inventario.InsertarOrdenado(
            CrearProducto(
                4101,
                "Producto Existente",
                100.00));

        int eventosAntes =
            auditoria.TotalRegistros;

        bool resultado =
            inventario.ActualizarPrecio(
                9999,
                500.00);

        Assert.IsFalse(resultado);

        Assert.AreEqual(
            eventosAntes,
            auditoria.TotalRegistros);
    }

    // =========================================================
    // ELIMINACION
    // =========================================================

    [TestMethod]
    public void EliminarPorSKU_ListaVacia_RetornaFalse()
    {
        AuditoriaService auditoria = new AuditoriaService();
        InventarioLista inventario =
            new InventarioLista(auditoria);

        bool resultado =
            inventario.EliminarPorSKU(5001);

        Assert.IsFalse(resultado);
        Assert.AreEqual(0, auditoria.TotalRegistros);
    }

    [TestMethod]
    public void EliminarPorSKU_Cabeza_EliminaProductoYAudita()
    {
        AuditoriaService auditoria = new AuditoriaService();
        InventarioLista inventario =
            new InventarioLista(auditoria);

        inventario.InsertarOrdenado(
            CrearProducto(
                5101,
                "Producto Cabeza",
                100.00));

        int eventosAntes =
            auditoria.TotalRegistros;

        bool resultado =
            inventario.EliminarPorSKU(5101);

        Assert.IsTrue(resultado);
        Assert.IsTrue(inventario.EstaVacia());

        Assert.AreEqual(
            eventosAntes + 1,
            auditoria.TotalRegistros);
    }

    [TestMethod]
    public void EliminarPorSKU_NodoIntermedio_PreservaCadena()
    {
        AuditoriaService auditoria = new AuditoriaService();
        InventarioLista inventario =
            CrearInventarioConTresProductos(auditoria);

        int eventosAntes =
            auditoria.TotalRegistros;

        bool resultado =
            inventario.EliminarPorSKU(3002);

        Assert.IsTrue(resultado);
        Assert.AreEqual(2, inventario.Contar());

        Assert.ThrowsExactly<KeyNotFoundException>(
            () => inventario.BuscarPorSKU(3002));

        Assert.AreEqual(
            eventosAntes + 1,
            auditoria.TotalRegistros);
    }

    [TestMethod]
    public void EliminarPorSKU_UltimoNodo_EliminaProducto()
    {
        AuditoriaService auditoria = new AuditoriaService();
        InventarioLista inventario =
            CrearInventarioConTresProductos(auditoria);

        bool resultado =
            inventario.EliminarPorSKU(3003);

        Assert.IsTrue(resultado);
        Assert.AreEqual(2, inventario.Contar());

        Assert.ThrowsExactly<KeyNotFoundException>(
            () => inventario.BuscarPorSKU(3003));
    }

    [TestMethod]
    public void EliminarPorSKU_SKUInexistente_RetornaFalseSinAuditar()
    {
        AuditoriaService auditoria = new AuditoriaService();
        InventarioLista inventario =
            CrearInventarioConTresProductos(auditoria);

        int eventosAntes =
            auditoria.TotalRegistros;

        bool resultado =
            inventario.EliminarPorSKU(9999);

        Assert.IsFalse(resultado);

        Assert.AreEqual(
            eventosAntes,
            auditoria.TotalRegistros);

        Assert.AreEqual(
            3,
            inventario.Contar());
    }

    // =========================================================
    // MOSTRAR TODOS
    // =========================================================

    [TestMethod]
    public void MostrarTodos_ListaVacia_MuestraMensaje()
    {
        InventarioLista inventario =
            new InventarioLista();

        string salida =
            CapturarSalida(
                inventario.MostrarTodos);

        StringAssert.Contains(
            salida,
            "inventario está vacío");
    }

    [TestMethod]
    public void MostrarTodos_ConProductos_MuestraDatosEsperados()
    {
        InventarioLista inventario =
            CrearInventarioConTresProductos();

        string salida =
            CapturarSalida(
                inventario.MostrarTodos);

        StringAssert.Contains(
            salida,
            "SKU: 3001");

        StringAssert.Contains(
            salida,
            "Producto Bajo");

        StringAssert.Contains(
            salida,
            "Producto Medio");

        StringAssert.Contains(
            salida,
            "Producto Alto");

        StringAssert.Contains(
            salida,
            "Proveedor Test");
    }

    // =========================================================
    // INTEGRACION INVENTARIO + AUDITORIA
    // =========================================================

    [TestMethod]
    public void InventarioLista_SecuenciaCompleta_GeneraCuatroEventos()
    {
        AuditoriaService auditoria =
            new AuditoriaService();

        InventarioLista inventario =
            new InventarioLista(auditoria);

        inventario.InsertarOrdenado(
            CrearProducto(
                6001,
                "Producto Uno",
                100.00));

        inventario.InsertarOrdenado(
            CrearProducto(
                6002,
                "Producto Dos",
                200.00));

        bool actualizado =
            inventario.ActualizarPrecio(
                6001,
                150.00);

        bool eliminado =
            inventario.EliminarPorSKU(
                6002);

        Assert.IsTrue(actualizado);
        Assert.IsTrue(eliminado);

        Assert.AreEqual(
            4,
            auditoria.TotalRegistros);

        Assert.AreEqual(
            1,
            inventario.Contar());

        Assert.IsTrue(
            auditoria.ValidarIntegridad());
    }

    // =========================================================
    // HELPERS
    // =========================================================

    private static AuditoriaService CrearAuditoriaDePrueba()
    {
        AuditoriaService auditoria =
            new AuditoriaService();

        auditoria.RegistrarEvento(
            "INSERT",
            1001,
            "Producto agregado.");

        auditoria.RegistrarEvento(
            "UPDATE",
            1001,
            "Producto actualizado.");

        auditoria.RegistrarEvento(
            "DELETE",
            1001,
            "Producto eliminado.");

        return auditoria;
    }

    private static InventarioLista CrearInventarioConTresProductos(
        AuditoriaService? auditoria = null)
    {
        AuditoriaService servicio =
            auditoria ?? new AuditoriaService();

        InventarioLista inventario =
            new InventarioLista(servicio);

        inventario.InsertarOrdenado(
            CrearProducto(
                3001,
                "Producto Bajo",
                100.00));

        inventario.InsertarOrdenado(
            CrearProducto(
                3002,
                "Producto Medio",
                200.00));

        inventario.InsertarOrdenado(
            CrearProducto(
                3003,
                "Producto Alto",
                300.00));

        return inventario;
    }

    private static Producto CrearProducto(
        int sku,
        string nombre,
        double precio)
    {
        Proveedor proveedor =
            new Proveedor
            {
                IdProveedor = 1,
                NombreCorporativo = "Proveedor Test"
            };

        return new Producto
        {
            SKU = sku,
            Nombre = nombre,
            Precio = precio,
            Stock = 10,
            DatosProveedor = proveedor
        };
    }

    private static string CapturarSalida(
        Action accion)
    {
        StringWriter salida =
            new StringWriter();

        TextWriter salidaOriginal =
            Console.Out;

        try
        {
            Console.SetOut(salida);
            accion();
        }
        finally
        {
            Console.SetOut(salidaOriginal);
        }

        return salida.ToString();
    }

    private static string ExtraerOperaciones(
        string salida)
    {
        string[] lineas =
            salida.Split(
                Environment.NewLine,
                StringSplitOptions.RemoveEmptyEntries);

        StringBuilder operaciones =
            new StringBuilder();

        foreach (string linea in lineas)
        {
            const string prefijo =
                "Operación : ";

            if (!linea.StartsWith(
                    prefijo,
                    StringComparison.Ordinal))
            {
                continue;
            }

            if (operaciones.Length > 0)
            {
                operaciones.Append('|');
            }

            operaciones.Append(
                linea[prefijo.Length..].Trim());
        }

        return operaciones.ToString();
    }
}