# Práctica 2: Simulación de Punteros en C#

## Datos del estudiante

- **Nombre completo:** Jose Paulo Santana Ramirez
- **Matrícula:** 14868430
- **Materia:** Estructura de Datos
- **Ciclo:** 26-3

## Descripción

Este proyecto demuestra el uso de los modificadores `ref` y `out` en C# mediante cuatro métodos.

Los métodos `Sumar` e `Intercambiar` utilizan `ref` para modificar directamente variables previamente inicializadas. Los métodos `AnalizarValores` e `IntentarDividir` utilizan `out` para producir múltiples resultados desde una misma llamada.

La práctica fue desarrollada mediante ramas independientes de Git y commits atómicos que documentan la implementación original, la refactorización y la ampliación necesaria para cumplir los criterios de nivel Excelente de la rúbrica.

## Reflexión personal

Durante esta práctica comprendí que `ref` permite trabajar directamente sobre variables que ya existen y que deben estar inicializadas antes de llamar al método.

También comprobé que `out` permite producir varios resultados y obliga al método a asignarlos antes de finalizar, incluso cuando existen diferentes rutas de ejecución.

La implementación de `Intercambiar` e `IntentarDividir` me permitió aplicar estos conceptos en situaciones distintas a los ejemplos iniciales.

Finalmente, el uso de ramas y commits atómicos facilitó ampliar el proyecto sin poner en riesgo la versión estable previamente terminada.

## Uso de herramientas de apoyo

Se utilizaron herramientas de inteligencia artificial como apoyo didáctico para comprender las instrucciones, revisar errores, analizar comandos y verificar el cumplimiento de la práctica.

El código fue escrito, ejecutado y comprobado directamente por la estudiante en Visual Studio Code.

## Requisitos

- .NET SDK 8 o una versión posterior compatible.
- Visual Studio Code con C# Dev Kit.
- Git instalado y configurado.

El proyecto mantiene `net8.0` como framework objetivo.

## Compilar

Desde la raíz del repositorio:

```powershell
dotnet build ".\Practica2\Practica2-Punteros\src\Practica2Punteros.csproj"
```

## Ejecutar

Desde la raíz del repositorio:

```powershell
dotnet run --project ".\Practica2\Practica2-Punteros\src\Practica2Punteros.csproj"
```

## Resultado esperado

```text
10
Prom:4.75 Max:8
Intercambio: primero=9 segundo=5
División válida: True cociente=3 residuo=2
```

## Conceptos aplicados

- Modificación de variables existentes mediante `ref`.
- Intercambio de dos valores utilizando referencias.
- Producción de múltiples resultados mediante `out`.
- Asignación obligatoria de parámetros `out` en todas las rutas.
- Conservación del comportamiento original después de refactorizar.
- Uso de clases y métodos estáticos.
- Documentación XML.
- Flujo de trabajo con ramas y commits atómicos en Git.