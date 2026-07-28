using System.Globalization;
using System.Text;

namespace SistemaInventario;

/// <summary>
/// Administra el guardado y la carga del inventario mediante un archivo CSV.
/// </summary>
internal static class InventarioCsv
{
    /// <summary>
    /// Nombre del archivo CSV utilizado por la aplicación.
    /// </summary>
    public const string NombreArchivo = "inventario.csv";

    /// <summary>
    /// Guarda las posiciones ocupadas del inventario en un archivo CSV.
    /// </summary>
    public static void Guardar(
        Producto[] inventario,
        int totalRegistros)
    {
        Console.WriteLine(
            "==================================================");

        Console.WriteLine(
            "          GUARDAR INVENTARIO EN CSV");

        Console.WriteLine(
            "==================================================");

        Console.WriteLine();

        if (totalRegistros == 0)
        {
            Console.WriteLine(
                "No hay productos disponibles para guardar.");

            Console.WriteLine(
                "Primero registra al menos un producto.");

            return;
        }

        try
        {
            using StreamWriter escritor = new(
                NombreArchivo,
                false,
                new UTF8Encoding(false));

            // Encabezado del archivo CSV.
            escritor.WriteLine(
                "ID;Nombre;Precio;Stock");

            for (int i = 0; i < totalRegistros; i++)
            {
                Producto producto = inventario[i];

                string nombreSeguro =
                    EscaparCampo(producto.Nombre);

                string precioSeguro =
                    producto.Precio.ToString(
                        CultureInfo.InvariantCulture);

                escritor.WriteLine(
                    $"{producto.ID};" +
                    $"{nombreSeguro};" +
                    $"{precioSeguro};" +
                    $"{producto.Stock}");
            }

            Console.WriteLine(
                "Inventario guardado correctamente.");

            Console.WriteLine(
                "--------------------------------------------------");

            Console.WriteLine(
                $"Productos guardados: {totalRegistros}");

            Console.WriteLine(
                $"Archivo: {NombreArchivo}");

            Console.WriteLine(
                $"Ruta: {Path.GetFullPath(NombreArchivo)}");

            Console.WriteLine(
                "--------------------------------------------------");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine(
                "No se cuenta con permiso para escribir el archivo.");
        }
        catch (IOException excepcion)
        {
            Console.WriteLine(
                "Ocurrió un error al escribir el archivo CSV.");

            Console.WriteLine(
                $"Detalle técnico: {excepcion.Message}");
        }
    }

    /// <summary>
    /// Carga productos desde el archivo CSV y reemplaza el inventario actual.
    /// El parámetro ref permite actualizar el contador original.
    /// </summary>
    public static void Cargar(
        Producto[] inventario,
        ref int totalRegistros)
    {
        Console.WriteLine(
            "==================================================");

        Console.WriteLine(
            "          CARGAR INVENTARIO DESDE CSV");

        Console.WriteLine(
            "==================================================");

        Console.WriteLine();

        if (!File.Exists(NombreArchivo))
        {
            Console.WriteLine(
                $"No se encontró el archivo {NombreArchivo}.");

            Console.WriteLine(
                "Guarda primero el inventario para poder recuperarlo.");

            return;
        }

        try
        {
            string[] lineas = File.ReadAllLines(
                NombreArchivo,
                Encoding.UTF8);

            if (lineas.Length <= 1)
            {
                Console.WriteLine(
                    "El archivo no contiene productos válidos.");

                return;
            }

            // Se limpia el arreglo antes de cargar los datos del archivo.
            Array.Clear(
                inventario,
                0,
                inventario.Length);

            totalRegistros = 0;

            int registrosOmitidos = 0;
            bool capacidadAlcanzada = false;

            // Se comienza en 1 porque la posición 0 contiene el encabezado.
            for (int i = 1; i < lineas.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lineas[i]))
                {
                    continue;
                }

                if (totalRegistros >= inventario.Length)
                {
                    capacidadAlcanzada = true;
                    break;
                }

                List<string> campos =
                    SepararLineaCsv(lineas[i]);

                if (campos.Count != 4)
                {
                    registrosOmitidos++;
                    continue;
                }

                bool idCorrecto = int.TryParse(
                    campos[0],
                    out int id);

                string nombre =
                    campos[1].Trim();

                bool precioCorrecto = double.TryParse(
                    campos[2],
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out double precio);

                bool stockCorrecto = int.TryParse(
                    campos[3],
                    out int stock);

                bool datosValidos =
                    idCorrecto &&
                    id > 0 &&
                    !string.IsNullOrWhiteSpace(nombre) &&
                    precioCorrecto &&
                    precio > 0 &&
                    stockCorrecto &&
                    stock >= 0;

                if (!datosValidos)
                {
                    registrosOmitidos++;
                    continue;
                }

                if (ExisteId(
                    inventario,
                    totalRegistros,
                    id))
                {
                    registrosOmitidos++;
                    continue;
                }

                inventario[totalRegistros] =
                    new Producto(
                        id,
                        nombre,
                        precio,
                        stock);

                totalRegistros++;
            }

            Console.WriteLine(
                "Inventario cargado correctamente.");

            Console.WriteLine(
                "--------------------------------------------------");

            Console.WriteLine(
                $"Productos cargados: {totalRegistros}");

            Console.WriteLine(
                $"Registros omitidos: {registrosOmitidos}");

            Console.WriteLine(
                $"Archivo: {NombreArchivo}");

            if (capacidadAlcanzada)
            {
                Console.WriteLine();

                Console.WriteLine(
                    "Advertencia: el archivo contiene más productos");

                Console.WriteLine(
                    "que la capacidad permitida por el arreglo.");
            }

            Console.WriteLine(
                "--------------------------------------------------");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine(
                "No se cuenta con permiso para leer el archivo.");
        }
        catch (IOException excepcion)
        {
            Console.WriteLine(
                "Ocurrió un error al leer el archivo CSV.");

            Console.WriteLine(
                $"Detalle técnico: {excepcion.Message}");
        }
    }

    /// <summary>
    /// Comprueba si un identificador ya existe en el arreglo.
    /// </summary>
    private static bool ExisteId(
        Producto[] inventario,
        int totalRegistros,
        int idBuscado)
    {
        for (int i = 0; i < totalRegistros; i++)
        {
            if (inventario[i].ID == idBuscado)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Prepara un campo CSV que contiene separadores, comillas o saltos.
    /// </summary>
    private static string EscaparCampo(
        string valor)
    {
        bool necesitaComillas =
            valor.Contains(';') ||
            valor.Contains('"') ||
            valor.Contains('\n') ||
            valor.Contains('\r');

        if (!necesitaComillas)
        {
            return valor;
        }

        string valorEscapado =
            valor.Replace(
                "\"",
                "\"\"");

        return $"\"{valorEscapado}\"";
    }

    /// <summary>
    /// Separa una línea CSV respetando los campos encerrados entre comillas.
    /// </summary>
    private static List<string> SepararLineaCsv(
        string linea)
    {
        List<string> campos = new();
        StringBuilder campoActual = new();

        bool dentroDeComillas = false;

        for (int i = 0; i < linea.Length; i++)
        {
            char caracter = linea[i];

            if (caracter == '"')
            {
                bool comillaEscapada =
                    dentroDeComillas &&
                    i + 1 < linea.Length &&
                    linea[i + 1] == '"';

                if (comillaEscapada)
                {
                    campoActual.Append('"');
                    i++;
                }
                else
                {
                    dentroDeComillas =
                        !dentroDeComillas;
                }
            }
            else if (
                caracter == ';' &&
                !dentroDeComillas)
            {
                campos.Add(
                    campoActual.ToString());

                campoActual.Clear();
            }
            else
            {
                campoActual.Append(caracter);
            }
        }

        campos.Add(
            campoActual.ToString());

        return campos;
    }
}