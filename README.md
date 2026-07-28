# EstructuradeDatos_2026

Repositorio académico de prácticas, ejercicios y entregables de la materia **Estructura de Datos**, correspondiente al ciclo **26-3**.

Los proyectos están desarrollados principalmente en **C# con .NET 8** y se administran mediante un único repositorio de **Git y GitHub**.

## Datos del estudiante

- **Nombre completo:** Jose Paulo Santana Ramirez
- **Matrícula:** 14868430
- **Materia:** Estructura de Datos
- **Ciclo:** 26-3
---

## Estado actual del repositorio

| Proyecto | Estado | Descripción |
|---|---|---|
| `Entregable1_Prueba` | Completado | Proyecto inicial para comprobar el funcionamiento de C#, .NET, Visual Studio Code, Git y GitHub. |
| `Practica1/CalculadoraFisica` | Completado | Calculadora de cinemática modular con validación de entradas y funciones independientes. |
| `Practica2/Practica2-Punteros` | Completado | Simulación de mecanismos de punteros en C# mediante parámetros `ref` y `out`, con documentación y evidencias. |
| `Practica3/SimuladorHeap` | Completado | Simulador de arreglos dinámicos para analizar Stack, Heap, mutación de objetos y reasignación local de referencias. |
| `Practica4/Semana4Recursividad` | Completado | Implementación segura de factorial y Fibonacci en versiones iterativas y recursivas, con medición mediante `Stopwatch` y análisis del Call Stack. |
| `Practica5/SistemaInventario` | Completado | Sistema de gestión de inventario con `struct`, arreglo estático, validaciones, búsqueda por ID, actualización de stock, persistencia CSV y depuración paso a paso. |

---

## Tecnologías utilizadas

- C#
- .NET 8
- Visual Studio Code
- C# Dev Kit
- PowerShell
- Git
- GitHub
- Archivos CSV

---

## Requisitos

Para compilar y ejecutar los proyectos se necesita:

- Windows, macOS o Linux
- .NET SDK 8.0 o una versión posterior compatible con proyectos `net8.0`
- Visual Studio Code
- Extensión C# Dev Kit de Microsoft
- Git

Para comprobar los SDK instalados:

```powershell
dotnet --list-sdks
```

Para consultar el SDK seleccionado por la terminal:

```powershell
dotnet --version
```

---

## Estructura del repositorio

```text
EstructuradeDatos_2026/
├── .vscode/
│   └── launch.json
├── Entregable1_Prueba/
│   ├── Entregable1_Prueba.csproj
│   └── Program.cs
├── Practica1/
│   └── CalculadoraFisica/
│       ├── CalculadoraFisica.csproj
│       ├── Calculos.cs
│       ├── EntradaUsuario.cs
│       └── Program.cs
├── Practica2/
│   └── Practica2-Punteros/
│       ├── capturas/
│       ├── src/
│       │   ├── Calculadora.cs
│       │   └── Practica2Punteros.csproj
│       ├── Program.cs
│       └── README.md
├── Practica3/
│   └── SimuladorHeap/
│       ├── Program.cs
│       └── SimuladorHeap.csproj
├── Practica4/
│   └── Semana4Recursividad/
│       ├── capturas/
│       │   ├── 01_scaffold_inicial_y_rama.png
│       │   ├── 03_comparativa_stopwatch.png
│       │   ├── 04_call_stack_factorial_caso_base.png
│       │   └── 05_call_stack_factorial_desapilado.png
│       ├── AlgoritmosIterativos.cs
│       ├── AlgoritmosRecursivos.cs
│       ├── Program.cs
│       ├── README.md
│       └── Semana4Recursividad.csproj
├── Practica5/
│   └── SistemaInventario/
│       ├── capturas/
│       ├── InventarioCsv.cs
│       ├── Producto.cs
│       ├── Program.cs
│       ├── inventario.csv
│       ├── README.md
│       └── SistemaInventario.csproj
├── .gitignore
└── README.md
```

# Entregable 1 — Proyecto de prueba

## Propósito

`Entregable1_Prueba` es una aplicación de consola creada para comprobar la configuración y comunicación correcta entre:

- Visual Studio Code
- C# Dev Kit
- .NET SDK
- Git
- GitHub

El proyecto utiliza .NET 8 y muestra el mensaje predeterminado:

```text
Hello, World!
```

## Compilar el Entregable 1

Desde la raíz del repositorio:

```powershell
dotnet build .\Entregable1_Prueba\Entregable1_Prueba.csproj
```

## Ejecutar el Entregable 1

```powershell
dotnet run --project .\Entregable1_Prueba\Entregable1_Prueba.csproj
```

---

# Práctica 1 — Calculadora Física

## Nombre de la práctica

**Creación de Submódulos de Cálculo mediante Funciones en C# .NET Core**

## Objetivo

Desarrollar una aplicación de consola modular capaz de realizar cálculos básicos de cinemática.

La práctica separa las responsabilidades del programa en distintos archivos:

- Flujo y menú principal.
- Operaciones matemáticas.
- Captura y validación de entradas.
- Configuración del proyecto.

Esta organización permite construir un programa más legible, mantenible y reutilizable.

---

## Funcionalidades

La calculadora permite seleccionar las siguientes operaciones:

1. Calcular velocidad.
2. Calcular distancia.
3. Calcular tiempo.
0. Salir del programa.

El menú se mantiene activo mediante un ciclo hasta que el usuario selecciona la opción de salida.

---

## Fórmulas implementadas

| Operación | Fórmula | Unidad del resultado |
|---|---|---|
| Velocidad | `v = d / t` | Metros por segundo (`m/s`) |
| Distancia | `d = v × t` | Metros (`m`) |
| Tiempo | `t = d / v` | Segundos (`s`) |

Donde:

- `v` representa la velocidad.
- `d` representa la distancia.
- `t` representa el tiempo.

---

## Arquitectura de la aplicación

### `Program.cs`

Es el punto de entrada y el módulo de orquestación del programa.

Sus responsabilidades son:

- Configurar la salida de consola en UTF-8.
- Mantener activo el ciclo principal.
- Mostrar el menú de opciones.
- Leer la opción seleccionada.
- Solicitar los datos necesarios para cada cálculo.
- Invocar los métodos de los demás módulos.
- Mostrar los resultados.
- Finalizar el programa cuando el usuario selecciona `0`.

Métodos principales:

```csharp
static void Main(string[] args)
static void MostrarMenu()
static bool ProcesarOpcion(string opcion)
```

---

### `Calculos.cs`

Contiene exclusivamente las operaciones matemáticas de la calculadora.

Métodos disponibles:

```csharp
Calculos.CalcularVelocidad(distanciaMetros, tiempoSegundos)
Calculos.CalcularDistancia(velocidadMs, tiempoSegundos)
Calculos.CalcularTiempo(distanciaMetros, velocidadMs)
```

Las funciones de este módulo son puras:

- Reciben datos mediante parámetros.
- No solicitan información al usuario.
- No imprimen directamente en consola.
- No modifican variables externas.
- Devuelven el resultado mediante `return`.
- Producen el mismo resultado cuando reciben los mismos valores.

---

### `EntradaUsuario.cs`

Centraliza la captura y validación de valores numéricos.

Método disponible:

```csharp
EntradaUsuario.PedirDouble(mensaje, soloPositivos)
```

La validación:

- Rechaza texto que no pueda convertirse a `double`.
- Rechaza cero y números negativos cuando `soloPositivos` es `true`.
- Vuelve a solicitar el dato hasta recibir un valor válido.
- Evita excepciones mediante `double.TryParse`.
- Protege la lectura ante valores `null`.
- Utiliza `InvariantCulture`.
- Espera el punto `.` como separador decimal.

Ejemplo válido:

```text
12.5
```

Ejemplo no válido para esta configuración:

```text
12,5
```

---

### `CalculadoraFisica.csproj`

Define la configuración del proyecto.

Características principales:

```xml
<OutputType>Exe</OutputType>
<TargetFramework>net8.0</TargetFramework>
<ImplicitUsings>enable</ImplicitUsings>
<Nullable>enable</Nullable>
```

El proyecto genera una aplicación ejecutable de consola dirigida a **.NET 8**.

---

## Principios de diseño aplicados

### Cohesión alta

Cada archivo concentra funciones relacionadas con una responsabilidad concreta.

- `Program.cs`: flujo principal.
- `Calculos.cs`: lógica matemática.
- `EntradaUsuario.cs`: captura y validación.

### Acoplamiento bajo

Los módulos se comunican mediante parámetros y valores de retorno.

### Responsabilidad única

Cada clase tiene una función claramente delimitada dentro de la aplicación.

### Funciones puras

Las operaciones matemáticas no producen efectos secundarios.

### Nomenclatura

- PascalCase para clases y métodos.
- camelCase para parámetros y variables locales.
- Nombres descriptivos para identificar el propósito de cada elemento.

### Validación centralizada

La lógica para leer y validar números se encuentra en un único método reutilizable.

---

## Compilar la Práctica 1

Desde la raíz del repositorio:

```powershell
dotnet build .\Practica1\CalculadoraFisica\CalculadoraFisica.csproj
```

Una compilación correcta debe finalizar sin errores:

```text
Compilación realizada correctamente
0 Error(es)
```

---

## Ejecutar la Práctica 1

```powershell
dotnet run --project .\Practica1\CalculadoraFisica\CalculadoraFisica.csproj
```

---

## Menú principal

Al ejecutar la aplicación se muestra un menú semejante al siguiente:

```text
┌────────────────────────────────┐
│ CALCULADORA DE CINEMÁTICA      │
├────────────────────────────────┤
│ 1. Calcular Velocidad          │
│ 2. Calcular Distancia          │
│ 3. Calcular Tiempo             │
│ 0. Salir                       │
└────────────────────────────────┘
```

---

## Ejemplos de uso

### Calcular velocidad

Datos:

```text
Distancia (m): 100
Tiempo (s): 20
```

Resultado:

```text
Velocidad: 5.00 m/s
```

---

### Calcular distancia

Datos:

```text
Velocidad (m/s): 10
Tiempo (s): 5
```

Resultado:

```text
Distancia: 50.00 m
```

---

### Calcular tiempo

Datos:

```text
Distancia (m): 100
Velocidad (m/s): 20
```

Resultado:

```text
Tiempo: 5.00 s
```

---

## Manejo de opciones

El programa utiliza un `switch` para procesar la selección del usuario.

| Opción | Acción | Continuación |
|---|---|---|
| `1` | Calcula velocidad | Regresa al menú |
| `2` | Calcula distancia | Regresa al menú |
| `3` | Calcula tiempo | Regresa al menú |
| `0` | Finaliza la aplicación | Termina el ciclo |
| Otra | Muestra un mensaje de opción inválida | Regresa al menú |

---

## Flujo general de ejecución

```text
Inicio
  │
  ├── Configurar UTF-8
  │
  ├── Mostrar menú
  │
  ├── Leer opción
  │
  ├── Procesar opción
  │      ├── Solicitar datos
  │      ├── Validar datos
  │      ├── Ejecutar cálculo
  │      └── Mostrar resultado
  │
  ├── Volver al menú
  │
  └── Salir cuando la opción sea 0
```

---

## Control de versiones

Este repositorio utiliza una sola carpeta `.git` en la raíz.

No se debe ejecutar dentro de los proyectos:

```powershell
git init
git remote add origin
git branch -M main
```

El flujo normal de trabajo desde la raíz es:

```powershell
git status
git add <ruta>
git commit -m "tipo: descripcion del cambio"
git push origin main
```

Convenciones utilizadas para los mensajes de commit:

| Tipo | Uso |
|---|---|
| `feat` | Nueva funcionalidad |
| `fix` | Corrección de errores |
| `refactor` | Mejora interna sin cambiar el comportamiento |
| `docs` | Cambios de documentación |
| `chore` | Mantenimiento y configuración |

Ejemplo:

```powershell
git commit -m "feat: implementar calculadora fisica modular"
```

---

## Archivos ignorados

El archivo `.gitignore` principal evita publicar archivos generados automáticamente, incluyendo:

```text
bin/
obj/
```

Estos directorios pueden existir localmente después de compilar, pero no forman parte del código fuente del repositorio.

---

## Estado de la Práctica 1

- Proyecto creado con .NET 8.
- Arquitectura modular implementada.
- Cálculo de velocidad comprobado.
- Cálculo de distancia comprobado.
- Cálculo de tiempo comprobado.
- Validación de entradas implementada.
- Compilación realizada sin errores.
- Commit creado.
- Proyecto publicado en GitHub.

---

# Práctica 2 — Simulación de Punteros en C#

## Nombre de la práctica

**Mecanismos de Pasaje de Parámetros Avanzados mediante `ref` y `out` en C#**

## Objetivo

Comprender cómo C# permite simular ciertos comportamientos asociados con punteros mediante parámetros especiales que pueden modificar variables del método llamador o inicializar valores desde un método auxiliar.

## Proyecto

```text
Practica2/Practica2-Punteros
```

## Conceptos aplicados

- Paso de parámetros por valor.
- Uso de parámetros `ref`.
- Uso de parámetros `out`.
- Modificación de variables desde métodos auxiliares.
- Separación de responsabilidades.
- Refactorización del código.
- Validación mediante compilación y ejecución.
- Flujo de trabajo con ramas y commits atómicos.

## Estructura principal

```text
Practica2/Practica2-Punteros/
├── capturas/
├── src/
│   ├── Calculadora.cs
│   └── Practica2Punteros.csproj
├── Program.cs
└── README.md
```

## Compilar la Práctica 2

Desde la raíz del repositorio:

```powershell
dotnet build ".\Practica2\Practica2-Punteros\src\Practica2Punteros.csproj"
```

## Ejecutar la Práctica 2

```powershell
dotnet run --project ".\Practica2\Practica2-Punteros\src\Practica2Punteros.csproj"
```

## Estado de la Práctica 2

- Proyecto dirigido a .NET 8.
- Uso de `ref` implementado y comprobado.
- Uso de `out` implementado y comprobado.
- Refactorización completada.
- Compilación realizada sin errores.
- Evidencias organizadas.
- Commits descriptivos conservados.
- Merge y publicación en GitHub completados.

---

# Práctica 3 — Control y Manipulación de Arreglos Dinámicos en el Heap

## Proyecto

```text
Practica3/SimuladorHeap
```

## Objetivo

Analizar el comportamiento de los arreglos de tipos de referencia en .NET y comprobar mediante el debugger la relación entre:

- Variables locales almacenadas en el Stack.
- Objetos dinámicos almacenados en el Heap.
- Copias de referencias enviadas como parámetros.
- Mutación de un objeto compartido.
- Reasignación local de una referencia.
- Inmutabilidad de los objetos `string`.

## Métodos implementados

```csharp
static void Main(string[] args)
static string[] InicializarArreglo(int n)
static void ModificarArreglo(string[] arr)
static void ModificarElementos(string[] arr)
static void ReasignarArreglo(string[] arr)
static void MostrarArreglo(string[] arr)
```

## Funcionamiento general

El programa:

1. Solicita la cantidad de elementos.
2. Crea un arreglo de `string` en tiempo de ejecución.
3. Captura y muestra sus valores iniciales.
4. Convierte cada elemento a mayúsculas.
5. Agrega el identificador `[MOD-i]`.
6. Modifica el primer elemento mediante una referencia compartida.
7. Reasigna localmente el parámetro a un arreglo nuevo.
8. Comprueba que esa reasignación no sustituye la referencia de `Main`.

## Stack y Heap

La variable local:

```csharp
string[] arreglo
```

se encuentra dentro del marco de ejecución de `Main` y contiene una referencia.

El objeto creado mediante:

```csharp
new string[n]
```

se almacena en el Heap administrado.

Cuando el arreglo se envía a un método sin utilizar `ref`, se copia la referencia. El método puede modificar los elementos del mismo objeto, pero una reasignación local no sustituye la variable del llamador.

## Escenario A — Mutación del objeto compartido

```csharp
static void ModificarElementos(string[] arr)
{
    arr[0] = "MODIFICADO";
}
```

La instrucción modifica una posición del arreglo compartido. Al regresar a `Main`, el cambio permanece visible.

## Escenario B — Reasignación local

```csharp
static void ReasignarArreglo(string[] arr)
{
    arr = new string[] { "NUEVO", "ARREGLO" };
}
```

Se crea otro objeto en el Heap, pero únicamente cambia la copia local de la referencia `arr`.

La variable `arreglo` de `Main` continúa apuntando al objeto original.

## Compilar la Práctica 3

```powershell
dotnet restore ".\Practica3\SimuladorHeap\SimuladorHeap.csproj"
dotnet build ".\Practica3\SimuladorHeap\SimuladorHeap.csproj" --no-restore
```

## Ejecutar la Práctica 3

```powershell
dotnet run --project ".\Practica3\SimuladorHeap\SimuladorHeap.csproj" --no-build
```

## Ejemplo de ejecución

Entradas:

```text
3
rojo
azul
verde
```

Resultado principal:

```text
--- Arreglo Inicial ---
 [0] = rojo
 [1] = azul
 [2] = verde

--- Arreglo Modificado ---
 [0] = ROJO [MOD-0]
 [1] = AZUL [MOD-1]
 [2] = VERDE [MOD-2]

--- Escenario A: Modificar elementos ---
 [0] = MODIFICADO
 [1] = AZUL [MOD-1]
 [2] = VERDE [MOD-2]

--- Escenario B: Reasignar arreglo ---
 [0] = MODIFICADO
 [1] = AZUL [MOD-1]
 [2] = VERDE [MOD-2]
```

## Depuración realizada

Mediante breakpoints y los paneles Variables, Watch y Call Stack se comprobó que:

- `arreglo` es `null` antes de ejecutar la inicialización.
- Después de `InicializarArreglo`, apunta a un objeto `string[3]`.
- El parámetro `arr` permite acceder al mismo objeto.
- La mutación de `arr[0]` persiste al regresar a `Main`.
- La reasignación de `arr` crea localmente un objeto `string[2]`.
- Al regresar a `Main`, `arreglo` conserva los tres elementos del objeto original.

## Flujo de Git de la Práctica 3

Rama de desarrollo:

```text
feature/simulador-heap
```

Commits registrados:

```text
chore: inicializar proyecto SimuladorHeap
feat: agregar inicialización de arreglo dinámico
feat: agregar función de modificación y display
feat: agregar experimento ref vs reasignación
```

Commit de integración:

```text
Merge PR: Práctica 3 completada
```

El merge se realizó con `--no-ff` para preservar la bifurcación de la rama y mantener un historial claramente trazable.

## Estado de la Práctica 3

- Proyecto creado con .NET 8.
- Código compilado sin errores.
- Ejecución completa verificada.
- Breakpoints configurados.
- Variables observadas mediante debugger.
- Escenario A comprobado.
- Escenario B comprobado.
- Tres commits funcionales registrados.
- Rama feature publicada.
- Merge `--no-ff` completado.
- `main` publicada y sincronizada con `origin/main`.
- Carpetas `bin` y `obj` excluidas del repositorio.

---

# Práctica 4 — Implementación Segura de Recursividad en C#

## Proyecto

```text
Practica4/Semana4Recursividad
```

## Objetivo

Implementar, validar y comparar algoritmos iterativos y recursivos para calcular factoriales y valores de la sucesión de Fibonacci, analizando su rendimiento, consumo de memoria y comportamiento dentro del Call Stack.

La práctica permite comprobar la importancia de:

- Definir casos base explícitos y alcanzables.
- Reducir progresivamente el problema en cada llamada recursiva.
- Validar entradas negativas.
- Detectar desbordamientos aritméticos.
- Comparar complejidad temporal y espacial.
- Medir el rendimiento con resultados reales.
- Observar el apilamiento y desapilamiento mediante el debugger.

## Estructura principal

```text
Practica4/Semana4Recursividad/
├── capturas/
│   ├── 01_scaffold_inicial_y_rama.png
│   ├── 03_comparativa_stopwatch.png
│   ├── 04_call_stack_factorial_caso_base.png
│   └── 05_call_stack_factorial_desapilado.png
├── AlgoritmosIterativos.cs
├── AlgoritmosRecursivos.cs
├── Program.cs
├── README.md
└── Semana4Recursividad.csproj
```

## Algoritmos implementados

```csharp
FactorialIterativo(int numero)
FibonacciIterativo(int numero)
FactorialRecursivo(int numero)
FibonacciRecursivo(int numero)
```

Las implementaciones incluyen:

- Uso de `long` para ampliar el rango numérico.
- Bloques `checked` para detectar desbordamientos.
- Excepciones `ArgumentOutOfRangeException` para entradas negativas.
- Casos base independientes para factorial y Fibonacci.
- Comparación automática entre resultados iterativos y recursivos.
- Medición mediante `System.Diagnostics.Stopwatch`.

## Factorial iterativo y recursivo

La versión iterativa utiliza un ciclo `for`:

```text
n! = 1 × 2 × 3 × ... × n
```

La versión recursiva aplica:

```text
Factorial(n) = n × Factorial(n - 1)
```

Caso base:

```csharp
if (numero <= 1)
{
    return 1;
}
```

Comparación de complejidad:

| Versión | Tiempo | Espacio adicional |
|---|---|---|
| Iterativa | `O(n)` | `O(1)` |
| Recursiva | `O(n)` | `O(n)` |

El factorial más grande almacenado de forma segura en `long` durante la práctica fue:

```text
20! = 2432902008176640000
```

## Fibonacci iterativo y recursivo

Definición aplicada:

```text
Fibonacci(0) = 0
Fibonacci(1) = 1
Fibonacci(n) = Fibonacci(n - 1) + Fibonacci(n - 2)
```

Casos base:

```csharp
if (numero == 0)
{
    return 0;
}

if (numero == 1)
{
    return 1;
}
```

Comparación de complejidad:

| Versión | Tiempo | Espacio adicional |
|---|---|---|
| Iterativa | `O(n)` | `O(1)` |
| Recursiva ingenua | Aproximadamente `O(2^n)` | `O(n)` |

La versión recursiva ingenua recalcula múltiples veces los mismos valores, por lo que su tiempo aumenta rápidamente conforme crece la entrada.

## Prueba comparativa con Stopwatch

La prueba utilizó:

```csharp
int nFactorial = 20;
int nFibonacci = 40;
```

Resultados numéricos:

```text
Factorial(20) = 2432902008176640000
Fibonacci(40) = 102334155
```

Resultados de la ejecución documentada:

| Algoritmo | Resultado | Tiempo | Ticks |
|---|---:|---:|---:|
| Factorial iterativo | 2432902008176640000 | 0.085900 ms | 859 |
| Factorial recursivo | 2432902008176640000 | 0.075200 ms | 752 |
| Fibonacci iterativo | 102334155 | 0.081000 ms | 810 |
| Fibonacci recursivo | 102334155 | 403.087200 ms | 4030872 |

Los tiempos pueden variar entre ejecuciones por la compilación JIT, la carga del procesador, los procesos activos y otras condiciones del entorno.

En la ejecución documentada, Fibonacci recursivo tardó aproximadamente 4,976 veces más que Fibonacci iterativo.

## Depuración del Call Stack

Se colocó un breakpoint en:

```csharp
return 1;
```

dentro del caso base de `FactorialRecursivo`.

La depuración permitió comprobar:

- Llegada al caso base con `numero = 1`.
- Acumulación de llamadas desde `FactorialRecursivo(20)` hasta `FactorialRecursivo(1)`.
- Permanencia de las llamadas anteriores mientras esperan un resultado.
- Retorno a `numero = 2` después de ejecutar una instrucción con `F10`.
- Retiro progresivo de marcos siguiendo el modelo LIFO.

Conceptualmente, la pila contenía:

```text
FactorialRecursivo(1)
FactorialRecursivo(2)
FactorialRecursivo(3)
...
FactorialRecursivo(20)
Program
```

## Compilar la Práctica 4

Desde la raíz del repositorio:

```powershell
dotnet build ".\Practica4\Semana4Recursividad\Semana4Recursividad.csproj"
```

Para la medición en modo `Release`:

```powershell
dotnet build ".\Practica4\Semana4Recursividad\Semana4Recursividad.csproj" -c Release
```

## Ejecutar la Práctica 4

```powershell
dotnet run --project ".\Practica4\Semana4Recursividad\Semana4Recursividad.csproj"
```

Para ejecutar el benchmark compilado en `Release`:

```powershell
dotnet run --project ".\Practica4\Semana4Recursividad\Semana4Recursividad.csproj" -c Release --no-build
```

## Evidencias incorporadas

```text
01_scaffold_inicial_y_rama.png
03_comparativa_stopwatch.png
04_call_stack_factorial_caso_base.png
05_call_stack_factorial_desapilado.png
```

Las evidencias documentan:

- Creación del proyecto y de la rama.
- Comparación de resultados, tiempos y ticks.
- Caso base con `numero = 1`.
- Inicio del desapilamiento con `numero = 2`.

## Flujo de Git de la Práctica 4

Rama de desarrollo:

```text
feature/recursividad-segura
```

Commits registrados:

```text
chore: inicializar proyecto Semana4Recursividad
feat: implementar factorial y fibonacci iterativos
feat: implementar factorial y fibonacci recursivos con casos base
test: agregar comparativa de rendimiento con Stopwatch
docs: documentar resultados y evidencias de la practica 4
```

Commit de integración:

```text
Merge PR: Práctica 4 completada
```

El merge se realizó con `--no-ff` para preservar la bifurcación de la rama y mantener un historial trazable.

## Estado de la Práctica 4

- Proyecto creado con .NET 8.
- Algoritmos iterativos implementados.
- Algoritmos recursivos implementados.
- Casos base verificados.
- Validación de entradas negativas incorporada.
- Control de desbordamiento incorporado.
- Comparativa con `Stopwatch` completada.
- Call Stack observado mediante debugger.
- Caso base y desapilamiento documentados.
- README individual terminado.
- Cuatro evidencias incorporadas.
- Cinco commits semánticos registrados.
- Rama feature publicada.
- Merge `--no-ff` completado.
- Carpetas `bin` y `obj` excluidas del repositorio.

---


# Práctica 5 — Sistema de Gestión de Inventario Básico

## Proyecto

```text
Practica5/SistemaInventario
```

## Objetivo

Desarrollar una aplicación de consola para administrar un inventario mediante una estructura personalizada `Producto`, un arreglo estático de capacidad controlada y un menú persistente.

La práctica integra los conceptos de:

- Modelado de datos mediante `struct`.
- Arreglos estáticos de estructuras.
- Contadores lógicos para posiciones ocupadas.
- Ciclos `do-while`.
- Sentencias `switch`.
- Parámetros modificables mediante `ref`.
- Validación robusta de entradas.
- Búsqueda lineal por identificador.
- Modificación directa de elementos almacenados.
- Persistencia mediante archivos CSV.
- Manejo de excepciones de entrada y salida.
- Depuración con breakpoints, Variables, Watch y Call Stack.
- Control de versiones con ramas y commits semánticos.

## Estructura principal

```text
Practica5/SistemaInventario/
├── capturas/
├── InventarioCsv.cs
├── Producto.cs
├── Program.cs
├── inventario.csv
├── README.md
└── SistemaInventario.csproj
```

La configuración de depuración se encuentra en:

```text
.vscode/launch.json
```

La documentación completa de esta práctica está disponible en:

[README individual de la Práctica 5](Practica5/SistemaInventario/README.md)

## Modelado de `Producto`

La entidad principal fue creada mediante un `struct`:

```csharp
public struct Producto
{
    public int ID;
    public string Nombre;
    public double Precio;
    public int Stock;
}
```

Cada registro conserva:

| Campo | Tipo | Función |
|---|---|---|
| `ID` | `int` | Identificador único |
| `Nombre` | `string` | Descripción del producto |
| `Precio` | `double` | Precio unitario |
| `Stock` | `int` | Cantidad disponible |

La estructura incluye un constructor para inicializar todos los campos de forma coherente.

## Arreglo estático y contador lógico

El sistema utiliza una capacidad fija de diez productos:

```csharp
private const int CapacidadMaxima = 10;

Producto[] inventario =
    new Producto[CapacidadMaxima];
```

El número de posiciones ocupadas se controla mediante:

```csharp
int totalRegistros = 0;
```

La capacidad física permanece en diez posiciones, mientras que `totalRegistros` delimita las posiciones que contienen información válida.

Los recorridos procesan únicamente:

```text
0 hasta totalRegistros - 1
```

## Menú interactivo

La aplicación permanece activa mediante un ciclo `do-while` y procesa las opciones mediante `switch`.

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

| Opción | Operación |
|---:|---|
| `1` | Registrar producto |
| `2` | Mostrar inventario |
| `3` | Buscar producto por ID |
| `4` | Actualizar stock |
| `5` | Guardar inventario en CSV |
| `6` | Cargar inventario desde CSV |
| `7` | Salir |

## Registro mediante `ref`

El método de registro recibe el contador mediante referencia:

```csharp
private static void RegistrarProducto(
    Producto[] inventario,
    ref int totalRegistros)
```

Después de almacenar el producto se ejecuta:

```csharp
totalRegistros++;
```

Esto modifica la variable original declarada en `Main`, no una copia local.

La operación valida:

- Capacidad disponible.
- ID numérico y positivo.
- Ausencia de identificadores duplicados.
- Nombre no vacío.
- Precio mayor que cero.
- Stock igual o mayor que cero.

## Presentación del inventario

El inventario se muestra en formato tabular:

```text
ID       NOMBRE                           PRECIO    STOCK
--------------------------------------------------------------
101      laptop hp                    $15,999.99        8
102      teclado mecanico              $1,299.50       40
103      mouse inalambrico                $599.90       20
--------------------------------------------------------------
Total de productos registrados: 3
```

Los precios se presentan con formato monetario correspondiente a México.

## Búsqueda lineal por ID

La localización de productos se realiza mediante:

```csharp
private static int BuscarIndicePorId(
    Producto[] inventario,
    int totalRegistros,
    int idBuscado)
```

El método recorre únicamente las posiciones ocupadas y devuelve:

- El índice del producto encontrado.
- `-1` cuando el ID no existe.

Se comprobaron tres escenarios:

- Inventario vacío.
- Producto existente.
- Producto inexistente.

## Actualización directa del stock

Después de localizar el producto, el sistema modifica únicamente su cantidad:

```csharp
inventario[indiceEncontrado].Stock =
    nuevoStock;
```

El ID, nombre y precio permanecen intactos.

Ejemplo verificado:

```text
Producto:         Teclado Mecanico
Stock anterior:   15
Stock nuevo:      40
Posición:         1
```

## Control de capacidad

Antes de registrar se comprueba:

```csharp
if (totalRegistros >= inventario.Length)
```

Cuando el arreglo alcanza `10/10`, el sistema rechaza el registro número once:

```text
No es posible registrar más productos.
El inventario alcanzó su capacidad máxima.
```

Esta condición evita accesos fuera de los límites del arreglo y previene `IndexOutOfRangeException`.

## Persistencia CSV

La lógica de archivos fue separada en:

```text
InventarioCsv.cs
```

### Guardado

La opción `5` genera:

```text
inventario.csv
```

con una estructura semejante a:

```csv
ID;Nombre;Precio;Stock
101;laptop hp;15999.99;8
102;teclado mecanico;1299.5;40
103;mouse inalambrico;599.9;20
```

El archivo utiliza:

- Encabezado.
- Separador `;`.
- Codificación UTF-8.
- Cultura invariante para valores numéricos.
- Escape de campos con comillas.
- Control de errores de escritura.

### Carga

La opción `6`:

1. Comprueba la existencia del archivo.
2. Lee y separa los campos.
3. Valida cada registro.
4. Rechaza datos inválidos.
5. Evita IDs duplicados.
6. Respeta la capacidad máxima.
7. Reconstruye los productos.
8. Actualiza `totalRegistros` mediante `ref`.

Se verificó la recuperación de los tres productos en una ejecución nueva.

## Manejo de excepciones

Las operaciones de archivo controlan:

```csharp
UnauthorizedAccessException
IOException
```

Además, se usa:

```csharp
File.Exists(NombreArchivo)
```

para evitar intentar cargar un archivo inexistente.

## Depuración realizada

La depuración se ejecutó en la terminal integrada de Visual Studio Code mediante:

```text
.vscode/launch.json
```

Se utilizaron:

- Breakpoints.
- Variables locales.
- Panel `Watch`.
- `Call Stack`.
- `F10` para ejecución paso a paso.
- `F5` para continuar.
- `Shift + F5` para detener.

### Comprobación de `ref`

Antes de ejecutar:

```csharp
totalRegistros++;
```

se observó:

```text
totalRegistros = 0
posicionUtilizada = 0
```

Después de `F10`:

```text
totalRegistros = 1
posicionUtilizada = 0
```

Esto confirmó que el contador original fue modificado.

### Comprobación del `struct` almacenado

Antes de ejecutar la asignación:

```text
stockAnterior = 40
nuevoStock = 55
inventario[indiceEncontrado].Stock = 40
```

Después de ejecutarla:

```text
stockAnterior = 40
nuevoStock = 55
inventario[indiceEncontrado].Stock = 55
```

Esto confirmó que el cambio se realizó directamente sobre el elemento del arreglo.

## Compilar la Práctica 5

Desde la raíz del repositorio:

```powershell
dotnet restore ".\Practica5\SistemaInventario\SistemaInventario.csproj"
dotnet build ".\Practica5\SistemaInventario\SistemaInventario.csproj"
```

## Ejecutar la Práctica 5

```powershell
dotnet run --project ".\Practica5\SistemaInventario\SistemaInventario.csproj"
```

## Pruebas realizadas

| Prueba | Resultado |
|---|---|
| Compilación | Correcta |
| Menú persistente | Correcto |
| Registro de productos | Correcto |
| ID inválido | Rechazado |
| ID duplicado | Rechazado |
| Nombre vacío | Rechazado |
| Precio inválido | Rechazado |
| Stock negativo | Rechazado |
| Listado tabular | Correcto |
| Búsqueda existente | Correcta |
| Búsqueda inexistente | Controlada |
| Actualización de stock | Correcta |
| Inventario lleno | Controlado |
| Guardado CSV | Correcto |
| Carga CSV | Correcta |
| Parámetro `ref` | Comprobado |
| Modificación directa del `struct` | Comprobada |
| Estado de Git | Limpio |

## Evidencias

La práctica contiene **28 capturas** organizadas en:

```text
Practica5/SistemaInventario/capturas
```

Las evidencias abarcan:

- Estado inicial del repositorio.
- Creación de la rama y del proyecto.
- Commit inicial de la práctica.
- Modelado del `struct Producto` y del arreglo estático.
- Menú interactivo con `do-while` y `switch`.
- Registro y listado de productos.
- Búsqueda positiva y negativa por ID.
- Actualización directa del stock.
- Guardado y carga del inventario en CSV.
- Validaciones de ID, nombre, precio y stock.
- Inventario completo con capacidad `10/10`.
- Rechazo del registro número once.
- Depuración del parámetro `ref`.
- Depuración de la asignación directa sobre el `struct`.
- Verificación final de compilación, archivos y estado de Git.
- Publicación de la rama `feature/sistema-inventario`.
- Integración de la Práctica 5 en `main` mediante `merge --no-ff`.
- Publicación final de `main` en GitHub.

Las cuatro evidencias de cierre son:

| Número | Evidencia |
|---:|---|
| 13 | [Verificación final de la Práctica 5](Practica5/SistemaInventario/capturas/13_verificacion_final_practica5.png) |
| 14 | [Publicación de la rama feature](Practica5/SistemaInventario/capturas/14_rama_feature_publicada.png) |
| 15 | [Integración de la Práctica 5 en main](Practica5/SistemaInventario/capturas/15_merge_practica5_en_main.png) |
| 16 | [Publicación final de main en GitHub](Practica5/SistemaInventario/capturas/16_main_publicada_en_github.png) |

El índice completo de las 28 capturas se encuentra en el
[README individual de la Práctica 5](Practica5/SistemaInventario/README.md).

## Flujo de Git de la Práctica 5

Rama de desarrollo:

```text
feature/sistema-inventario
```

### Commits registrados en la rama de desarrollo

```text
ac7c4a2 chore: inicializar proyecto SistemaInventario
e69dfac feat: modelar productos con struct y arreglo
b012ea7 feat: implementar menu interactivo del inventario
cec0513 feat: implementar registro y listado de productos
0a7adbc feat: agregar busqueda de productos por id
e245450 feat: implementar actualizacion de stock
7c279dc feat: implementar persistencia de inventario en csv
1f3b664 chore: agregar configuracion de depuracion en vscode
b40a3f2 docs: documentar practica 5 y agregar evidencias
5c7af5b docs: completar README detallado de la practica 5
7ea1391 docs: actualizar README principal con la practica 5
cad43ba docs: agregar evidencias finales de verificacion y publicacion
```

### Integración y cierre en `main`

```text
c7f839a Merge PR: Práctica 5 completada
bdab1af docs: agregar evidencias finales de merge y publicacion
```

La integración se realizó mediante:

```powershell
git merge --no-ff feature/sistema-inventario -m "Merge PR: Práctica 5 completada"
```

El historial gráfico conservó la bifurcación de la rama de desarrollo y el
commit explícito de integración. Posteriormente, `main` fue publicada y quedó
sincronizada con `origin/main`.

En total se conservaron:

- Doce commits en la rama de desarrollo.
- Un commit de integración mediante `merge --no-ff`.
- Un commit documental final en `main`.

## Estado de la Práctica 5

- Proyecto creado con .NET 8.
- `struct Producto` implementado.
- Arreglo estático de diez posiciones.
- Contador lógico implementado.
- Menú con `do-while` y `switch`.
- Registro y listado funcionales.
- Validaciones robustas.
- Búsqueda por ID.
- Actualización directa de stock.
- Control de capacidad máxima.
- Persistencia CSV.
- Manejo de errores de archivo.
- Depuración configurada.
- Parámetro `ref` comprobado.
- Modificación del `struct` comprobada.
- README individual terminado.
- Veintiocho evidencias incorporadas.
- Doce commits de desarrollo, un commit de integración y un commit documental final conservados.
- Compilación sin errores.
- Rama `feature/sistema-inventario` publicada en GitHub.
- Integración `--no-ff` completada en `main`.
- `main` publicada y sincronizada con `origin/main`.
- Repositorio sin cambios pendientes.

---

## Propósito académico

Este repositorio fue desarrollado con fines educativos para comprender, implementar y documentar conceptos de:

- Funciones y métodos.
- Modularización.
- Separación de responsabilidades.
- Validación de datos.
- Flujo de control.
- Parámetros `ref` y `out`.
- Estructuras personalizadas mediante `struct`.
- Arreglos estáticos y control lógico de posiciones ocupadas.
- Menús persistentes con `do-while` y `switch`.
- Búsqueda lineal y actualización de registros.
- Persistencia y recuperación mediante archivos CSV.
- Manejo de excepciones de entrada y salida.
- Tipos de valor y tipos de referencia.
- Stack y Heap.
- Recursividad segura.
- Caso base y caso recursivo.
- Crecimiento y desapilamiento del Call Stack.
- Comparación entre iteración y recursividad.
- Complejidad temporal y espacial.
- Medición de rendimiento mediante `Stopwatch`.
- Arreglos creados dinámicamente.
- Mutación de objetos compartidos.
- Reasignación local de referencias.
- Inmutabilidad de `string`.
- Depuración de aplicaciones .NET.
- Uso de breakpoints, Variables, Watch y Call Stack.
- Organización de proyectos en C#.
- Uso de ramas y commits atómicos.
- Integración mediante merges `--no-ff`.
- Uso responsable de Git y GitHub.

---

## Uso de inteligencia artificial como apoyo didáctico

Durante el desarrollo de las prácticas se utilizó ChatGPT como herramienta de apoyo para:

- Interpretar las instrucciones y documentos oficiales.
- Comprender la estructura de los comandos de PowerShell.
- Aplicar correctamente el flujo de Git y GitHub.
- Organizar las carpetas y archivos de cada proyecto.
- Explicar conceptos de programación y estructuras de datos.
- Analizar errores de compilación, ejecución y depuración.
- Verificar el cumplimiento de los requisitos y checklists.
- Preparar documentación técnica y registros cronológicos.

Todos los comandos fueron escritos y ejecutados directamente por el estudiante en Visual Studio Code.

El código fue capturado, compilado, ejecutado y depurado mediante resultados reales. Las evidencias utilizadas corresponden al entorno de trabajo del estudiante y no fueron fabricadas ni sustituidas por ejemplos externos.

La inteligencia artificial se utilizó como apoyo didáctico para mejorar la comprensión y la aplicación de los conceptos, sin reemplazar la ejecución, comprobación ni responsabilidad académica del estudiante.
