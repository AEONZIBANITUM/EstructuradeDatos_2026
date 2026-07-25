int total = 4;

Calculadora.Sumar(ref total, 6);

Console.WriteLine(total);

int[] datos = { 3, 8, 1, 7 };

Calculadora.AnalizarValores(
    datos,
    out double prom,
    out int max);

Console.WriteLine($"Prom:{prom} Max:{max}");

int primero = 5;
int segundo = 9;

Calculadora.Intercambiar(ref primero, ref segundo);

Console.WriteLine(
    $"Intercambio: primero={primero} segundo={segundo}");

    bool divisionValida = Calculadora.IntentarDividir(
    17,
    5,
    out int cociente,
    out int residuo);

Console.WriteLine(
    $"División válida: {divisionValida} cociente={cociente} residuo={residuo}");