Documentacion de Pruebas Automatizadas - Selenium WebDriver
Datos del Estudiante
Nombre: Rafael Antonio Urbaez Hernandez

Institucion: Instituto Tecnologico de Las Americas (ITLA)

Carrera: Desarrollo de Software

Materia: Programacion III 

1. Descripcion del Proyecto
Este proyecto consiste en la implementacion de una suite de pruebas automatizadas para un sistema de gestion de videojuegos (CRUD) desarrollado en ASP.NET Core. Se han diseñado 5 Historias de Usuario principales, cada una validada mediante tres escenarios especificos: Camino Feliz (Happy Path), Pruebas Negativas y Pruebas de Limites (Boundary Testing).

Cobertura de Pruebas
Se ejecutaron un total de 15 casos de prueba automatizados que cubren:

Autenticacion: Validacion de acceso con credenciales correctas e incorrectas.

Registro (Create): Validacion de campos obligatorios y rangos de precio (0.00 a 10,000.00).

Lectura (Read): Verificacion de carga de datos en tablas y manejo de errores 404.

Edicion (Update): Persistencia de cambios y re-validacion de reglas de negocio.

Borrado (Delete): Confirmacion de eliminacion y cancelacion de la accion.

2. Requisitos del Sistema
Para ejecutar este proyecto de pruebas, el entorno debe contar con:

SDK de .NET: Version 10.0 o superior.

Navegador: Microsoft Edge (actualizado).

Base de Datos: Instancia local de SQL Server configurada segun el appsettings.json.

Driver: Selenium.WebDriver.MSEdgeDriver.

3. Guia de Ejecucion
El sistema requiere que la aplicacion web este activa para que el bot de Selenium pueda interactuar con la interfaz. Siga estos pasos en orden:

Paso 1: Levantar la Aplicacion (Terminal 1)
Desde la carpeta raiz del proyecto:

PowerShell
dotnet run
La aplicacion debe estar disponible en: http://localhost:5068.

Paso 2: Ejecutar Suite de Pruebas (Terminal 2)
Navegue a la carpeta del proyecto de pruebas y ejecute el siguiente comando:

PowerShell
cd Tarea4SeleniumTests
dotnet test --logger "html;LogFileName=ReporteSeleniumFinal.html"
4. Resultados y Evidencias
Al finalizar la ejecucion, el sistema genera automaticamente las siguientes evidencias requeridas para la evaluacion:

Reporte HTML: Se genera en la carpeta Tarea4SeleniumTests/TestResults/. Muestra el estado "Passed" de los 15 casos.

Capturas de Pantalla: Se almacenan en Tarea4SeleniumTests/bin/Debug/net10.0/Capturas/. Incluyen evidencia visual de cada validacion realizada por el bot.

Log de Consola: Detalla el tiempo de ejecucion y los asserts validados.