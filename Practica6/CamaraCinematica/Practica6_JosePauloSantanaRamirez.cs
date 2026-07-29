using System;

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
        // Verificación inicial en consola
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

        Console.WriteLine();
        Console.WriteLine("Estructuras anidadas inicializadas correctamente.");
    }
}