using System;
using System.Text;
using System.Threading;

// ==========================================================
// PRÁCTICA 6
// Composición de Cámaras Virtuales mediante Structs Anidados
// Alumno: José Paulo Santana Ramírez
// Materia: Estructura de Datos
// Ciclo: 26-3
// ==========================================================

/// <summary>
/// Representa una ubicación tridimensional dentro de la escena.
/// </summary>
public struct Posicion
{
    public float x;
    public float y;
    public float z;
}

/// <summary>
/// Representa el punto tridimensional hacia el cual apunta la cámara.
/// </summary>
public struct Foco
{
    public float x;
    public float y;
    public float z;
}

/// <summary>
/// Agrupa el estado completo de un rig de cámara cinematográfica.
/// </summary>
public struct CamaraCinematica
{
    public string nombre;
    public Posicion pos;
    public Foco foco;
    public float fov;
    public float velocidad;
}

internal class Program
{
    private static void Main(string[] args)
    {
        _ = args;

        Console.OutputEncoding = Encoding.UTF8;

        // ----------------------------------------------------------
        // Inicialización del rig principal
        // ----------------------------------------------------------
        CamaraCinematica camara = new CamaraCinematica
        {
            nombre = "CAM_PRINCIPAL",

            pos = new Posicion
            {
                x = 10f,
                y = 5f,
                z = -8f
            },

            foco = new Foco
            {
                x = 0f,
                y = 0f,
                z = 0f
            },

            fov = 60f,
            velocidad = 0.08f
        };

        // ----------------------------------------------------------
        // Objetivos cinematográficos
        // ----------------------------------------------------------
        Posicion posicionObjetivo = new Posicion
        {
            x = 0f,
            y = 2f,
            z = -5f
        };

        Foco focoObjetivo = new Foco
        {
            x = 0f,
            y = 1f,
            z = 0f
        };

        // ----------------------------------------------------------
        // Presentación del estado inicial
        // ----------------------------------------------------------
        Console.WriteLine(
            "======================================================");

        Console.WriteLine(
            "  PRÁCTICA 6 - SISTEMA DE CÁMARA CINEMATOGRÁFICA");

        Console.WriteLine(
            "======================================================");

        Console.WriteLine();

        Console.WriteLine($"Rig creado: {camara.nombre}");
        Console.WriteLine($"FOV: {camara.fov:F2} grados");

        Console.WriteLine(
            $"Velocidad de interpolación: {camara.velocidad:P0}");

        Console.WriteLine();

        Console.WriteLine(
            $"Posición inicial: " +
            $"({camara.pos.x:F2}, " +
            $"{camara.pos.y:F2}, " +
            $"{camara.pos.z:F2})");

        Console.WriteLine(
            $"Foco inicial: " +
            $"({camara.foco.x:F2}, " +
            $"{camara.foco.y:F2}, " +
            $"{camara.foco.z:F2})");

        Console.WriteLine();

        Console.WriteLine(
            $"Posición objetivo: " +
            $"({posicionObjetivo.x:F2}, " +
            $"{posicionObjetivo.y:F2}, " +
            $"{posicionObjetivo.z:F2})");

        Console.WriteLine(
            $"Foco objetivo: " +
            $"({focoObjetivo.x:F2}, " +
            $"{focoObjetivo.y:F2}, " +
            $"{focoObjetivo.z:F2})");

        // ----------------------------------------------------------
        // Bucle de simulación cinematográfica
        // ----------------------------------------------------------
        Console.WriteLine();
        Console.WriteLine("=== SIMULACIÓN DE 20 FRAMES ===");
        Console.WriteLine();

        for (int frame = 1; frame <= 20; frame++)
        {
            ActualizarCamara(
                ref camara,
                posicionObjetivo,
                focoObjetivo);

            ImprimirEstado(camara, frame);

            // Pausa visual para representar el avance cuadro a cuadro.
            Thread.Sleep(80);
        }

        Console.WriteLine();
        Console.WriteLine("¡Simulación completada!");
        Console.WriteLine();

        Console.WriteLine("Estado final aproximado:");

        Console.WriteLine(
            $"POS({camara.pos.x:F2}, " +
            $"{camara.pos.y:F2}, " +
            $"{camara.pos.z:F2})");

        Console.WriteLine(
            $"FOCO({camara.foco.x:F2}, " +
            $"{camara.foco.y:F2}, " +
            $"{camara.foco.z:F2})");

        // ----------------------------------------------------------
        // Extensión: segundo rig cinematográfico
        // ----------------------------------------------------------
        CamaraCinematica camaraCloseUp = new CamaraCinematica
        {
            nombre = "CAM_CLOSEUP",

            pos = new Posicion
            {
                x = 1f,
                y = 1.8f,
                z = -1.5f
            },

            foco = new Foco
            {
                x = 0f,
                y = 1.7f,
                z = 0f
            },

            fov = 35f,
            velocidad = 0.15f
        };

        Console.WriteLine();
        Console.WriteLine(
            "======================================================");

        Console.WriteLine(
            "  EXTENSIÓN: SEGUNDO RIG Y CORTE INSTANTÁNEO");

        Console.WriteLine(
            "======================================================");

        Console.WriteLine();

        Console.WriteLine(
            $"Rig fuente preparado: {camaraCloseUp.nombre}");

        Console.WriteLine(
            $"Velocidad del segundo rig: " +
            $"{camaraCloseUp.velocidad:P0}");

        Console.WriteLine(
            $"POS({camaraCloseUp.pos.x:F2}, " +
            $"{camaraCloseUp.pos.y:F2}, " +
            $"{camaraCloseUp.pos.z:F2})");

        Console.WriteLine(
            $"FOCO({camaraCloseUp.foco.x:F2}, " +
            $"{camaraCloseUp.foco.y:F2}, " +
            $"{camaraCloseUp.foco.z:F2})");

        Console.WriteLine(
            $"FOV del rig fuente: " +
            $"{camaraCloseUp.fov:F2} grados");

        // ----------------------------------------------------------
        // Estado de la cámara antes del corte
        // ----------------------------------------------------------
        Console.WriteLine();
        Console.WriteLine(
            "Estado de CAM_PRINCIPAL antes del corte:");

        ImprimirEstado(camara, 0);

        Console.WriteLine(
            $"FOV antes del corte: {camara.fov:F2} grados");

        // ----------------------------------------------------------
        // Corte cinematográfico instantáneo
        // ----------------------------------------------------------
        CortarA(
            ref camara,
            camaraCloseUp);

        // ----------------------------------------------------------
        // Estado de la cámara después del corte
        // ----------------------------------------------------------
        Console.WriteLine();
        Console.WriteLine(
            "Estado de CAM_PRINCIPAL después del corte:");

        ImprimirEstado(camara, 0);

        Console.WriteLine(
            $"FOV después del corte: {camara.fov:F2} grados");

        Console.WriteLine();
        Console.WriteLine(
            "Extensión cinematográfica completada.");
    }

    /// <summary>
    /// Modifica directamente el rig original y lo desplaza
    /// progresivamente hacia la posición y el foco objetivo.
    /// </summary>
    private static void ActualizarCamara(
        ref CamaraCinematica cam,
        Posicion posicionObjetivo,
        Foco focoObjetivo)
    {
        // Factor alpha de interpolación.
        float alpha = cam.velocidad;

        // Interpolación manual de la posición.
        cam.pos.x +=
            (posicionObjetivo.x - cam.pos.x) * alpha;

        cam.pos.y +=
            (posicionObjetivo.y - cam.pos.y) * alpha;

        cam.pos.z +=
            (posicionObjetivo.z - cam.pos.z) * alpha;

        // Interpolación manual del foco.
        cam.foco.x +=
            (focoObjetivo.x - cam.foco.x) * alpha;

        cam.foco.y +=
            (focoObjetivo.y - cam.foco.y) * alpha;

        cam.foco.z +=
            (focoObjetivo.z - cam.foco.z) * alpha;
    }

    /// <summary>
    /// Imprime el estado actual del rig en un frame específico.
    /// </summary>
    private static void ImprimirEstado(
        CamaraCinematica cam,
        int frame)
    {
        Console.WriteLine(
            $"[Frame {frame:D3}] {cam.nombre} | " +
            $"POS({cam.pos.x:F2}, " +
            $"{cam.pos.y:F2}, " +
            $"{cam.pos.z:F2}) | " +
            $"FOCO({cam.foco.x:F2}, " +
            $"{cam.foco.y:F2}, " +
            $"{cam.foco.z:F2})");
    }

    /// <summary>
    /// Realiza un corte instantáneo copiando la posición,
    /// el foco y el campo de visión del rig fuente al destino.
    /// </summary>
    private static void CortarA(
        ref CamaraCinematica destino,
        CamaraCinematica fuente)
    {
        destino.pos = fuente.pos;
        destino.foco = fuente.foco;
        destino.fov = fuente.fov;

        Console.WriteLine();
        Console.WriteLine(
            $"Corte instantáneo aplicado: " +
            $"{fuente.nombre} -> {destino.nombre}");
    }
}