using ProyectoFinal_FastCart.Models;

namespace ProyectoFinal_FastCart.Services;

public static class OrdenamientoService
{
    public static void ShellSort(Producto[] catalogo)
    {
        int n = catalogo.Length;

        // Secuencia de Knuth: 1, 4, 13, 40, 121...
        int gap = 1;

        while (gap < n / 3)
        {
            gap = gap * 3 + 1;
        }

        while (gap >= 1)
        {
            // Insertion Sort utilizando la brecha actual.
            for (int i = gap; i < n; i++)
            {
                Producto temporal = catalogo[i];
                int j = i;

                // Precio DESC y SKU ASC como desempate.
                while (j >= gap &&
                       DebeIrDespues(catalogo[j - gap], temporal))
                {
                    catalogo[j] = catalogo[j - gap];
                    j -= gap;
                }

                catalogo[j] = temporal;
            }

            // Reducir la brecha siguiendo la secuencia de Knuth.
            gap /= 3;
        }
    }

    private static bool DebeIrDespues(Producto a, Producto b)
    {
        // Criterio principal: Precio descendente.
        if (a.Precio != b.Precio)
        {
            return a.Precio < b.Precio;
        }

        // Criterio secundario: SKU ascendente.
        return a.SKU > b.SKU;
    }

    public static bool EstaCorrectamenteOrdenado(Producto[] catalogo)
    {
        for (int i = 1; i < catalogo.Length; i++)
        {
            Producto anterior = catalogo[i - 1];
            Producto actual = catalogo[i];

            if (anterior.Precio < actual.Precio)
            {
                return false;
            }

            if (anterior.Precio == actual.Precio &&
                anterior.SKU > actual.SKU)
            {
                return false;
            }
        }

        return true;
    }
}