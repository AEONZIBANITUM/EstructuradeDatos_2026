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
/// Representa el punto tridimensional hacia el que apunta la cámara.
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
        Console.OutputEncoding = Encoding.UTF8;

        // ----------------------------------------------------------
        // Inicialización del rig principal
        // ----------------------------------------------------------
        CamaraCinematica camara;

        camara.nombre = "CAM_PRINCIPAL";
        camara.fov = 60f;
        camara.velocidad = 0.08f;

        // Posición inicial: elevada y desplazada hacia la derecha.
        camara.pos.x = 10f;
        camara.pos.y = 5f;
        camara.pos.z = -8f;

        // Foco inicial: origen de la escena.
        camara.foco.x = 0f;
        camara.foco.y = 0f;
        camara.foco.z = 0f;

        // ----------------------------------------------------------
        // Objetivos cinematográficos
        // ----------------------------------------------------------
        Posicion posicionObjetivo;

        posicionObjetivo.x = 0f;
        posicionObjetivo.y = 2f;
        posicionObjetivo.z = -5f;

        Foco focoObjetivo;

        focoObjetivo.x = 0f;
        focoObjetivo.y = 1f;
        focoObjetivo.z = 0f;

        // ----------------------------------------------------------
        // Presentación del estado inicial
        // ----------------------------------------------------------
        Console.WriteLine("======================================================");
        Console.WriteLine("  PRÁCTICA 6 - SISTEMA DE CÁMARA CINEMATOGRÁFICA");
        Console.WriteLine("======================================================");
        Console.WriteLine();

        Console.WriteLine($"Rig creado: {camara.nombre}");
        Console.WriteLine($"FOV: {camara.fov:F2} grados");
        Console.WriteLine($"Velocidad de interpolación: {camara.velocidad:P0}");
        Console.WriteLine();

        Console.WriteLine(
            $"Posición inicial: " +
            $"({camara.pos.x:F2}, {camara.pos.y:F2}, {camara.pos.z:F2})");

        Console.WriteLine(
            $"Foco inicial: " +
            $"({camara.foco.x:F2}, {camara.foco.y:F2}, {camara.foco.z:F2})");

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
    }

    /// <summary>
    /// Modifica directamente el rig original y lo desplaza suavemente
    /// hacia la posición y el foco objetivo.
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
            $"POS({cam.pos.x:F2}, {cam.pos.y:F2}, {cam.pos.z:F2}) | " +
            $"FOCO({cam.foco.x:F2}, {cam.foco.y:F2}, {cam.foco.z:F2})");
    }
}