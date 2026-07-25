# Práctica 2: Simulación de Punteros en C#

## Datos del estudiante

- **Nombre completo:** Jose Paulo Santana Ramirez
- **Matrícula:** 14868430
- **Materia:** Estructura de Datos
- **Ciclo:** 26-3

## Descripción

Este proyecto demuestra el uso de los modificadores `ref` y `out` en C#.
El método `Sumar` modifica directamente una variable previamente inicializada mediante `ref`, mientras que `AnalizarValores` produce múltiples resultados mediante parámetros `out`.

La práctica fue desarrollada en una rama independiente de Git llamada `feature/referencias`, utilizando commits intermedios para documentar cada etapa de la refactorización.

Reflexión personal

Durante esta práctica comprendí que ref permite modificar directamente una variable que ya fue inicializada, mientras que out permite producir uno o varios resultados desde un método.
También observé que ambos modificadores cambian la forma en que los métodos se comunican con las variables del programa principal.
La comparación entre el código original y el refactorizado me ayudó a entender mejor la diferencia entre devolver un valor y trabajar mediante referencia.
Finalmente, el uso de una rama y commits intermedios permitió mantener un historial ordenado y documentar claramente cada cambio realizado.

Uso de herramientas de apoyo

Se utilizó herramientas de inteligencia artifial como apoyo didáctico para comprender las instrucciones, revisar errores, analizar comandos y verificar el cumplimiento de la práctica. El código fue escrito, ejecutado y comprobado directamente por la estudiante en Visual Studio Code.

## Requisitos

- .NET SDK 8 o una versión posterior compatible.
- Visual Studio Code con C# Dev Kit.
- Git instalado y configurado.

El proyecto mantiene `net8.0` como framework objetivo.

## Compilar

Desde la raíz del repositorio:

```powershell
dotnet build ".\Practica2\Practica2-Punteros\src\Practica2Punteros.csproj"

