using System;

class SimuladorHeap
{
    // MAIN: punto de entrada, vive en el Stack
    static void Main(string[] args)
    {
        Console.Write("¿Cuántos elementos? ");
        int n = int.Parse(Console.ReadLine()!);

        // La REFERENCIA 'arreglo' vive en el Stack
        // El OBJETO arreglo vive en el Heap
        string[] arreglo = InicializarArreglo(n);

        Console.WriteLine("\n--- Arreglo Inicial ---");
        MostrarArreglo(arreglo);

        // Pasamos la referencia a la función modificadora
        ModificarArreglo(arreglo);

        Console.WriteLine("\n--- Arreglo Modificado ---");
        MostrarArreglo(arreglo);
    }

    // Crea y retorna una nueva referencia al Heap
    static string[] InicializarArreglo(int n)
    {
        string[] temp = new string[n];

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Elemento [{i}]: ");
            temp[i] = Console.ReadLine()!;
        }

        return temp; // retorna la referencia del Heap
    }

    // Recibe la referencia: trabaja sobre
    // el MISMO objeto en el Heap
    static void ModificarArreglo(string[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            // Los strings son inmutables: se crea
            // un nuevo objeto string en el Heap
            // y se actualiza la referencia en arr[i]
            arr[i] = arr[i].ToUpper() + $" [MOD-{i}]";
        }

        // No necesitamos retornar nada:
        // los cambios ya están en el Heap
    }

    // Solo lee la referencia, no modifica
    static void MostrarArreglo(string[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Console.WriteLine($" [{i}] = {arr[i]}");
        }
    }
}
