# Pixel-Gualy

## 🌍 Español

Pixel-Gualy es un intérprete del lenguaje creado para el segundo proyecto de programación de primer año: Pixel Walle. Este lenguaje posee instrucciones, funciones, asignaciones de variables y saltos condicionales para dibujar en un lienzo estilo pixel art.

El proyecto está programado en C#, utilizando el framework .NET 9. La solución consta de varios proyectos que conforman el backend, completamente separados del frontend, el cual está creado utilizando WinForm.

---

### 📋 Resumen de funcionalidades

Mediante el uso del editor de texto **Visual Gualy**, es posible:

- Escribir código del lenguaje Pixel Walle.  
- Cargar un archivo de texto de formato `.pw` que contenga un código Pixel Walle.  
- Guardar en un archivo `.pw` el código escrito en el editor.  
- Ajustar el tamaño del lienzo.  
- Visualizar los errores de compilación o de ejecución.  
- Visualizar la imagen creada en el lienzo.  
- Correr paso a paso el código, mostrando en cada momento el estado del canvas.  
- Guardar la imagen generada por el código.

> Gracias a su enfoque modular, puedes ejecutar un código de Pixel WALLE y crear la imagen sin necesidad de usar el editor de texto Visual Gualy.

---

### 💻 Requisitos de hardware

- **Procesador:** Intel Core i9 14th gen  
- **Memoria:** 1 MB de RAM (opcional)  
- **Gráficos:** NVIDIA GeForce RTX 5090 (obligatorio)  
- **Almacenamiento:** 2 MB SSD  

### 🖥️ Requisitos de software

- **Sistema operativo:** Windows 10 o superior

---

## Guía de uso del Visual Gualy

El editor de texto Visual Gualy posee dos áreas fundamentales:

1. **Área de código** (izquierda)  
   - Cada línea se crea al escribir y presionar **Enter**.  
   - Atajos:
     - **Insert:** insertar una nueva línea.  
     - **Delete:** eliminar la línea actual.  
     - **F2:** editar la línea seleccionada.  

2. **Lienzo y panel de errores** (derecha)  
   - Muestra la imagen actual del canvas.  
   - Lista errores de compilación (fila, columna) – `{Mensaje de Error}` y errores de ejecución.

En la **barra de menús** encontrarás:

- **Archivo**
  - **Nuevo:** limpia el lienzo y el editor de código.  
  - **Cargar:** importa un archivo `.pw` desde el disco.  
  - **Guardar:** guarda el código actual en `.pw` (si es la primera vez, actúa como “Guardar Como”).  
  - **Guardar Como:** elige directorio y guarda en `.pw`.  
  - **Guardar imagen:** exporta la imagen generada.  
  - **Salir:** cierra la aplicación.

- **Preferencias**
  - **Lienzo:** ajusta el tamaño del canvas (valores > 1 y < 2 000 000 001).

- **Depurar**
  - **Ejecutar todo:** compila y ejecuta el código completo.  
  - **Depurar:** compila y permite la ejecución paso a paso.

---

## Guía del lenguaje Pixel Wall-E

El lenguaje de Pixel Wall-E es un intérprete diseñado para manipular un lienzo en pixel-art mediante comandos secuenciales. Ofrece:

- **Asignaciones de variables**  
- **Expresiones aritméticas** y **booleanas**  
- **Funciones**  
- **Saltos condicionales**

### Características principales

- **Inicio obligatorio:** todo programa comienza con `Spawn(x, y)`.  
- **Control de pincel:** color con `Color(color)` y grosor con `Size(k)`.  
- **Dibujo de formas:**  
  - Líneas: `DrawLine(dirX, dirY, distance)`  
  - Círculos: `DrawCircle(dirX, dirY, radius)`  
  - Rectángulos: `DrawRectangle(dirX, dirY, distance, width, height)`  
- **Lógica y flujo:**  
  - Operaciones aritméticas: `+`, `-`, `*`, `/`, `**`, `%`.  
  - Operaciones booleanas: `&&`, `||`, `!`.  
  - Comparaciones: `==`, `<`, `>`, `<=`, `>=`, `!=`.  
- **Saltos condicionales:** etiquetas (`label`) y `GoTo [label] (condition)`.

---

## Detalles técnicos

> Este lenguaje ha sido diseñado con una arquitectura altamente extensible, lo que permite la incorporación de nuevos tokens, funciones, instrucciones y operadores sin necesidad de modificar el código base. La clave de esta flexibilidad radica en la estructuración del sistema, donde todas las entidades del lenguaje se gestionan mediante colecciones de datos centralizadas.  
>  
> Al agregar una nueva instrucción, simplemente se registra en la colección correspondiente, definiendo su comportamiento sin afectar el funcionamiento de las demás. Del mismo modo, los operadores y funciones se integran de forma modular, evitando la necesidad de modificar el intérprete o el parser existentes.  
>  
> Gracias a este enfoque, se pueden extender las capacidades del lenguaje de manera rápida y segura, manteniendo la estabilidad del sistema y permitiendo mejoras continuas sin comprometer la compatibilidad con versiones anteriores.

En este lenguaje, los booleanos se interpretan como enteros, al estilo C++:  
- `true` → `1`  
- `false` → `0`

Las operaciones booleanas se comportan así:  
> - `A && B` se evalúa como `A * B != 0`.  
> - `A || B` se evalúa como `A + B != 0`.  
> - `!A` devuelve `1` si `A == 0`, y `0` si `A != 0`.  
>
> Esto permite mezclar expresiones booleanas y aritméticas sin conversiones explícitas.

---

# Pixel-Gualy

## 🌎 English

Pixel-Gualy is an interpreter for the language created for the second first-year programming project: Pixel Walle. This language provides instructions, functions, variable assignments, and conditional jumps to draw on a pixel-art-style canvas.

The project is written in C# using the .NET 9 framework. The solution consists of multiple backend projects, fully decoupled from the frontend, which is built with WinForms.

---

### 📋 Feature Overview

Using the **Visual Gualy** text editor, you can:

- Write Pixel Walle language code.  
- Load a `.pw` text file containing Pixel Walle code.  
- Save the editor’s code to a `.pw` file.  
- Adjust the canvas size.  
- View compilation or runtime errors.  
- Preview the image rendered on the canvas.  
- Step through code execution, observing the canvas state at each step.  
- Save the generated image.

> Thanks to its modular design, you can execute Pixel WALLE code and generate an image without using the Visual Gualy editor.

---

### 💻 Hardware Requirements

- **Processor:** Intel Core i9 14th gen  
- **Memory:** 1 MB RAM (optional)  
- **Graphics:** NVIDIA GeForce RTX 5090 (required)  
- **Storage:** 2 MB SSD  

### 🖥️ Software Requirements

- **Operating System:** Windows 10 or higher

---

## Visual Gualy Usage Guide

The Visual Gualy editor has two main areas:

1. **Code Area** (left)  
   - Each line is created by typing and pressing **Enter**.  
   - Shortcuts:
     - **Insert:** add a new line  
     - **Delete:** remove the current line  
     - **F2:** edit the selected line  

2. **Canvas & Error Panel** (right)  
   - Displays the current canvas image.  
   - Lists compilation errors (`row, column` – `{Error Message}`) and runtime errors.

In the **menu bar** you’ll find:

- **File**
  - **New:** clears the canvas and code editor  
  - **Load:** imports a `.pw` file from disk  
  - **Save:** saves current code to `.pw` (acts as “Save As” on first save)  
  - **Save As:** choose a directory and save to `.pw`  
  - **Save Image:** exports the generated image  
  - **Exit:** closes the application

- **Preferences**
  - **Canvas:** set the canvas size (values > 1 and < 2 000 000 001)

- **Debug**
  - **Run All:** compiles and runs the entire code  
  - **Step Debug:** compiles and allows step-by-step execution

---

## Pixel Walle Language Guide

The Pixel Walle language is an interpreted language designed to manipulate a pixel-art canvas via sequential commands. It supports:

- **Variable assignments**  
- **Arithmetic** and **boolean expressions**  
- **Functions**  
- **Conditional jumps**

### Key Features

- **Mandatory start:** every program begins with `Spawn(x, y)`.  
- **Brush control:** color via `Color(color)` and thickness via `Size(k)`.  
- **Shape drawing:**  
  - Lines: `DrawLine(dirX, dirY, distance)`  
  - Circles: `DrawCircle(dirX, dirY, radius)`  
  - Rectangles: `DrawRectangle(dirX, dirY, distance, width, height)`  
- **Logic & flow:**  
  - Arithmetic operators: `+`, `-`, `*`, `/`, `**`, `%`  
  - Boolean operators: `&&`, `||`, `!`  
  - Comparisons: `==`, `<`, `>`, `<=`, `>=`, `!=`  
- **Conditional jumps:** labels (`label`) and `GoTo [label] (condition)`

---

## Technical Details

> This language was designed with a highly extensible architecture, allowing the addition of new tokens, functions, instructions, and operators without altering the core codebase. The key to this flexibility lies in centralizing all language entities in data collections.  
>  
> When adding a new instruction, you simply register it in the appropriate collection and define its behavior without impacting other features. Likewise, operators and functions integrate modularly, avoiding changes to the existing interpreter or parser.  
>  
> Thanks to this approach, you can rapidly and safely extend the language’s capabilities while preserving system stability and backward compatibility.

In this language, booleans are treated as integers (C++ style):  
- `true` → `1`  
- `false` → `0`

Boolean operations behave as follows:  
> - `A && B` is evaluated as `A * B != 0`.   
> - `A || B` is evaluated as `(A + B) != 0`.  
> - `!A` returns `1` if `A == 0`, otherwise `0`.  
>
> This enables mixing boolean and arithmetic expressions without explicit conversions.
