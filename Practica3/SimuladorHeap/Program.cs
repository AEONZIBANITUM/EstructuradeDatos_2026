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
}