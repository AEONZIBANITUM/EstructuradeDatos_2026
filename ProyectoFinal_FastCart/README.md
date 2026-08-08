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



---

# Fase 2 — Arquitectura Dinámica del Catálogo Maestro

## Migración de Arreglo a Lista Simplemente Enlazada

La Fase 2 transforma el catálogo de FastCart desde el arreglo nativo utilizado
durante la Fase 1 hacia una **Lista Simplemente Enlazada implementada
manualmente en C#**.

La finalidad principal de esta evolución es permitir que el inventario pueda
crecer y reducirse dinámicamente durante la ejecución sin depender de una
capacidad fija previamente establecida.

La nueva estructura se compone de:

```text
InventarioLista
      │
      └── _cabeza
             ↓
       NodoProducto
             ↓
       NodoProducto
             ↓
       NodoProducto
             ↓
           null
```

Cada `NodoProducto` almacena:

- Un objeto `Producto` mediante la propiedad `Data`.
- Una referencia `Siguiente` hacia el próximo nodo.
- `null` cuando el nodo representa el final de la lista.

---

## Estructuras Implementadas

### NodoProducto

Representa la unidad individual de almacenamiento del catálogo dinámico.

```csharp
public class NodoProducto
{
    public Producto Data { get; set; }
    public NodoProducto? Siguiente { get; set; }
}
```

Cada nodo mantiene un producto completo y una referencia hacia el siguiente
elemento de la cadena.

---

### InventarioLista

Centraliza la administración de la estructura enlazada mediante un único
puntero principal:

```csharp
private NodoProducto? _cabeza;
```

Los métodos implementados son:

- `EstaVacia()`
- `InsertarInicio()`
- `InsertarOrdenado()`
- `Contar()`
- `BuscarPorSKU()`
- `EliminarPorSKU()`
- `MostrarTodos()`

---

## Inserción Dinámica

La operación:

```csharp
InsertarInicio()
```

permite agregar un nodo directamente al frente de la lista con complejidad:

```text
O(1)
```

También se implementó:

```csharp
InsertarOrdenado()
```

que recorre los nodos hasta encontrar la posición correspondiente según:

```text
Precio ASC
```

Su complejidad temporal es:

```text
O(n)
```

La inserción ordenada no necesita desplazar los demás productos en memoria.
Únicamente modifica las referencias necesarias entre nodos.

---

## Búsqueda por SKU

La operación:

```csharp
BuscarPorSKU(int sku)
```

realiza un recorrido secuencial desde `_cabeza`.

Complejidad:

```text
O(n)
```

Cuando el SKU solicitado no existe, el método genera una excepción controlada:

```csharp
KeyNotFoundException
```

Durante las pruebas se utilizó deliberadamente:

```text
SKU 9999
```

obteniéndose:

```text
Excepción controlada: SKU 9999 no encontrado en el inventario.
```

---

## Eliminación por SKU

La operación:

```csharp
EliminarPorSKU(int sku)
```

localiza el nodo correspondiente y modifica las referencias necesarias para
preservar la continuidad de la lista.

Se probaron satisfactoriamente los siguientes escenarios:

- Lista vacía.
- Eliminación de la cabeza.
- Eliminación de un nodo intermedio.
- Eliminación del último nodo.
- Eliminación de un SKU inexistente.

Cuando un nodo deja de ser alcanzable desde `_cabeza`, queda disponible para
ser recuperado posteriormente por el Garbage Collector de .NET.

---

# Prueba Funcional de Fase 2

Se realizó una prueba mediante consola con:

```text
15 productos
```

Los productos fueron insertados deliberadamente en un orden diferente al de
sus precios.

La operación:

```csharp
InsertarOrdenado()
```

generó automáticamente el catálogo final en:

```text
Precio ASC
```

Los precios observados comenzaron en:

```text
$179.90
```

y finalizaron en:

```text
$24,999.00
```

confirmando que la lista permaneció ordenada mientras aumentaba dinámicamente
su cantidad de nodos.

---

# Auditoría de Casos Borde

La implementación fue sometida a una auditoría estructural adicional.

Resultados:

```text
Lista vacía                         CORRECTO
Inserción al inicio                 CORRECTO
Eliminación de cabeza               CORRECTO
Eliminación de nodo intermedio      CORRECTO
Eliminación del último nodo         CORRECTO
Búsqueda de SKU inexistente         CORRECTO
```

Resultado final:

```text
AUDITORÍA ESTRUCTURAL COMPLETADA CORRECTAMENTE
```

---

# Comparación Técnica: Fase 1 vs. Fase 2

| Característica | Fase 1 — Arreglo | Fase 2 — Lista Enlazada |
|---|---|---|
| Estructura principal | `Producto[]` | `NodoProducto` |
| Tamaño | Determinado al crear el arreglo | Dinámico durante ejecución |
| Memoria física | Contigua para los elementos del arreglo | Nodos independientes enlazados por referencias |
| Acceso por índice | O(1) | No existe acceso directo |
| Búsqueda por SKU | O(n) | O(n) |
| Inserción al inicio | Requiere desplazamiento | O(1) |
| Inserción ordenada | O(n) + desplazamientos | O(n) + reenlace |
| Eliminación | Requiere reorganizar elementos | Reenlace de referencias |
| Crecimiento | Requiere nueva capacidad/arreglo | Creación dinámica de nodos |
| Liberación | Arreglo completo según ciclo de vida | Nodos no alcanzables gestionados por GC |

---

## Comparación de Uso de Memoria

### Arreglo — Fase 1

Un arreglo ofrece una representación compacta de elementos y acceso directo por
posición.

Su principal ventaja es la localidad de memoria y el acceso mediante índice en
tiempo constante.

Sin embargo, su capacidad queda determinada cuando se crea la estructura. Si se
reserva más espacio del realmente utilizado puede existir capacidad desperdiciada;
si el espacio resulta insuficiente, es necesario crear una estructura mayor y copiar
los elementos.

Por esta razón es adecuado cuando:

- la cantidad de productos es conocida;
- se necesita acceso frecuente por posición;
- existen pocas inserciones o eliminaciones;
- la estructura cambia poco durante su ciclo de vida.

---

### Lista Enlazada — Fase 2

La lista simplemente enlazada crea memoria únicamente cuando se agrega un nuevo
nodo al catálogo.

Cada nodo incorpora un costo adicional correspondiente a la referencia
`Siguiente`, por lo que individualmente posee más overhead que un elemento
equivalente dentro de un arreglo.

Sin embargo, la estructura no necesita reservar capacidad adicional para un
crecimiento futuro.

Esto permite que FastCart adapte el catálogo al número real de productos durante
la ejecución.

La lista resulta especialmente adecuada cuando:

- el número de elementos cambia con frecuencia;
- existen inserciones y eliminaciones constantes;
- no es indispensable el acceso aleatorio mediante índice;
- se prioriza flexibilidad estructural sobre localidad de memoria.

---

## Conclusión del Comparativo

La Fase 2 no sustituye al arreglo porque la lista enlazada consuma necesariamente
menos memoria por elemento.

De hecho, cada nodo requiere una referencia adicional y es administrado como un
objeto independiente en el Heap.

La ventaja principal de la lista enlazada se encuentra en su **flexibilidad
dinámica**.

Mientras que la Fase 1 ofrece mejor acceso directo y una representación compacta
para conjuntos conocidos, la Fase 2 permite que el catálogo crezca, disminuya y
modifique sus conexiones sin reconstruir un bloque completo de almacenamiento.

Por lo tanto:

```text
Fase 1 → eficiencia de acceso y almacenamiento contiguo
Fase 2 → flexibilidad de crecimiento y modificación dinámica
```

La elección correcta depende del comportamiento esperado del sistema.

Para FastCart, donde el catálogo puede sufrir altas y bajas frecuentes, la lista
enlazada proporciona una base estructural más adecuada para las siguientes fases
del proyecto.

---

# Evidencias de Fase 2

## Evidencia F2-01
Creación e integración inicial de la rama `proyecto/fase2-listas`.

## Evidencia F2-02
Implementación y validación del nodo dinámico `NodoProducto`.

## Evidencia F2-03-1
Inserción dinámica de 15 productos y validación del orden por precio ascendente.

## Evidencia F2-03-2
Búsqueda por SKU, excepción controlada y eliminación dinámica.

## Evidencia F2-04-1
Auditoría de casos borde e integridad de la lista enlazada.

## Evidencia F2-04-2
Verificación final de la integridad estructural.

---

# Estado de la Fase 2

- [x] Rama `proyecto/fase2-listas`.
- [x] Clase `NodoProducto`.
- [x] Propiedad `Data`.
- [x] Referencia `Siguiente`.
- [x] Clase `InventarioLista`.
- [x] Puntero `_cabeza`.
- [x] Inserción al inicio.
- [x] Inserción ordenada.
- [x] Orden por precio ascendente.
- [x] Búsqueda por SKU.
- [x] Excepción controlada.
- [x] Eliminación por SKU.
- [x] Recorrido secuencial.
- [x] Prueba dinámica con 15 productos.
- [x] Auditoría de casos borde.
- [x] Comentarios XML.
- [x] Comparación técnica con Fase 1.
- [x] Documentación técnica.
- [ ] Pull Request de Fase 2.