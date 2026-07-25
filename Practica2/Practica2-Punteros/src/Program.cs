int total = 4;

Calculadora.Sumar(ref total, 6);

Console.WriteLine(total);

int[] datos = { 3, 8, 1, 7 };

Calculadora.AnalizarValores(
    datos,
    out double prom,
    out int max);

Console.WriteLine($"Prom:{prom} Max:{max}");