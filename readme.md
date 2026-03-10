# Aplicacion Web Refugio de Animales - ASP.NET Core MVC

Este proyecto es una aplicacion web completa desarrollada con ASP.NET Core MVC para la gestion integral de un refugio de animales. La aplicacion permite administrar el catalogo de animales, gestionar adoptantes, procesar adopciones y manejar la seguridad mediante autenticacion.

## Caracteristicas del Proyecto

El sistema ha sido desarrollado en dos fases, integrando las siguientes funcionalidades:

### Gestion de Animales (CRUD Completo)

- Visualizacion de un listado general de animales con acceso a fichas detalladas.
- Registro de nuevos animales incluyendo nombre, especie, edad, estado y descripcion.
- Sistema de subida de imagenes almacenadas directamente en la base de datos (campos binarios).
- Edicion y eliminacion de registros existentes.

### Gestion de Adoptantes y Adopciones

- Mantenimiento de registros de adoptantes (nombre, email, telefono y fecha de alta).
- Flujo de adopcion: Permite asignar un adoptante y una fecha a un animal, cambiando su estado a Adoptado de forma automatica.
- Funcion de desadopcion para restablecer el estado del animal y limpiar los datos de asignacion.
- Bloqueos de seguridad: La interfaz y el servidor impiden intentar adoptar un animal que ya tiene dueño.

### Seguridad y Acceso

- Sistema de Login y Autenticacion para la proteccion de rutas y funciones administrativas.
- Gestion de usuarios con contraseñas cifradas mediante tecnicas de hash y salt.
- Proteccion de la API mediante tokens JWT (JSON Web Tokens).

## Tecnologias Utilizadas

- Framework: ASP.NET Core 8.0 (Pattern Model-View-Controller).
- Persistencia: Entity Framework Core utilizando el enfoque Code First.
- Base de Datos: SQL Server / LocalDB.
- Seguridad: Autenticacion JWT y DataAnnotations para validacion de modelos en servidor y cliente.
- Frontend: Razor Views, HTML5, CSS y TagHelpers para la navegacion dinamica.

## Estructura de Datos (Modelado)

- Animal: Identificador, datos descriptivos, contenido de foto (byte[]), tipo MIME de foto, referencia al adoptante y fecha de adopcion.
- Adoptante: Identificador, datos de contacto y fecha de alta en el sistema.
- Usuario: Credenciales de acceso, PasswordHash, Salt y rol asignado.

## Instrucciones de Ejecucion

Para poner en marcha el proyecto en un entorno local, siga estos pasos:

1. Clonar o descargar el repositorio.
2. Abrir la solucion en Visual Studio 2022 o superior.
3. Ejecutar el comando Update-Database desde la Consola del Administrador de Paquetes para generar la base de datos mediante las migraciones incluidas.
4. Iniciar la aplicacion (Build & Run).
5. Al arrancar por primera vez, el sistema ejecutara un Seed de datos cargando automaticamente:
   - 3 animales de ejemplo con sus respectivas imagenes desde la carpeta SeedImages.
   - 2 adoptantes de prueba.
   - 1 usuario administrador para el acceso inicial.

---

Proyecto desarrollado para la asignatura de Desarrollo Web en Entorno Servidor del Ciclo Superior de Desarrollo de Aplicaciones Web.
