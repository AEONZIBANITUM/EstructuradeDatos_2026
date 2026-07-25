# EstructuradeDatos_2026

Repositorio académico de prácticas, ejercicios y entregables de la materia **Estructura de Datos**, correspondiente al ciclo **26-3**.

Los proyectos están desarrollados principalmente en **C# con .NET 8** y se administran mediante un único repositorio de **Git y GitHub**.

---

## Estado actual del repositorio

| Proyecto | Estado | Descripción |
|---|---|---|
| `Entregable1_Prueba` | Completado | Proyecto inicial para comprobar el funcionamiento de C#, .NET, Visual Studio Code, Git y GitHub. |
| `Practica1/CalculadoraFisica` | Completado | Calculadora de cinemática modular con validación de entradas y funciones independientes. |

---

## Tecnologías utilizadas

- C#
- .NET 8
- Visual Studio Code
- C# Dev Kit
- PowerShell
- Git
- GitHub

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
├── Entregable1_Prueba/
│   ├── Entregable1_Prueba.csproj
│   └── Program.cs
├── Practica1/
│   └── CalculadoraFisica/
│       ├── CalculadoraFisica.csproj
│       ├── Calculos.cs
│       ├── EntradaUsuario.cs
│       └── Program.cs
├── .gitignore
└── README.md
```

Las carpetas `bin/` y `obj/` se generan automáticamente durante la compilación, pero permanecen excluidas del control de versiones mediante el archivo `.gitignore` principal.

---

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

## Propósito académico

Este repositorio fue desarrollado con fines educativos para aplicar conceptos de:

- Funciones y métodos.
- Modularización.
- Separación de responsabilidades.
- Validación de datos.
- Flujo de control.
- Estructura de proyectos en C#.
- Uso de Git y GitHub.
