# FastCart Backend Core

## Proyecto Final — Estructura de Datos

### Fase 1: Módulo de Inteligencia de Precios

FastCart Backend Core es el proyecto integrador desarrollado para la materia de **Estructura de Datos**, correspondiente al ciclo 26-3.

La **Fase 1** establece la base técnica del sistema mediante el modelado de productos y proveedores, la generación de un catálogo reproducible, la implementación manual del algoritmo **ShellSort** y la medición de su rendimiento mediante `System.Diagnostics.Stopwatch`.

El objetivo principal de esta etapa es construir una solución clara, modular y preparada para evolucionar posteriormente hacia estructuras dinámicas como listas enlazadas, listas dobles, pilas y colas.

---

## Objetivo de la Fase 1

La primera fase de FastCart tiene como propósito desarrollar el núcleo inicial del catálogo de productos y comprobar su comportamiento mediante pruebas reproducibles.

Los principales objetivos implementados son:

- Representar productos y proveedores mediante estructuras compuestas.
- Utilizar `struct` para los modelos base de la aplicación.
- Generar un catálogo de productos mediante un arreglo nativo `Producto[]`.
- Mantener SKU únicos a partir del valor `1001`.
- Generar precios dentro de un rango controlado.
- Generar cantidades de stock entre `0` y `500`.
- Utilizar datos reproducibles mediante una semilla fija.
- Implementar manualmente el algoritmo ShellSort.
- Utilizar la secuencia de gaps de Knuth.
- Ordenar los productos por precio descendente.
- Utilizar SKU ascendente como criterio de desempate.
- Validar automáticamente el resultado completo del ordenamiento.
- Medir el rendimiento mediante `Stopwatch`.
- Ejecutar las pruebas finales en modo `Release`.
- Mantener un historial Git incremental y trazable.

---

## Estructura del proyecto

La Fase 1 se organizó separando los modelos de datos, los servicios y el punto de entrada principal de la aplicación.

```text
ProyectoFinal_FastCart/
│
├── Models/
│   ├── Producto.cs
│   └── Proveedor.cs
│
├── Services/
│   ├── CatalogoService.cs
│   └── OrdenamientoService.cs
│
├── Program.cs
├── ProyectoFinal_FastCart.csproj
└── README.md
```

### Models

Contiene las estructuras que representan los datos principales del catálogo.

### Services

Contiene la lógica encargada de generar productos, mostrar información y ejecutar el algoritmo de ordenamiento.

### Program.cs

Coordina la demostración completa de la Fase 1:

- generación del catálogo;
- visualización previa;
- ejecución de ShellSort;
- validación;
- prueba de desempate;
- medición de rendimiento.

---

# Arquitectura de modelos de datos

## Proveedor

El proveedor se representa mediante un `struct` denominado `Proveedor`.

```csharp
public struct Proveedor
{
    public int IdProveedor;
    public string NombreCorporativo;
}
```

La estructura contiene:

- `IdProveedor`: identificador numérico del proveedor.
- `NombreCorporativo`: nombre utilizado para identificar a la empresa proveedora.

---

## Producto

Cada producto del catálogo se representa mediante el `struct` `Producto`.

```csharp
public struct Producto
{
    public int SKU;
    public string Nombre;
    public double Precio;
    public int Stock;
    public Proveedor DatosProveedor;
}
```

Los campos utilizados son:

- `SKU`: identificador único del producto.
- `Nombre`: descripción legible del artículo.
- `Precio`: valor utilizado como criterio principal del ordenamiento.
- `Stock`: cantidad disponible del producto.
- `DatosProveedor`: estructura `Proveedor` asociada al artículo.

La inclusión de `Proveedor` dentro de `Producto` permite trabajar con una **estructura compuesta**, manteniendo reunida la información esencial de cada elemento del catálogo.

---

# Value Types y modelo de memoria

Los modelos `Producto` y `Proveedor` fueron desarrollados como `struct`, es decir, como **tipos por valor** en C#.

Esta decisión sigue el enfoque académico planteado para la Fase 1, donde se busca estudiar la diferencia entre:

- tipos por valor;
- tipos por referencia;
- Stack;
- Heap;
- Garbage Collector;
- localidad de memoria;
- costo de copia.

En el caso de un arreglo de estructuras, los elementos se almacenan como valores dentro del bloque correspondiente al arreglo, lo que favorece un acceso predecible durante operaciones repetitivas de comparación y desplazamiento.

También debe considerarse que los `struct` poseen semántica de copia por valor, por lo que su utilización resulta especialmente conveniente cuando los modelos mantienen un tamaño controlado y no requieren identidad compartida mediante referencias.

---

# Generación del catálogo de prueba

La clase estática:

```text
CatalogoService
```

es responsable de generar los datos utilizados durante las pruebas.

El catálogo se construye mediante:

```csharp
Producto[]
```

sin utilizar `List<T>` ni otras colecciones genéricas para almacenar los productos de esta fase.

---

## Características de los datos generados

La generación cumple con las siguientes condiciones:

- SKU únicos.
- Numeración de SKU iniciando en `1001`.
- Precio mínimo aproximado de `$10.00`.
- Precio máximo aproximado de `$9,999.99`.
- Stock entre `0` y `500`.
- Diversos nombres de producto.
- Diversos proveedores.
- Semilla pseudoaleatoria fija.
- Datos reproducibles entre ejecuciones.

La semilla utilizada es:

```csharp
new Random(2603);
```

Esto permite que cada ejecución genere exactamente el mismo conjunto de productos y facilita:

- las pruebas;
- la depuración;
- la comparación entre ejecuciones;
- la validación del algoritmo;
- la documentación de evidencias.

---

# Caso especial para prueba de desempate

Los primeros tres productos generados reciben deliberadamente el mismo precio:

```text
$1499.99
```

Los SKU correspondientes son:

```text
1001
1002
1003
```

Este caso fue agregado específicamente para comprobar el criterio secundario de ordenamiento.

Después de ejecutar ShellSort, los tres productos deben conservar el siguiente orden:

```text
SKU 1001
SKU 1002
SKU 1003
```

De esta manera puede verificarse visualmente que, cuando existen precios iguales, el sistema utiliza **SKU ascendente** como criterio de desempate.

---

# Algoritmo ShellSort

El motor de ordenamiento se encuentra implementado manualmente dentro de:

```text
OrdenamientoService.cs
```

La clase es estática porque no conserva estado entre ejecuciones y únicamente opera sobre el arreglo recibido.

El método principal es:

```csharp
public static void ShellSort(Producto[] catalogo)
```

---

## Secuencia de gaps

La implementación utiliza la secuencia de Knuth:

```text
1, 4, 13, 40, 121, ...
```

generada mediante:

```text
h = 3h + 1
```

El algoritmo comienza utilizando brechas relativamente grandes y posteriormente las reduce hasta llegar a:

```text
gap = 1
```

La última etapa funciona de manera equivalente a una pasada de Insertion Sort sobre un arreglo que ya se encuentra parcialmente ordenado.

---

# Criterios de ordenamiento

La prioridad utilizada es:

```text
Precio DESC → SKU ASC
```

## Primer criterio: Precio descendente

Los productos de mayor precio deben aparecer primero.

Ejemplo:

```text
$9996.95
$9995.17
$9983.03
$9930.16
$9911.60
```

Esto corresponde a un orden de mayor a menor.

---

## Segundo criterio: SKU ascendente

Cuando dos o más productos poseen exactamente el mismo precio, se utiliza el SKU como desempate.

Ejemplo:

```text
Precio: $1499.99
SKU 1001
SKU 1002
SKU 1003
```

La comparación secundaria hace que el SKU numéricamente menor aparezca primero.

---

# Validación automática del ordenamiento

Además de visualizar los primeros elementos del catálogo después de ShellSort, se desarrolló el método:

```csharp
EstaCorrectamenteOrdenado(Producto[] catalogo)
```

Este método recorre todo el arreglo y verifica que cada pareja de productos consecutivos cumpla las siguientes reglas:

```text
Precio anterior >= Precio actual
```

y, cuando ambos precios son iguales:

```text
SKU anterior <= SKU actual
```

Si alguna condición falla, la validación devuelve:

```text
false
```

Si los 500 productos cumplen correctamente las reglas, devuelve:

```text
true
```

La salida observada durante las pruebas fue:

```text
Resultado: CORRECTO - Precio DESC / SKU ASC
```

---

# Prueba específica de desempate

También se desarrolló el método:

```csharp
MostrarPorPrecio(Producto[] catalogo, double precio)
```

Este método permite localizar visualmente productos con un precio determinado sin utilizar LINQ.

Durante la prueba se utilizó:

```text
$1499.99
```

y se obtuvo:

```text
SKU: 1001 | Precio: $1499.99
SKU: 1002 | Precio: $1499.99
SKU: 1003 | Precio: $1499.99
```

Esto demuestra directamente el funcionamiento del criterio secundario **SKU ASC**.

---

# Complejidad de ShellSort

La complejidad de ShellSort depende de la secuencia de gaps utilizada.

De acuerdo con el material correspondiente a la Fase 1, se considera de manera aproximada:

- Mejor caso: `O(n log n)`
- Caso promedio: entre `O(n^1.25)` y `O(n^1.5)`
- Peor caso: puede degradarse hasta `O(n²)`
- Espacio auxiliar: `O(1)`
- Estabilidad: no estable

ShellSort trabaja **in-place**, por lo que no requiere crear otro arreglo completo para realizar el ordenamiento.

Aunque ShellSort no es un algoritmo estable, el criterio adicional por SKU permite producir un resultado determinístico para los productos con el mismo precio.

---

# Restricciones técnicas

La Fase 1 fue desarrollada sin utilizar mecanismos automáticos que sustituyeran la implementación manual del algoritmo.

Durante la auditoría final se verificó la ausencia de:

```text
System.Linq
OrderBy
Array.Sort()
.Sort()
List<T>
Queue<T>
Stack<T>
LinkedList<T>
```

La comprobación fue realizada directamente sobre el repositorio mediante comandos `git grep`.

Ejemplos:

```powershell
git grep -n "System.Linq" -- ProyectoFinal_FastCart
git grep -n "OrderBy" -- ProyectoFinal_FastCart
git grep -n "Array.Sort" -- ProyectoFinal_FastCart
git grep -n "\.Sort(" -- ProyectoFinal_FastCart
git grep -n "List<" -- ProyectoFinal_FastCart
git grep -n "Queue<" -- ProyectoFinal_FastCart
git grep -n "Stack<" -- ProyectoFinal_FastCart
git grep -n "LinkedList<" -- ProyectoFinal_FastCart
```

Las búsquedas no devolvieron resultados.

Por lo tanto, ShellSort fue desarrollado manualmente y no depende de algoritmos de ordenamiento proporcionados por el framework.

---

# Pruebas realizadas

La implementación fue probada de manera incremental.

## Prueba inicial del modelo

Se verificó:

- creación de `Producto`;
- creación de `Proveedor`;
- composición de ambas estructuras;
- acceso a `DatosProveedor`;
- compilación correcta.

---

## Prueba con 50 productos

El primer catálogo funcional fue generado con:

```text
50 productos
```

Esto permitió verificar:

- SKU;
- nombres;
- precios;
- stock;
- proveedores;
- reproducibilidad;
- precios repetidos.

---

## Prueba de ShellSort

Posteriormente se ejecutó el algoritmo sobre el catálogo y se compararon:

```text
CATÁLOGO ANTES DEL ORDENAMIENTO
```

y:

```text
CATÁLOGO DESPUÉS DEL ORDENAMIENTO
```

Los resultados mostraron correctamente los precios en orden descendente.

---

## Prueba de desempate

Se utilizaron tres productos con:

```text
Precio = $1499.99
```

y se comprobó que el resultado fuera:

```text
1001
1002
1003
```

---

## Validación completa

El método `EstaCorrectamenteOrdenado()` comprobó todos los elementos del catálogo y reportó:

```text
Resultado: CORRECTO - Precio DESC / SKU ASC
```

---

# Medición de rendimiento

La medición del algoritmo fue realizada mediante:

```csharp
System.Diagnostics.Stopwatch
```

La medición se limita exclusivamente a la ejecución de ShellSort.

El flujo utilizado fue:

```text
Generación del catálogo
        ↓
Warm-up
        ↓
Generación del catálogo real
        ↓
Stopwatch.Start()
        ↓
ShellSort
        ↓
Stopwatch.Stop()
        ↓
Resultados
```

---

# Warm-up del JIT

Antes del benchmark real se ejecuta ShellSort sobre un catálogo independiente.

Esto permite reducir el impacto de la compilación Just-In-Time en la medición principal.

El catálogo utilizado durante el warm-up no es el mismo arreglo utilizado durante el benchmark.

De esta forma se evita medir ShellSort sobre datos previamente ordenados.

---

# Configuración del benchmark

Las pruebas finales fueron realizadas con:

```text
Target Framework: .NET 8
Configuración: Release
Cantidad: 500 productos
Medición: System.Diagnostics.Stopwatch
```

El proyecto fue ejecutado mediante:

```powershell
dotnet build -c Release
```

y:

```powershell
dotnet run -c Release
```

---

# Resultados de rendimiento

Se realizaron múltiples ejecuciones reales.

## Ejecución 1

```text
Productos procesados: 500
Tiempo: 0 ms
Tiempo: 89.50 µs
Ticks: 895
```

## Ejecución 2

```text
Productos procesados: 500
Tiempo: 0 ms
Tiempo: 105.60 µs
Ticks: 1056
```

Tabla comparativa:

| Ejecución | Productos | Milisegundos | Microsegundos | Ticks |
|---|---:|---:|---:|---:|
| 1 | 500 | 0 ms | 89.50 µs | 895 |
| 2 | 500 | 0 ms | 105.60 µs | 1056 |

El valor de `0 ms` es esperado debido a que el algoritmo completa el procesamiento de 500 productos en menos de un milisegundo.

Por esta razón también se registraron:

- microsegundos;
- ticks.

Estos valores permiten observar diferencias que no serían visibles utilizando únicamente milisegundos.

La pequeña variación entre ejecuciones corresponde al comportamiento normal de un benchmark realizado en un entorno real.

---

# Salida de consola

La aplicación muestra durante la ejecución:

```text
FASTCART BACKEND CORE - FASE 1
```

seguido de:

```text
Modo de compilación: Release
Total de productos: 500
```

Después presenta:

```text
CATÁLOGO ANTES DEL ORDENAMIENTO
```

los primeros cinco elementos originales.

Posteriormente:

```text
Ejecutando ShellSort...
Criterio: Precio DESC -> SKU ASC
```

y después:

```text
CATÁLOGO DESPUÉS DEL ORDENAMIENTO
```

Finalmente se presentan:

```text
VALIDACIÓN DEL ORDENAMIENTO
PRUEBA DE DESEMPATE POR SKU
MÉTRICAS DE RENDIMIENTO - SHELLSORT
```

---

# Compilación

El proyecto puede compilarse desde la carpeta principal mediante:

```powershell
dotnet build
```

Para las pruebas finales se utiliza:

```powershell
dotnet build -c Release
```

La compilación final de auditoría terminó correctamente.

---

# Ejecución

Para ejecutar la versión optimizada:

```powershell
dotnet run -c Release
```

---

# Control de versiones

El proyecto forma parte del repositorio:

```text
EstructuradeDatos_2026
```

La Fase 1 fue desarrollada en la rama:

```text
proyecto/fase1-ordenamiento
```

La rama fue creada a partir de `main` después de comprobar que el repositorio se encontraba actualizado y limpio.

---

# Historial incremental

El desarrollo fue dividido en commits independientes.

## Modelos y catálogo

```text
feat: add FastCart base models and catalog generator
```

Incluye:

- estructura inicial;
- `Producto`;
- `Proveedor`;
- `CatalogoService`;
- generación reproducible de datos.

---

## Motor de ordenamiento

```text
feat: implement ShellSort with price and SKU ordering
```

Incluye:

- `OrdenamientoService`;
- ShellSort manual;
- secuencia de Knuth;
- Precio DESC;
- SKU ASC;
- validación automática;
- prueba del desempate.

---

## Benchmark

```text
perf: add ShellSort Stopwatch benchmark
```

Incluye:

- Stopwatch;
- warm-up;
- ejecución con 500 productos;
- milisegundos;
- microsegundos;
- ticks;
- modo Release.

---

# Evidencias recopiladas

Durante el desarrollo de la Fase 1 se recopilaron evidencias cronológicas.

## Evidencia 01

**Creación y verificación de la rama de Fase 1**

Demuestra:

- actualización de `main`;
- creación de `proyecto/fase1-ordenamiento`;
- rama correcta;
- working tree limpio.

---

## Evidencia 02

**Creación y validación inicial del FastCart Backend Core**

Demuestra:

- creación del proyecto;
- restauración;
- compilación;
- ejecución inicial.

---

## Evidencia 03

**Implementación y validación de los modelos Producto y Proveedor**

Demuestra:

- estructuras `Producto`;
- estructura `Proveedor`;
- modelo compuesto;
- acceso al proveedor;
- compilación correcta.

---

## Evidencia 04

**Generación del catálogo de 50 productos de prueba**

Demuestra:

- 50 productos;
- SKU desde 1001;
- precios;
- stock;
- proveedores;
- reproducibilidad;
- tres precios idénticos.

---

## Evidencia 05

**Implementación y validación del algoritmo ShellSort**

Demuestra:

- `OrdenamientoService`;
- ejecución de ShellSort;
- Precio DESC;
- SKU ASC;
- validación correcta.

---

## Evidencia 06

**Validación del criterio secundario SKU ascendente**

Demuestra:

```text
1001 → $1499.99
1002 → $1499.99
1003 → $1499.99
```

y confirma el desempate ascendente por SKU.

---

## Evidencia 07

**Medición y validación de rendimiento mediante Stopwatch**

Incluye:

- catálogo antes;
- catálogo después;
- validación;
- ejecución en Release;
- 500 productos;
- microsegundos;
- ticks;
- múltiples ejecuciones.

---

## Evidencia 08

**Auditoría técnica, compilación final e historial de commits**

Demuestra:

- ausencia de LINQ;
- ausencia de `OrderBy`;
- ausencia de `Array.Sort()`;
- ausencia de `.Sort()`;
- ausencia de colecciones genéricas prohibidas;
- compilación Release correcta;
- historial incremental Git;
- sincronización con el repositorio remoto.

---

# Estado actual de la Fase 1

Hasta este punto se encuentran completados:

- [x] Proyecto FastCart Backend Core.
- [x] Rama exclusiva para Fase 1.
- [x] Modelo `Proveedor`.
- [x] Modelo `Producto`.
- [x] Estructuras compuestas.
- [x] Catálogo reproducible.
- [x] SKU únicos.
- [x] Precios controlados.
- [x] Stock controlado.
- [x] Caso de empate intencional.
- [x] ShellSort manual.
- [x] Secuencia de Knuth.
- [x] Precio descendente.
- [x] SKU ascendente.
- [x] Validación automática del ordenamiento.
- [x] Prueba específica de desempate.
- [x] Benchmark mediante Stopwatch.
- [x] Warm-up previo.
- [x] Ejecución con 500 productos.
- [x] Compilación Release.
- [x] Auditoría de restricciones.
- [x] Historial Git incremental.
- [x] Evidencias técnicas recopiladas.
- [ ] Pull Request final de la Fase 1.

---

# Flujo Git de entrega

La rama correspondiente es:

```text
proyecto/fase1-ordenamiento
```

El Pull Request deberá dirigirse hacia:

```text
main
```

con el título:

```text
[Fase 1] Módulo de Ordenamiento ShellSort
```

La rama no deberá fusionarse inmediatamente, ya que el Pull Request debe permanecer disponible para revisión.

---

# Conclusión de la Fase 1

La primera fase de FastCart establece una base funcional para el resto del Proyecto Final.

Se construyó un modelo de catálogo mediante estructuras `Producto` y `Proveedor`, se generó un conjunto de datos reproducible y se implementó manualmente un algoritmo ShellSort capaz de ordenar los productos mediante una regla compuesta:

```text
Precio DESC → SKU ASC
```

La implementación fue validada tanto de forma visual como automática sobre el arreglo completo.

También se comprobó específicamente el funcionamiento del desempate mediante tres productos con el mismo precio, obteniendo correctamente el orden ascendente por SKU.

El comportamiento del algoritmo fue medido utilizando `System.Diagnostics.Stopwatch` sobre 500 productos y en modo `Release`, registrando tiempos inferiores a un milisegundo y complementando las mediciones con microsegundos y ticks.

Finalmente, se realizó una auditoría técnica para confirmar que la solución no depende de LINQ, `Array.Sort()`, `.Sort()` ni de colecciones genéricas que sustituyan la implementación manual requerida.

Con esta base terminada, el proyecto queda preparado para continuar con las siguientes fases, donde el catálogo evolucionará hacia estructuras de datos dinámicas y nuevos módulos de gestión.