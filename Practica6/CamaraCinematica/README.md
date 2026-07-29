# Práctica 6 — Composición de Cámaras Virtuales mediante Structs Anidados

Simulación de un sistema de cámara cinematográfica virtual desarrollado en **C# con .NET 8**, utilizando estructuras personalizadas anidadas, paso de parámetros mediante `ref`, interpolación lineal progresiva y depuración paso a paso.

---

## Información académica

| Dato | Información |
|---|---|
| Alumno | José Paulo Santana Ramírez |
| Materia | Estructura de Datos |
| Ciclo | 26-3 |
| Práctica | Práctica 6 |
| Lenguaje | C# |
| Framework | .NET 8 |
| Entorno de desarrollo | Visual Studio Code |
| Control de versiones | Git y GitHub |
| Rama de desarrollo | `feature/camara-cinematica` |

---

## Descripción general

La práctica implementa una simulación numérica de una cámara cinematográfica virtual que se desplaza progresivamente desde una posición inicial hacia una posición objetivo.

La cámara también modifica gradualmente el punto tridimensional hacia el cual dirige su enfoque.

El sistema fue desarrollado como una aplicación de consola, por lo que el movimiento de la cámara se representa mediante la impresión de sus coordenadas durante una secuencia de 20 frames.

La práctica integra los siguientes conceptos:

- Modelado de datos mediante `struct`.
- Composición de estructuras anidadas.
- Tipos por valor.
- Paso de parámetros mediante `ref`.
- Modificación directa de una variable original.
- Interpolación lineal manual.
- Simulación de frames.
- Formateo de salida en consola.
- Depuración mediante breakpoints.
- Inspección de variables anidadas.
- Implementación de un segundo rig cinematográfico.
- Corte instantáneo entre configuraciones de cámara.
- Control de versiones mediante Git.

---

## Objetivos

### Objetivo general

Construir un sistema de cámara virtual capaz de modificar progresivamente su posición y su punto de enfoque mediante estructuras anidadas y paso por referencia.

### Objetivos específicos

- Definir las estructuras `Posicion`, `Foco` y `CamaraCinematica`.
- Integrar `Posicion` y `Foco` dentro de `CamaraCinematica`.
- Inicializar todos los campos de la cámara principal.
- Crear una posición y un foco objetivo.
- Implementar el método `ActualizarCamara`.
- Utilizar `ref` para modificar el rig original.
- Aplicar interpolación lineal en los seis componentes tridimensionales.
- Ejecutar una simulación de 20 frames.
- Mostrar la convergencia de los valores.
- Crear un segundo rig denominado `CAM_CLOSEUP`.
- Implementar una función de corte instantáneo.
- Comprobar mediante depuración el efecto del paso por referencia.
- Documentar el proceso mediante evidencias y commits semánticos.

---

## Estructura del proyecto

```text
Practica6/
└── CamaraCinematica/
    ├── capturas/
    ├── bin/
    ├── obj/
    ├── CamaraCinematica.csproj
    ├── Practica6_JosePauloSantanaRamirez.cs
    └── README.md
```

Los directorios `bin` y `obj` son generados automáticamente durante la compilación y están excluidos del control de versiones mediante `.gitignore`.

El proyecto contiene un único archivo fuente de C#:

```text
Practica6_JosePauloSantanaRamirez.cs
```

---

## Modelo de datos

El sistema utiliza tres estructuras personalizadas.

### `Posicion`

Representa la ubicación tridimensional de la cámara.

```csharp
public struct Posicion
{
    public float x;
    public float y;
    public float z;
}
```

### `Foco`

Representa el punto tridimensional hacia el cual apunta la cámara.

```csharp
public struct Foco
{
    public float x;
    public float y;
    public float z;
}
```

### `CamaraCinematica`

Agrupa el estado completo de un rig cinematográfico.

```csharp
public struct CamaraCinematica
{
    public string nombre;
    public Posicion pos;
    public Foco foco;
    public float fov;
    public float velocidad;
}
```

---

## Composición de las estructuras

La estructura lógica del rig es la siguiente:

```text
CamaraCinematica
├── nombre
├── fov
├── velocidad
├── pos : Posicion
│   ├── x
│   ├── y
│   └── z
└── foco : Foco
    ├── x
    ├── y
    └── z
```

`CamaraCinematica` contiene directamente una estructura `Posicion` y una estructura `Foco`.

Al tratarse de estructuras, sus valores se copian de forma predeterminada cuando se asignan o se envían como parámetros sin modificadores.

---

## Cámara principal

El rig principal se denomina:

```text
CAM_PRINCIPAL
```

### Configuración inicial

| Campo | Valor |
|---|---:|
| Nombre | `CAM_PRINCIPAL` |
| Posición X | `10f` |
| Posición Y | `5f` |
| Posición Z | `-8f` |
| Foco X | `0f` |
| Foco Y | `0f` |
| Foco Z | `0f` |
| FOV | `60f` |
| Velocidad | `0.08f` |

La estructura se inicializa mediante un inicializador de objetos:

```csharp
CamaraCinematica camara = new CamaraCinematica
{
    nombre = "CAM_PRINCIPAL",

    pos = new Posicion
    {
        x = 10f,
        y = 5f,
        z = -8f
    },

    foco = new Foco
    {
        x = 0f,
        y = 0f,
        z = 0f
    },

    fov = 60f,
    velocidad = 0.08f
};
```

---

## Objetivos cinematográficos

### Posición objetivo

```text
(0, 2, -5)
```

```csharp
Posicion posicionObjetivo = new Posicion
{
    x = 0f,
    y = 2f,
    z = -5f
};
```

### Foco objetivo

```text
(0, 1, 0)
```

```csharp
Foco focoObjetivo = new Foco
{
    x = 0f,
    y = 1f,
    z = 0f
};
```

---

## Interpolación lineal

La cámara no cambia instantáneamente de posición durante la simulación principal.

En cada frame se desplaza un porcentaje de la distancia que todavía existe entre el valor actual y el objetivo.

La fórmula utilizada es:

```text
actual = actual + (objetivo - actual) × alpha
```

En el programa, `alpha` corresponde a la velocidad del rig:

```csharp
float alpha = cam.velocidad;
```

Para `CAM_PRINCIPAL`:

```text
alpha = 0.08
```

Esto representa un desplazamiento del 8 % de la distancia restante durante cada frame.

---

## Método `ActualizarCamara`

El método central recibe la cámara mediante `ref`:

```csharp
private static void ActualizarCamara(
    ref CamaraCinematica cam,
    Posicion posicionObjetivo,
    Foco focoObjetivo)
```

El modificador `ref` permite que el método trabaje directamente con la variable original creada en `Main`.

Sin `ref`, el método recibiría una copia del `struct` y las modificaciones se perderían al finalizar la llamada.

---

## Interpolación de posición

La fórmula se aplica individualmente sobre los tres ejes de la posición:

```csharp
cam.pos.x +=
    (posicionObjetivo.x - cam.pos.x) * alpha;

cam.pos.y +=
    (posicionObjetivo.y - cam.pos.y) * alpha;

cam.pos.z +=
    (posicionObjetivo.z - cam.pos.z) * alpha;
```

---

## Interpolación del foco

La misma operación se aplica sobre los tres componentes del foco:

```csharp
cam.foco.x +=
    (focoObjetivo.x - cam.foco.x) * alpha;

cam.foco.y +=
    (focoObjetivo.y - cam.foco.y) * alpha;

cam.foco.z +=
    (focoObjetivo.z - cam.foco.z) * alpha;
```

En total, la interpolación se realiza sobre seis componentes:

```text
Posición:
- x
- y
- z

Foco:
- x
- y
- z
```

---

## Simulación de 20 frames

El programa ejecuta 20 actualizaciones consecutivas:

```csharp
for (int frame = 1; frame <= 20; frame++)
{
    ActualizarCamara(
        ref camara,
        posicionObjetivo,
        focoObjetivo);

    ImprimirEstado(camara, frame);

    Thread.Sleep(80);
}
```

En cada iteración se realizan las siguientes acciones:

1. Actualizar la cámara mediante `ref`.
2. Aplicar la interpolación en los seis componentes.
3. Imprimir los valores actualizados.
4. Esperar 80 milisegundos.
5. Continuar con el siguiente frame.

---

## Formato de salida

El método `ImprimirEstado` utiliza los formatos:

- `D3` para mostrar el número de frame con tres dígitos.
- `F2` para mostrar las coordenadas con dos decimales.

```csharp
private static void ImprimirEstado(
    CamaraCinematica cam,
    int frame)
{
    Console.WriteLine(
        $"[Frame {frame:D3}] {cam.nombre} | " +
        $"POS({cam.pos.x:F2}, " +
        $"{cam.pos.y:F2}, " +
        $"{cam.pos.z:F2}) | " +
        $"FOCO({cam.foco.x:F2}, " +
        $"{cam.foco.y:F2}, " +
        $"{cam.foco.z:F2})");
}
```

Ejemplo:

```text
[Frame 001] CAM_PRINCIPAL | POS(9.20, 4.76, -7.76) | FOCO(0.00, 0.08, 0.00)
```

---

## Convergencia observada

Los primeros valores de la simulación fueron:

```text
[Frame 001] CAM_PRINCIPAL | POS(9.20, 4.76, -7.76) | FOCO(0.00, 0.08, 0.00)
[Frame 002] CAM_PRINCIPAL | POS(8.46, 4.54, -7.54) | FOCO(0.00, 0.15, 0.00)
[Frame 003] CAM_PRINCIPAL | POS(7.79, 4.34, -7.34) | FOCO(0.00, 0.22, 0.00)
```

El resultado del frame 20 fue:

```text
[Frame 020] CAM_PRINCIPAL | POS(1.89, 2.57, -5.57) | FOCO(0.00, 0.81, 0.00)
```

Los valores avanzan progresivamente hacia:

```text
Posición objetivo: (0.00, 2.00, -5.00)
Foco objetivo:     (0.00, 1.00, 0.00)
```

La interpolación se aproxima continuamente al objetivo sin producir un salto instantáneo.

---

## Segundo rig cinematográfico

Como extensión se implementó un segundo rig:

```text
CAM_CLOSEUP
```

### Configuración

| Campo | Valor |
|---|---:|
| Nombre | `CAM_CLOSEUP` |
| Posición X | `1f` |
| Posición Y | `1.8f` |
| Posición Z | `-1.5f` |
| Foco X | `0f` |
| Foco Y | `1.7f` |
| Foco Z | `0f` |
| FOV | `35f` |
| Velocidad | `0.15f` |

Código:

```csharp
CamaraCinematica camaraCloseUp = new CamaraCinematica
{
    nombre = "CAM_CLOSEUP",

    pos = new Posicion
    {
        x = 1f,
        y = 1.8f,
        z = -1.5f
    },

    foco = new Foco
    {
        x = 0f,
        y = 1.7f,
        z = 0f
    },

    fov = 35f,
    velocidad = 0.15f
};
```

Este rig representa una configuración de plano cercano.

---

## Función de corte instantáneo

También se implementó el método:

```csharp
private static void CortarA(
    ref CamaraCinematica destino,
    CamaraCinematica fuente)
```

La cámara de destino utiliza `ref` porque su estado debe modificarse directamente.

La cámara fuente se recibe por valor porque solamente se consulta.

```csharp
private static void CortarA(
    ref CamaraCinematica destino,
    CamaraCinematica fuente)
{
    destino.pos = fuente.pos;
    destino.foco = fuente.foco;
    destino.fov = fuente.fov;

    Console.WriteLine();

    Console.WriteLine(
        $"Corte instantáneo aplicado: " +
        $"{fuente.nombre} -> {destino.nombre}");
}
```

La función copia:

- Posición.
- Foco.
- Campo de visión.

El nombre del rig principal se conserva para demostrar que se modificó el mismo `struct`.

---

## Resultado del corte

### Antes del corte

```text
[Frame 000] CAM_PRINCIPAL |
POS(1.89, 2.57, -5.57) |
FOCO(0.00, 0.81, 0.00)

FOV antes del corte: 60.00 grados
```

### Después del corte

```text
[Frame 000] CAM_PRINCIPAL |
POS(1.00, 1.80, -1.50) |
FOCO(0.00, 1.70, 0.00)

FOV después del corte: 35.00 grados
```

El resultado comprueba que la posición, el foco y el FOV fueron sustituidos correctamente.

---

## Depuración del paso por referencia

Se utilizó el depurador integrado de Visual Studio Code para observar el contenido del rig antes y después de ejecutar la interpolación.

### Estado inicial observado

```text
cam.pos.x  = 10
cam.pos.y  = 5
cam.pos.z  = -8

cam.foco.x = 0
cam.foco.y = 0
cam.foco.z = 0

cam.velocidad = 0.08
```

### Objetivos observados

```text
posicionObjetivo.x = 0
posicionObjetivo.y = 2
posicionObjetivo.z = -5

focoObjetivo.x = 0
focoObjetivo.y = 1
focoObjetivo.z = 0
```

### Estado modificado

Durante la ejecución paso a paso se comprobó que los campos de `cam.pos` y `cam.foco` cambiaron dentro de `ActualizarCamara`.

Al regresar al método principal, los nuevos valores permanecieron almacenados en la cámara original.

Esto demuestra el funcionamiento del modificador `ref` aplicado a un tipo por valor.

---

## Incidencia de ejecución con AppHost

Durante el desarrollo, Windows bloqueó inicialmente el ejecutable nativo:

```text
CamaraCinematica.exe
```

La compilación era correcta, pero el sistema operativo impedía iniciar el AppHost generado automáticamente.

El programa pudo ejecutarse directamente mediante el ensamblado:

```powershell
dotnet .\Practica6\CamaraCinematica\bin\Debug\net8.0\CamaraCinematica.dll
```

Como solución permanente se añadió al archivo `.csproj`:

```xml
<UseAppHost>false</UseAppHost>
```

Configuración final:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <UseAppHost>false</UseAppHost>
  </PropertyGroup>

</Project>
```

Esta configuración permite ejecutar el ensamblado administrado mediante el host de .NET sin depender del ejecutable nativo bloqueado.

---

## Compilación

Desde la raíz del repositorio:

```powershell
dotnet build .\Practica6\CamaraCinematica\CamaraCinematica.csproj
```

Resultado esperado:

```text
Compilación realizada correctamente
0 advertencias
0 errores
```

---

## Ejecución

### Ejecución mediante el proyecto

```powershell
dotnet run --project .\Practica6\CamaraCinematica\CamaraCinematica.csproj
```

### Ejecución directa del ensamblado

```powershell
dotnet .\Practica6\CamaraCinematica\bin\Debug\net8.0\CamaraCinematica.dll
```

---

## Rúbrica cubierta

| Criterio | Implementación | Estado |
|---|---|---|
| Tres `structs` definidos correctamente | `Posicion`, `Foco`, `CamaraCinematica` | Cumplido |
| Campos públicos | Todos los campos son `public` | Cumplido |
| Composición de estructuras | `Posicion` y `Foco` dentro de `CamaraCinematica` | Cumplido |
| Método con `ref` | `ActualizarCamara(ref CamaraCinematica cam, ...)` | Cumplido |
| Interpolación en seis ejes | Tres ejes de posición y tres del foco | Cumplido |
| Simulación mínima de 20 frames | Bucle de frames 1 a 20 | Cumplido |
| Formato de salida | `D3` para frames y `F2` para coordenadas | Cumplido |
| Convergencia progresiva | Valores aproximándose al objetivo | Cumplido |
| Segundo rig | `CAM_CLOSEUP` | Cumplido |
| Función de corte | `CortarA` | Cumplido |
| Depuración del paso por referencia | Breakpoints y variables anidadas | Cumplido |
| Documentación | README y capturas | Cumplido |

---

## Flujo de trabajo con Git

La práctica fue desarrollada en la rama:

```text
feature/camara-cinematica
```

### Commits principales

```text
chore: crear estructura inicial de la practica 6

feat: definir structs anidados e inicializar camara principal

feat: implementar interpolacion con ref y simulacion de 20 frames

feat: agregar segundo rig y corte cinematografico

docs: agregar evidencias de depuracion del paso por referencia
```

Los cambios fueron registrados mediante commits separados para conservar una evolución clara del proyecto.

---

## Evidencias oficiales

Las imágenes se encuentran en:

```text
Practica6/CamaraCinematica/capturas
```

### Estado inicial y creación del proyecto

```text
01_estado_inicial_repositorio.png
02_rama_y_scaffold_practica6.png
02-2_rama_y_scaffold_practica6.png
02-3_commit_inicial_practica6.png
```

### Modelado de estructuras

```text
03_structs_anidados_codigo.png
03_structs_anidados_codigo.png.png
03_structs_anidados_y_objetivos.png
03-2_commit_structs_e_inicializacion.png
```

### Interpolación, `ref` e incidencia de AppHost

```text
04_funcion_ref_y_lerp.png
04-0_bloqueo_apphost_windows.png
04-1_bloqueo_apphost_windows.png
04-1_funcion_ref_y_lerp.png
04-2_bloqueo_apphost_windows.png
04-2_funcion_ref_y_lerp.png
04-3_bloqueo_apphost_windows.png
```

### Simulación y commit

```text
05_simulacion_20_frames_convergencia.png.png
05-2_commit_interpolacion_y_frames.png
```

### Segundo rig y corte cinematográfico

```text
06_segundo_rig_cam_closeup.png
06-2_funcion_cortarA_y_resultado.png
06-3_commit_segundo_rig_y_corte.png
```

### Depuración del paso por referencia

```text
07_debug_ref_antes_interpolacion.png
07-2_debug_ref_despues_interpolacion.png
07-3_commit_debug_ref.png
```

---

## Galería de evidencias

### Estado inicial del repositorio

![Estado inicial](capturas/01_estado_inicial_repositorio.png)

### Rama y estructura de la práctica

![Rama y scaffold](capturas/02_rama_y_scaffold_practica6.png)

![Estructura y compilación](capturas/02-2_rama_y_scaffold_practica6.png)

### Commit inicial

![Commit inicial](capturas/02-3_commit_inicial_practica6.png)

### Estructuras anidadas

![Structs anidados](capturas/03_structs_anidados_codigo.png)

![Structs anidados complementaria](capturas/03_structs_anidados_codigo.png.png)

### Inicialización y objetivos

![Inicialización y objetivos](capturas/03_structs_anidados_y_objetivos.png)

### Commit de estructuras

![Commit de estructuras](capturas/03-2_commit_structs_e_inicializacion.png)

### Método con `ref` e interpolación

![Función ref y lerp](capturas/04_funcion_ref_y_lerp.png)

![Función ref y lerp complementaria](capturas/04-1_funcion_ref_y_lerp.png)

![Función ref y lerp complementaria 2](capturas/04-2_funcion_ref_y_lerp.png)

### Incidencia de AppHost

![Bloqueo AppHost](capturas/04-0_bloqueo_apphost_windows.png)

![Bloqueo AppHost 1](capturas/04-1_bloqueo_apphost_windows.png)

![Bloqueo AppHost 2](capturas/04-2_bloqueo_apphost_windows.png)

![Bloqueo AppHost 3](capturas/04-3_bloqueo_apphost_windows.png)

### Simulación de 20 frames

![Simulación de 20 frames](capturas/05_simulacion_20_frames_convergencia.png.png)

### Commit de interpolación

![Commit de interpolación](capturas/05-2_commit_interpolacion_y_frames.png)

### Segundo rig

![Segundo rig CAM_CLOSEUP](capturas/06_segundo_rig_cam_closeup.png)

### Función de corte

![Función CortarA](capturas/06-2_funcion_cortarA_y_resultado.png)

### Commit del segundo rig

![Commit segundo rig](capturas/06-3_commit_segundo_rig_y_corte.png)

### Depuración antes de la interpolación

![Debug antes](capturas/07_debug_ref_antes_interpolacion.png)

### Depuración después de la interpolación

![Debug después](capturas/07-2_debug_ref_despues_interpolacion.png)

### Commit de depuración

![Commit debug ref](capturas/07-3_commit_debug_ref.png)

---

## Problemas identificados y soluciones

### 1. Modificación de una copia del `struct`

**Riesgo:** enviar `CamaraCinematica` por valor produciría una copia.

**Solución:** utilizar `ref` tanto en la declaración como en la llamada.

```csharp
ActualizarCamara(
    ref camara,
    posicionObjetivo,
    focoObjetivo);
```

### 2. Campos no inicializados

**Riesgo:** leer componentes sin asignar previamente todos sus valores.

**Solución:** utilizar inicializadores completos para las tres estructuras.

### 3. Velocidad inválida

**Riesgo:** una velocidad igual a cero impediría el movimiento.

**Solución:** utilizar valores positivos:

```text
CAM_PRINCIPAL = 0.08
CAM_CLOSEUP   = 0.15
```

### 4. Bloqueo del ejecutable por Windows

**Riesgo:** Windows impidió iniciar el AppHost generado.

**Solución:** ejecutar la DLL con `dotnet` y configurar:

```xml
<UseAppHost>false</UseAppHost>
```

### 5. Verificación insuficiente de `ref`

**Riesgo:** observar únicamente la salida final sin comprobar los valores internos.

**Solución:** utilizar breakpoints y expandir `cam.pos` y `cam.foco` en el panel de variables.

---

## Aprendizajes obtenidos

Esta práctica permitió comprobar que los `structs` tienen semántica de valor y que el paso normal de parámetros genera una copia independiente.

El uso de `ref` permite modificar directamente una estructura original sin devolverla como resultado.

También se comprobó que una estructura puede contener otras estructuras, permitiendo representar datos relacionados mediante una composición clara.

La interpolación lineal muestra cómo un valor puede aproximarse progresivamente hacia un objetivo mediante operaciones simples aplicadas repetidamente.

El segundo rig y la función `CortarA` demostraron la diferencia entre una transición progresiva y un cambio instantáneo de estado.

Finalmente, la depuración paso a paso permitió observar directamente los valores antes y después de cada modificación, reforzando la relación entre el código, la memoria y el resultado mostrado en consola.

---

## Conclusión

La Práctica 6 fue completada mediante una implementación modular y documentada de una cámara cinematográfica virtual.

El programa utiliza tres estructuras personalizadas, interpolación manual en seis ejes, modificación mediante `ref`, una simulación de 20 frames, un segundo rig de plano cercano y una función de corte instantáneo.

La ejecución demostró la convergencia progresiva de la posición y el foco. La depuración confirmó que los cambios permanecen en la variable original gracias al paso por referencia.

La práctica también fue integrada al flujo de control de versiones del repositorio académico mediante una rama independiente, commits semánticos y evidencias verificables.

---

## Integridad académica y uso de apoyo didáctico

El código fue construido, ejecutado, depurado y validado dentro del entorno local del estudiante.

Durante el desarrollo se utilizó ChatGPT como herramienta de apoyo didáctico para:

- Comprender la estructura de los comandos.
- Analizar el comportamiento de `struct` y `ref`.
- Organizar el proceso de desarrollo.
- Identificar y resolver errores de compilación y ejecución.
- Estructurar la documentación técnica.
- Verificar el cumplimiento de los requisitos de la práctica.

La herramienta de inteligencia artificial fue utilizada como acompañamiento para el aprendizaje y no como sustitución de la ejecución, comprobación y comprensión del trabajo.

---

## Autoría

Desarrollo académico realizado por:

**José Paulo Santana Ramírez**

Materia:

**Estructura de Datos — Ciclo 26-3**