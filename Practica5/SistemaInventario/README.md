# Práctica 5 — Sistema de Gestión de Inventario Básico

Aplicación de consola desarrollada en **C# con .NET 8** para administrar un inventario de productos mediante una estructura personalizada, un arreglo estático y un menú interactivo.

El proyecto permite registrar, consultar, buscar y actualizar productos, además de guardar y recuperar la información mediante un archivo CSV.

---

## Datos del estudiante

| Campo | Información |
|---|---|
| Nombre | Jose Paulo Santana Ramirez |
| Matrícula | 14868430 |
| Materia | Estructura de Datos |
| Ciclo | 26-3 |
| Práctica | Práctica 5 |
| Proyecto | SistemaInventario |
| Tecnología | C# y .NET 8 |

---

## Objetivo

Desarrollar un sistema básico de gestión de inventario que permita aplicar de forma práctica los siguientes conceptos:

- Modelado de datos mediante `struct`.
- Arreglos estáticos de estructuras.
- Control lógico de posiciones ocupadas.
- Ciclos `do-while`.
- Sentencias `switch`.
- Métodos modulares y reutilizables.
- Parámetros enviados mediante `ref`.
- Validación robusta de entradas.
- Búsqueda lineal dentro de arreglos.
- Modificación directa de estructuras almacenadas.
- Persistencia de información mediante archivos CSV.
- Depuración con breakpoints, `Variables`, `Watch` y `Call Stack`.
- Control de versiones con Git y commits semánticos.

---

## Ubicación del proyecto

```text
Practica5/SistemaInventario
```

---

## Estructura del proyecto

```text
SistemaInventario/
├── capturas/
├── InventarioCsv.cs
├── Producto.cs
├── Program.cs
├── inventario.csv
├── README.md
└── SistemaInventario.csproj
```

En la raíz del repositorio también se incorporó:

```text
.vscode/
└── launch.json
```

El archivo `launch.json` configura la depuración del proyecto mediante la terminal integrada de Visual Studio Code.

Las carpetas:

```text
bin/
obj/
```

se generan automáticamente durante la compilación y están excluidas del repositorio mediante `.gitignore`.

---

# Modelado de datos

## Estructura `Producto`

La entidad principal fue modelada mediante un `struct` llamado `Producto`.

```csharp
public struct Producto
{
    public int ID;
    public string Nombre;
    public double Precio;
    public int Stock;
}
```

Cada producto almacena:

| Campo | Tipo | Descripción |
|---|---|---|
| `ID` | `int` | Identificador único del producto |
| `Nombre` | `string` | Nombre descriptivo |
| `Precio` | `double` | Precio unitario |
| `Stock` | `int` | Cantidad disponible |

El `struct Producto` se encuentra declarado fuera de la clase `Program`, dentro del mismo espacio de nombres.

---

## Constructor del producto

La estructura incluye un constructor que permite inicializar todos sus campos:

```csharp
new Producto(id, nombre, precio, stock);
```

Esto evita crear productos parcialmente configurados y mantiene agrupados sus datos principales.

---

# Arreglo estático

El inventario utiliza un arreglo con capacidad fija:

```csharp
private const int CapacidadMaxima = 10;

Producto[] inventario =
    new Producto[CapacidadMaxima];
```

La capacidad física del arreglo es de diez posiciones.

El sistema utiliza una variable adicional:

```csharp
int totalRegistros = 0;
```

Esta variable funciona como límite lógico y determina cuántas posiciones contienen información válida.

Por ejemplo:

```text
Capacidad física:     10 posiciones
Productos ocupados:    3 posiciones
Posiciones válidas:    0, 1 y 2
Siguiente posición:    3
```

Los recorridos solamente procesan las posiciones comprendidas entre:

```text
0 y totalRegistros - 1
```

De esta manera se evita mostrar o procesar posiciones vacías.

---

# Menú interactivo

El programa permanece activo mediante un ciclo:

```csharp
do
{
    // Mostrar menú y ejecutar una opción.
}
while (opcion != 7);
```

La selección se procesa mediante una sentencia `switch`.

```text
==================================================
       SISTEMA DE GESTIÓN DE INVENTARIO
==================================================
Productos registrados: 0/10
--------------------------------------------------
1. Registrar producto
2. Mostrar inventario
3. Buscar producto por ID
4. Actualizar stock
5. Guardar inventario en archivo CSV
6. Cargar inventario desde archivo CSV
7. Salir
--------------------------------------------------
```

## Opciones implementadas

| Opción | Función |
|---:|---|
| `1` | Registrar un producto |
| `2` | Mostrar el inventario completo |
| `3` | Buscar un producto mediante su ID |
| `4` | Actualizar el stock de un producto |
| `5` | Guardar el inventario en CSV |
| `6` | Cargar el inventario desde CSV |
| `7` | Cerrar la aplicación |

El menú valida que la opción introducida sea un número comprendido entre `1` y `7`.

---

# Funcionalidades implementadas

## 1. Registro de productos

El método de registro recibe:

```csharp
Producto[] inventario
ref int totalRegistros
```

La firma permite modificar el contador original:

```csharp
private static void RegistrarProducto(
    Producto[] inventario,
    ref int totalRegistros)
```

Cada producto se guarda en:

```csharp
inventario[totalRegistros]
```

Después se incrementa el contador:

```csharp
totalRegistros++;
```

El registro incluye:

- Validación de capacidad.
- Validación de ID.
- Prevención de IDs duplicados.
- Validación de nombre.
- Validación de precio.
- Validación de stock.
- Confirmación de la posición utilizada.
- Cálculo de espacios disponibles.

---

## 2. Presentación del inventario

El método `MostrarInventario` recorre únicamente las posiciones ocupadas:

```csharp
for (int i = 0; i < totalRegistros; i++)
{
    Producto producto = inventario[i];
}
```

Los datos se presentan mediante una tabla:

```text
ID       NOMBRE                           PRECIO    STOCK
--------------------------------------------------------------
101      laptop hp                    $15,999.99        8
102      teclado mecanico              $1,299.50       40
103      mouse inalambrico                $599.90       20
--------------------------------------------------------------
Total de productos registrados: 3
```

Los precios se muestran con formato monetario correspondiente a México.

---

## 3. Búsqueda de productos por ID

La búsqueda se realiza mediante un recorrido lineal:

```csharp
private static int BuscarIndicePorId(
    Producto[] inventario,
    int totalRegistros,
    int idBuscado)
```

El método devuelve:

```text
Índice encontrado
```

o:

```text
-1
```

cuando el identificador no existe.

### Producto encontrado

El sistema muestra:

- Posición dentro del arreglo.
- ID.
- Nombre.
- Precio.
- Stock disponible.

### Producto inexistente

El sistema informa:

```text
No se encontró un producto con el ID solicitado.
```

### Inventario vacío

La búsqueda se detiene de forma segura y muestra:

```text
No es posible realizar la búsqueda.
El inventario está vacío.
```

---

## 4. Actualización de stock

El método `ActualizarStock` localiza primero el producto mediante su ID.

Después modifica directamente el campo almacenado en el arreglo:

```csharp
inventario[indiceEncontrado].Stock =
    nuevoStock;
```

La operación conserva intactos:

- ID.
- Nombre.
- Precio.

Solamente cambia:

```text
Stock
```

Ejemplo:

```text
Producto:         Teclado Mecanico
Stock anterior:   15
Stock nuevo:      40
Posición:         1
```

---

# Validaciones implementadas

## ID

El identificador debe:

- Ser numérico.
- Ser entero.
- Ser mayor que cero.
- No existir previamente.

Entradas rechazadas durante las pruebas:

```text
abc
-5
101
```

El ID `101` fue rechazado porque ya pertenecía a otro producto.

---

## Nombre

El nombre:

- No puede quedar vacío.
- No puede contener únicamente espacios.

Después de una entrada inválida, el sistema vuelve a solicitar el dato.

---

## Precio

El precio debe:

- Ser numérico.
- Ser mayor que cero.

Entradas rechazadas:

```text
texto
0
```

---

## Stock

El stock debe:

- Ser un número entero.
- Ser igual o mayor que cero.

Entrada rechazada:

```text
-1
```

Un producto puede tener stock `0`, ya que representa un artículo actualmente agotado.

---

## Capacidad máxima

Antes de registrar un producto se valida:

```csharp
if (totalRegistros >= inventario.Length)
```

Cuando el inventario alcanza:

```text
10/10
```

el sistema impide un registro adicional:

```text
No es posible registrar más productos.
El inventario alcanzó su capacidad máxima.
```

Esta validación evita acceder a una posición inexistente y previene una excepción:

```text
IndexOutOfRangeException
```

---

# Persistencia en archivo CSV

La persistencia fue separada en la clase:

```text
InventarioCsv
```

Archivo:

```text
InventarioCsv.cs
```

## Guardar inventario

La opción `5` escribe las posiciones ocupadas del arreglo en:

```text
inventario.csv
```

Formato utilizado:

```csv
ID;Nombre;Precio;Stock
101;laptop hp;15999.99;8
102;teclado mecanico;1299.5;40
103;mouse inalambrico;599.9;20
```

Los precios se almacenan mediante una cultura invariante para evitar problemas relacionados con separadores decimales.

También se incluyen:

- Encabezado CSV.
- Codificación UTF-8.
- Escape de campos con comillas.
- Control de errores de escritura.
- Visualización de la ruta completa.
- Cantidad de productos guardados.

---

## Cargar inventario

La opción `6`:

1. Comprueba que el archivo exista.
2. Lee las líneas del archivo.
3. Omite el encabezado.
4. Separa los campos.
5. Valida cada registro.
6. Rechaza datos inválidos.
7. Evita IDs duplicados.
8. Respeta la capacidad máxima.
9. Reconstruye los productos.
10. Actualiza `totalRegistros` mediante `ref`.

Firma utilizada:

```csharp
public static void Cargar(
    Producto[] inventario,
    ref int totalRegistros)
```

La carga muestra:

```text
Inventario cargado correctamente.
Productos cargados: 3
Registros omitidos: 0
Archivo: inventario.csv
```

---

# Manejo de errores de archivos

Las operaciones CSV controlan errores mediante:

```csharp
try
{
    // Escritura o lectura.
}
catch (UnauthorizedAccessException)
{
    // Falta de permisos.
}
catch (IOException excepcion)
{
    // Error de entrada o salida.
}
```

También se comprueba la existencia del archivo:

```csharp
File.Exists(NombreArchivo)
```

Esto evita que la aplicación termine inesperadamente cuando el CSV no está disponible.

---

# Modularización

La lógica fue separada en métodos con responsabilidades específicas.

## Métodos principales de `Program`

```csharp
Main()
MostrarMenu()
RegistrarProducto()
MostrarInventario()
BuscarProductoPorId()
ActualizarStock()
BuscarIndicePorId()
LeerOpcionMenu()
LeerEnteroPositivo()
LeerEnteroNoNegativo()
LeerPrecioPositivo()
LeerTextoNoVacio()
TruncarTexto()
MostrarDespedida()
Pausar()
```

## Métodos de `InventarioCsv`

```csharp
Guardar()
Cargar()
ExisteId()
EscaparCampo()
SepararLineaCsv()
```

Esta separación:

- Reduce duplicación.
- Facilita la lectura.
- Simplifica las pruebas.
- Permite corregir funciones sin alterar todo el programa.
- Mantiene separada la persistencia de la interfaz de consola.

---

# Depuración realizada

La depuración se configuró mediante:

```text
.vscode/launch.json
```

La aplicación se ejecutó en la terminal integrada para permitir el funcionamiento correcto de:

```csharp
Console.Clear();
Console.ReadLine();
Console.WriteLine();
```

Durante las pruebas se utilizaron:

- Breakpoints.
- Panel `Variables`.
- Panel `Watch`.
- `Call Stack`.
- Ejecución paso a paso con `F10`.
- Continuación mediante `F5`.
- Detención mediante `Shift + F5`.

---

## Comprobación del parámetro `ref`

Se colocó un breakpoint en:

```csharp
totalRegistros++;
```

### Antes del incremento

```text
totalRegistros = 0
posicionUtilizada = 0
inventario[posicionUtilizada].ID = 201
inventario[posicionUtilizada].Nombre = "Producto Debug"
inventario[posicionUtilizada].Stock = 7
```

### Después del incremento

```text
totalRegistros = 1
posicionUtilizada = 0
```

Los datos del producto permanecieron almacenados en la posición `0`.

Esto comprobó que el parámetro `ref` modificó el contador original.

---

## Comprobación de actualización directa del `struct`

Se colocó un breakpoint en:

```csharp
inventario[indiceEncontrado].Stock =
    nuevoStock;
```

### Antes de la asignación

```text
idBuscado = 102
indiceEncontrado = 1
stockAnterior = 40
nuevoStock = 55
inventario[indiceEncontrado].Stock = 40
```

### Después de la asignación

```text
idBuscado = 102
indiceEncontrado = 1
stockAnterior = 40
nuevoStock = 55
inventario[indiceEncontrado].Stock = 55
```

La prueba confirmó que la asignación modificó directamente el elemento del arreglo, y no solamente una copia temporal del `struct`.

---

# Compilación y ejecución

## Desde la carpeta del proyecto

```powershell
Set-Location ".\Practica5\SistemaInventario"
dotnet restore
dotnet build
dotnet run
```

## Desde la raíz del repositorio

```powershell
dotnet restore ".\Practica5\SistemaInventario\SistemaInventario.csproj"
dotnet build ".\Practica5\SistemaInventario\SistemaInventario.csproj"
dotnet run --project ".\Practica5\SistemaInventario\SistemaInventario.csproj"
```

## Resultado esperado de la compilación

```text
Compilación realizada correctamente
0 Advertencia(s)
0 Error(es)
```

---

# Pruebas realizadas

| Prueba | Resultado |
|---|---|
| Compilación del proyecto | Correcta |
| Menú interactivo | Correcto |
| Opción inválida | Rechazada |
| Registro de producto | Correcto |
| ID no numérico | Rechazado |
| ID negativo | Rechazado |
| ID duplicado | Rechazado |
| Nombre vacío | Rechazado |
| Precio no numérico | Rechazado |
| Precio igual a cero | Rechazado |
| Stock negativo | Rechazado |
| Listado tabular | Correcto |
| Búsqueda de ID existente | Correcta |
| Búsqueda de ID inexistente | Controlada |
| Búsqueda con inventario vacío | Controlada |
| Actualización de stock | Correcta |
| Inventario con 10 productos | Correcto |
| Registro número 11 | Rechazado |
| Guardado CSV | Correcto |
| Carga CSV en nueva ejecución | Correcta |
| Parámetro `ref` mediante debugger | Comprobado |
| Modificación directa del `Stock` | Comprobada |
| Estado final de Git | Limpio |

---

# Evidencias

Las capturas se encuentran en:

```text
Practica5/SistemaInventario/capturas
```

## Lista completa

| Número | Evidencia |
|---:|---|
| 01 | [Estado inicial del repositorio](capturas/01_estado_inicial_repositorio.png) |
| 02 | [Creación de rama y scaffold](capturas/02_rama_y_scaffold_practica5.png) |
| 02-2 | [Commit inicial de la práctica](capturas/02-2_commit_inicial_practica5.png) |
| 03 | [Struct Producto y arreglo](capturas/03_struct_producto_y_arreglo.png) |
| 04 | [Menú con do-while y switch](capturas/04_menu_interactivo_do_while_switch.png) |
| 04-2 | [Segunda evidencia del menú](capturas/04-2_menu_interactivo_do_while_switch.png) |
| 04-3 | [Tercera evidencia del menú](capturas/04-3_menu_interactivo_do_while_switch.png) |
| 05 | [Registro y listado de productos](capturas/05_registro_y_listado_productos.png) |
| 06 | [Búsqueda de producto encontrado](capturas/06_busqueda_producto_encontrado.png) |
| 06-2 | [Búsqueda de producto no encontrado](capturas/06-2_busqueda_producto_no_encontrado.png) |
| 07 | [Actualización correcta de stock](capturas/07_actualizacion_stock_correcta.png) |
| 08 | [Guardado del inventario en CSV](capturas/08_guardado_inventario_csv.png) |
| 08-2 | [Carga CSV en nueva ejecución](capturas/08-2_carga_csv_nueva_ejecucion.png) |
| 08-3 | [Verificación complementaria de la carga](capturas/08-3_carga_csv_nueva_ejecucion.png) |
| 08-C | [Evidencia adicional de carga CSV](capturas/08-carga_csv_nueva_ejecucion.png) |
| 09 | [Validación de ID y duplicados](capturas/09_validacion_id_y_duplicados.png) |
| 09-2 | [Validación de nombre, precio y stock](capturas/09-2_validacion_nombre_precio_stock.png) |
| 09-3 | [Inventario después de las validaciones](capturas/09-3_inventario_tras_validaciones.png) |
| 10 | [Inventario completo 10 de 10](capturas/10_inventario_completo_10_de_10.png) |
| 10-2 | [Rechazo por inventario lleno](capturas/10-2_rechazo_por_inventario_lleno.png) |
| 11 | [Debugger antes del incremento por ref](capturas/11_debug_ref_antes_incremento.png) |
| 11-2 | [Debugger después del incremento por ref](capturas/11-2_debug_ref_despues_incremento.png) |
| 12 | [Debugger antes de actualizar stock](capturas/12_debug_stock_antes_asignacion.png) |
| 12-2 | [Debugger después de actualizar stock](capturas/12-2_debug_stock_despues_asignacion.png) |

---

# Flujo de Git

## Rama de desarrollo

```text
feature/sistema-inventario
```

## Commits registrados

```text
ac7c4a2 chore: inicializar proyecto SistemaInventario
e69dfac feat: modelar productos con struct y arreglo
b012ea7 feat: implementar menu interactivo del inventario
cec0513 feat: implementar registro y listado de productos
0a7adbc feat: agregar busqueda de productos por id
e245450 feat: implementar actualizacion de stock
7c279dc feat: implementar persistencia de inventario en csv
1f3b664 chore: agregar configuracion de depuracion en vscode
```

El desarrollo se organizó mediante commits atómicos para conservar un historial claro y trazable.

---

# Comandos principales utilizados

## Creación de la rama

```powershell
git switch -c feature/sistema-inventario
```

## Creación del proyecto

```powershell
dotnet new console `
    -n SistemaInventario `
    -o ".\Practica5\SistemaInventario" `
    --framework net8.0
```

## Compilación

```powershell
dotnet build
```

## Ejecución

```powershell
dotnet run
```

## Revisión de cambios

```powershell
git status --short
git diff --cached --stat
git log --oneline
```

## Registro de cambios

```powershell
git add <archivo>
git commit -m "tipo: descripción"
```

---

# Estado final de la práctica

- [x] Proyecto creado con .NET 8.
- [x] Rama de desarrollo independiente.
- [x] `struct Producto` implementado.
- [x] Arreglo estático con capacidad para diez productos.
- [x] Contador lógico de registros.
- [x] Menú implementado con `do-while`.
- [x] Opciones procesadas mediante `switch`.
- [x] Registro de productos.
- [x] Prevención de IDs duplicados.
- [x] Listado tabular.
- [x] Búsqueda lineal por ID.
- [x] Actualización directa de stock.
- [x] Validaciones robustas.
- [x] Control del inventario lleno.
- [x] Persistencia mediante CSV.
- [x] Recuperación de datos en una nueva ejecución.
- [x] Manejo de errores de lectura y escritura.
- [x] Configuración de depuración en VS Code.
- [x] Parámetro `ref` comprobado con debugger.
- [x] Modificación de un `struct` comprobada con debugger.
- [x] Evidencias organizadas.
- [x] Ocho commits semánticos registrados.
- [x] Compilación sin errores.
- [x] Repositorio sin cambios pendientes.

---

# Reflexión final

Esta práctica permitió comprender cómo una estructura personalizada puede utilizarse como unidad de almacenamiento dentro de un arreglo estático.

El uso de `Producto` como `struct` facilitó agrupar en una sola entidad los datos de identificación, nombre, precio y stock. El arreglo permitió reservar una capacidad conocida, mientras que `totalRegistros` funcionó como límite lógico para distinguir las posiciones ocupadas de las posiciones disponibles.

El parámetro `ref` permitió modificar desde un método el contador declarado originalmente en `Main`. Esta modificación fue comprobada mediante el debugger, observando el cambio de `totalRegistros` de `0` a `1`.

También se comprobó que los `struct` tienen semántica de valor. La asignación de un elemento del arreglo a una variable local produce una copia; por ello, para conservar una actualización, fue necesario modificar directamente:

```csharp
inventario[indiceEncontrado].Stock
```

Las validaciones impidieron almacenar identificadores inválidos o duplicados, nombres vacíos, precios incorrectos y cantidades negativas. La comprobación del inventario lleno evitó exceder la capacidad física del arreglo.

La incorporación de persistencia CSV permitió conservar los productos después de cerrar el programa y recuperarlos durante una nueva ejecución. La separación de esta responsabilidad en `InventarioCsv.cs` mantuvo organizado el código y facilitó el manejo de errores.

Finalmente, el uso de Git permitió dividir el desarrollo en etapas pequeñas, verificables y documentadas mediante commits semánticos.

---

# Uso de inteligencia artificial como apoyo didáctico

Durante el desarrollo se utilizó ChatGPT como herramienta de apoyo para:

- Analizar las instrucciones de la práctica.
- Organizar el procedimiento.
- Explicar conceptos relacionados con `struct`, arreglos y parámetros `ref`.
- Revisar la estructura de los comandos.
- Diagnosticar errores de compilación.
- Configurar la depuración en Visual Studio Code.
- Diseñar pruebas de validación.
- Verificar el cumplimiento de los requisitos.
- Organizar las evidencias.
- Preparar la documentación técnica.

El código fue escrito, ejecutado, probado y verificado directamente por el estudiante en Visual Studio Code.

La inteligencia artificial se utilizó como apoyo didáctico y no como sustitución del análisis, la ejecución ni la comprobación del trabajo realizado.

---

# Autoría

El proyecto constituye un desarrollo académico propio, construido y comprobado durante la realización de la Práctica 5 de la materia Estructura de Datos.

Las herramientas de asistencia utilizadas fueron documentadas con transparencia conforme a los principios de integridad académica.