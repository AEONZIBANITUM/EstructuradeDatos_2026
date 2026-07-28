using System.Globalization;
using System.Text;

namespace SistemaInventario;

internal static class Program
{
    /// <summary>
    /// Número máximo de productos permitidos en el arreglo estático.
    /// </summary>
    private const int CapacidadMaxima = 10;

    private static void Main()
    {
        // Configuración regional para mostrar precios en pesos mexicanos.
        CultureInfo.DefaultThreadCurrentCulture =
            CultureInfo.GetCultureInfo("es-MX");

        CultureInfo.DefaultThreadCurrentUICulture =
            CultureInfo.GetCultureInfo("es-MX");

        // Permite mostrar correctamente caracteres especiales y acentos.
        Console.OutputEncoding = Encoding.UTF8;

        // Arreglo estático de estructuras Producto.
        Producto[] inventario = new Producto[CapacidadMaxima];

        // Indica cuántas posiciones del arreglo contienen productos válidos.
        int totalRegistros = 0;

        int opcion;

        Console.Title = "Sistema de Gestión de Inventario";

        do
        {
            Console.Clear();

            MostrarMenu(
                totalRegistros,
                inventario.Length);

            opcion = LeerOpcionMenu();

            Console.Clear();

            switch (opcion)
            {
                case 1:
                    RegistrarProducto(
                        inventario,
                        ref totalRegistros);
                    break;

                case 2:
                    MostrarInventario(
                        inventario,
                        totalRegistros);
                    break;

                case 3:
                    BuscarProductoPorId(
                        inventario,
                        totalRegistros);
                    break;

                case 4:
                    ActualizarStock(
                        inventario,
                        totalRegistros);
                    break;

                case 5:
                    MostrarModuloPendiente(
                        "GUARDAR INVENTARIO EN ARCHIVO CSV");
                    break;

                case 6:
                    MostrarModuloPendiente(
                        "CARGAR INVENTARIO DESDE ARCHIVO CSV");
                    break;

                case 7:
                    MostrarDespedida();
                    break;
            }

            if (opcion != 7)
            {
                Pausar();
            }

        } while (opcion != 7);
    }

    /// <summary>
    /// Muestra el menú principal y el estado actual del inventario.
    /// </summary>
    private static void MostrarMenu(
        int totalRegistros,
        int capacidad)
    {
        Console.WriteLine(
            "==================================================");

        Console.WriteLine(
            "       SISTEMA DE GESTIÓN DE INVENTARIO");

        Console.WriteLine(
            "==================================================");

        Console.WriteLine(
            $"Productos registrados: {totalRegistros}/{capacidad}");

        Console.WriteLine(
            "--------------------------------------------------");

        Console.WriteLine("1. Registrar producto");
        Console.WriteLine("2. Mostrar inventario");
        Console.WriteLine("3. Buscar producto por ID");
        Console.WriteLine("4. Actualizar stock");
        Console.WriteLine("5. Guardar inventario en archivo CSV");
        Console.WriteLine("6. Cargar inventario desde archivo CSV");
        Console.WriteLine("7. Salir");

        Console.WriteLine(
            "--------------------------------------------------");
    }

    /// <summary>
    /// Registra un producto en la siguiente posición disponible.
    /// El parámetro ref permite modificar el contador original.
    /// </summary>
    private static void RegistrarProducto(
        Producto[] inventario,
        ref int totalRegistros)
    {
        Console.WriteLine(
            "==================================================");

        Console.WriteLine(
            "              REGISTRAR PRODUCTO");

        Console.WriteLine(
            "==================================================");

        Console.WriteLine();

        if (totalRegistros >= inventario.Length)
        {
            Console.WriteLine(
                "No es posible registrar más productos.");

            Console.WriteLine(
                "El inventario alcanzó su capacidad máxima.");

            return;
        }

        int id;

        while (true)
        {
            id = LeerEnteroPositivo(
                "ID del producto: ");

            int indiceExistente = BuscarIndicePorId(
                inventario,
                totalRegistros,
                id);

            if (indiceExistente == -1)
            {
                break;
            }

            Console.WriteLine();

            Console.WriteLine(
                $"El ID {id} ya pertenece a otro producto.");

            Console.WriteLine(
                "Escribe un identificador diferente.");

            Console.WriteLine();
        }

        string nombre = LeerTextoNoVacio(
            "Nombre del producto: ");

        double precio = LeerPrecioPositivo(
            "Precio unitario: ");

        int stock = LeerEnteroNoNegativo(
            "Cantidad disponible: ");

        int posicionUtilizada = totalRegistros;

        inventario[posicionUtilizada] = new Producto(
            id,
            nombre,
            precio,
            stock);

        // Se modifica directamente el contador original recibido por ref.
        totalRegistros++;

        Console.WriteLine();

        Console.WriteLine(
            "Producto registrado correctamente.");

        Console.WriteLine(
            "--------------------------------------------------");

        Console.WriteLine(
            $"Posición utilizada:  {posicionUtilizada}");

        Console.WriteLine(
            $"Productos registrados: {totalRegistros}");

        Console.WriteLine(
            $"Espacios disponibles: {inventario.Length - totalRegistros}");

        Console.WriteLine(
            "--------------------------------------------------");
    }

    /// <summary>
    /// Recorre el arreglo y muestra únicamente las posiciones ocupadas.
    /// </summary>
    private static void MostrarInventario(
        Producto[] inventario,
        int totalRegistros)
    {
        Console.WriteLine(
            "==============================================================");

        Console.WriteLine(
            "                    INVENTARIO ACTUAL");

        Console.WriteLine(
            "==============================================================");

        Console.WriteLine();

        if (totalRegistros == 0)
        {
            Console.WriteLine(
                "El inventario está vacío.");

            Console.WriteLine(
                "Primero registra al menos un producto.");

            return;
        }

        Console.WriteLine(
            "{0,-8} {1,-24} {2,14} {3,8}",
            "ID",
            "NOMBRE",
            "PRECIO",
            "STOCK");

        Console.WriteLine(
            "--------------------------------------------------------------");

        for (int i = 0; i < totalRegistros; i++)
        {
            Producto producto = inventario[i];

            string precioFormateado =
                producto.Precio.ToString(
                    "C2",
                    CultureInfo.CurrentCulture);

            Console.WriteLine(
                "{0,-8} {1,-24} {2,14} {3,8}",
                producto.ID,
                TruncarTexto(producto.Nombre, 24),
                precioFormateado,
                producto.Stock);
        }

        Console.WriteLine(
            "--------------------------------------------------------------");

        Console.WriteLine(
            $"Total de productos registrados: {totalRegistros}");
    }

    /// <summary>
    /// Solicita un ID y muestra la información del producto encontrado.
    /// </summary>
    private static void BuscarProductoPorId(
        Producto[] inventario,
        int totalRegistros)
    {
        Console.WriteLine(
            "==================================================");

        Console.WriteLine(
            "           BUSCAR PRODUCTO POR ID");

        Console.WriteLine(
            "==================================================");

        Console.WriteLine();

        if (totalRegistros == 0)
        {
            Console.WriteLine(
                "No es posible realizar la búsqueda.");

            Console.WriteLine(
                "El inventario está vacío.");

            return;
        }

        int idBuscado = LeerEnteroPositivo(
            "Escribe el ID que deseas buscar: ");

        int indiceEncontrado = BuscarIndicePorId(
            inventario,
            totalRegistros,
            idBuscado);

        Console.WriteLine();

        if (indiceEncontrado == -1)
        {
            Console.WriteLine(
                $"No se encontró un producto con el ID {idBuscado}.");

            Console.WriteLine(
                "Verifica el identificador e intenta nuevamente.");

            return;
        }

        Producto productoEncontrado =
            inventario[indiceEncontrado];

        string precioFormateado =
            productoEncontrado.Precio.ToString(
                "C2",
                CultureInfo.CurrentCulture);

        Console.WriteLine(
            "Producto encontrado correctamente.");

        Console.WriteLine(
            "--------------------------------------------------");

        Console.WriteLine(
            $"Posición en el arreglo: {indiceEncontrado}");

        Console.WriteLine(
            $"ID:                     {productoEncontrado.ID}");

        Console.WriteLine(
            $"Nombre:                 {productoEncontrado.Nombre}");

        Console.WriteLine(
            $"Precio:                 {precioFormateado}");

        Console.WriteLine(
            $"Stock disponible:       {productoEncontrado.Stock}");

        Console.WriteLine(
            "--------------------------------------------------");
    }

    /// <summary>
    /// Busca un producto mediante su ID y modifica únicamente su stock.
    /// </summary>
    private static void ActualizarStock(
        Producto[] inventario,
        int totalRegistros)
    {
        Console.WriteLine(
            "==================================================");

        Console.WriteLine(
            "               ACTUALIZAR STOCK");

        Console.WriteLine(
            "==================================================");

        Console.WriteLine();

        if (totalRegistros == 0)
        {
            Console.WriteLine(
                "No es posible actualizar productos.");

            Console.WriteLine(
                "El inventario está vacío.");

            return;
        }

        int idBuscado = LeerEnteroPositivo(
            "Escribe el ID del producto: ");

        int indiceEncontrado = BuscarIndicePorId(
            inventario,
            totalRegistros,
            idBuscado);

        Console.WriteLine();

        if (indiceEncontrado == -1)
        {
            Console.WriteLine(
                $"No se encontró un producto con el ID {idBuscado}.");

            Console.WriteLine(
                "No se realizó ninguna modificación.");

            return;
        }

        Producto productoActual =
            inventario[indiceEncontrado];

        string precioFormateado =
            productoActual.Precio.ToString(
                "C2",
                CultureInfo.CurrentCulture);

        Console.WriteLine(
            "Producto localizado correctamente.");

        Console.WriteLine(
            "--------------------------------------------------");

        Console.WriteLine(
            $"Posición:         {indiceEncontrado}");

        Console.WriteLine(
            $"ID:               {productoActual.ID}");

        Console.WriteLine(
            $"Nombre:           {productoActual.Nombre}");

        Console.WriteLine(
            $"Precio:           {precioFormateado}");

        Console.WriteLine(
            $"Stock actual:     {productoActual.Stock}");

        Console.WriteLine(
            "--------------------------------------------------");

        Console.WriteLine();

        int nuevoStock = LeerEnteroNoNegativo(
            "Escribe el nuevo stock: ");

        int stockAnterior =
            inventario[indiceEncontrado].Stock;

        /*
         * El elemento del arreglo es una variable de tipo Producto.
         * Se modifica directamente el campo Stock del struct almacenado
         * en la posición encontrada.
         */
        inventario[indiceEncontrado].Stock =
            nuevoStock;

        Console.WriteLine();

        Console.WriteLine(
            "Stock actualizado correctamente.");

        Console.WriteLine(
            "--------------------------------------------------");

        Console.WriteLine(
            $"Producto:         {inventario[indiceEncontrado].Nombre}");

        Console.WriteLine(
            $"Stock anterior:   {stockAnterior}");

        Console.WriteLine(
            $"Stock nuevo:      {inventario[indiceEncontrado].Stock}");

        Console.WriteLine(
            $"Posición:         {indiceEncontrado}");

        Console.WriteLine(
            "--------------------------------------------------");
    }

    /// <summary>
    /// Realiza una búsqueda lineal dentro de las posiciones ocupadas.
    /// Devuelve el índice encontrado o -1 cuando el ID no existe.
    /// </summary>
    private static int BuscarIndicePorId(
        Producto[] inventario,
        int totalRegistros,
        int idBuscado)
    {
        for (int i = 0; i < totalRegistros; i++)
        {
            if (inventario[i].ID == idBuscado)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Solicita y valida una opción del menú entre 1 y 7.
    /// </summary>
    private static int LeerOpcionMenu()
    {
        while (true)
        {
            Console.Write(
                "Selecciona una opción: ");

            string? entrada = Console.ReadLine();

            bool conversionCorrecta =
                int.TryParse(
                    entrada,
                    out int opcion);

            if (conversionCorrecta &&
                opcion >= 1 &&
                opcion <= 7)
            {
                return opcion;
            }

            Console.WriteLine();

            Console.WriteLine(
                "Entrada no válida. Escribe un número del 1 al 7.");

            Console.WriteLine();
        }
    }

    /// <summary>
    /// Solicita un número entero mayor que cero.
    /// </summary>
    private static int LeerEnteroPositivo(
        string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);

            string? entrada = Console.ReadLine();

            bool conversionCorrecta =
                int.TryParse(
                    entrada,
                    out int valor);

            if (conversionCorrecta && valor > 0)
            {
                return valor;
            }

            Console.WriteLine(
                "Entrada no válida. " +
                "Escribe un entero mayor que cero.");
        }
    }

    /// <summary>
    /// Solicita un número entero igual o mayor que cero.
    /// </summary>
    private static int LeerEnteroNoNegativo(
        string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);

            string? entrada = Console.ReadLine();

            bool conversionCorrecta =
                int.TryParse(
                    entrada,
                    out int valor);

            if (conversionCorrecta && valor >= 0)
            {
                return valor;
            }

            Console.WriteLine(
                "Entrada no válida. " +
                "Escribe un entero igual o mayor que cero.");
        }
    }

    /// <summary>
    /// Solicita un precio numérico mayor que cero.
    /// </summary>
    private static double LeerPrecioPositivo(
        string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);

            string? entrada = Console.ReadLine();

            bool conversionCorrecta =
                double.TryParse(
                    entrada,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out double precio);

            if (conversionCorrecta && precio > 0)
            {
                return precio;
            }

            Console.WriteLine(
                "Entrada no válida. " +
                "Escribe un precio mayor que cero.");
        }
    }

    /// <summary>
    /// Solicita texto que no esté vacío ni contenga solo espacios.
    /// </summary>
    private static string LeerTextoNoVacio(
        string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);

            string? entrada = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(entrada))
            {
                return entrada.Trim();
            }

            Console.WriteLine(
                "Entrada no válida. " +
                "El texto no puede quedar vacío.");
        }
    }

    /// <summary>
    /// Limita la longitud visual de los nombres dentro de la tabla.
    /// </summary>
    private static string TruncarTexto(
        string texto,
        int longitudMaxima)
    {
        if (texto.Length <= longitudMaxima)
        {
            return texto;
        }

        return texto[..(longitudMaxima - 3)] + "...";
    }

    /// <summary>
    /// Muestra temporalmente una sección todavía no implementada.
    /// </summary>
    private static void MostrarModuloPendiente(
        string titulo)
    {
        Console.WriteLine(
            "==================================================");

        Console.WriteLine(
            $"   {titulo}");

        Console.WriteLine(
            "==================================================");

        Console.WriteLine();

        Console.WriteLine(
            "Módulo preparado. Su lógica será implementada");

        Console.WriteLine(
            "en una etapa posterior de la práctica.");
    }

    /// <summary>
    /// Muestra el mensaje final de la aplicación.
    /// </summary>
    private static void MostrarDespedida()
    {
        Console.WriteLine(
            "==================================================");

        Console.WriteLine(
            "       SISTEMA DE GESTIÓN DE INVENTARIO");

        Console.WriteLine(
            "==================================================");

        Console.WriteLine();

        Console.WriteLine(
            "El sistema se cerró correctamente.");

        Console.WriteLine(
            "Gracias por utilizar la aplicación.");

        Console.WriteLine();
    }

    /// <summary>
    /// Detiene temporalmente la aplicación antes de volver al menú.
    /// </summary>
    private static void Pausar()
    {
        Console.WriteLine();

        Console.WriteLine(
            "Presiona ENTER para volver al menú...");

        Console.ReadLine();
    }
}