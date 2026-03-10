# Aplicacion Web Refugio de Animales - ASP.NET Core MVC

Este proyecto es una aplicacion web completa para la gestion de un refugio de animales. Permite registrar animales disponibles para adopcion (incluyendo datos basicos y su foto), gestionar adoptantes, administrar cada proceso de adopcion/desadopcion y controlar el acceso mediante un sistema de usuarios.

## Caracteristicas del Proyecto

El sistema integra las siguientes funcionalidades desarrolladas bajo el patron MVC:

### Gestion de Animales (CRUD Completo)

- Visualizacion de un listado general de animales y acceso a fichas detalladas con nombre, especie, edad y estado.
- Registro de nuevos animales con sistema de subida de imagenes almacenadas directamente en la base de datos como campos binarios.
- Edicion y eliminacion de registros existentes con validaciones de modelo.

### Gestion de Adoptantes y Adopciones

- Mantenimiento de registros de adoptantes con informacion de contacto y fecha de alta.
- Flujo de adopcion: Permite asignar un adoptante y fecha a un animal, cambiando su estado a Adoptado automaticamente.
- En la ficha de cada adoptante es posible visualizar historicamente los animales que ha adoptado.
- Funcionalidad de desadopcion para restablecer el estado del animal y liberar la asignacion.
- Bloqueos de seguridad: La interfaz e infraestructura impiden adoptar un animal que ya tiene dueño asignado.

### Seguridad y Acceso

- Sistema de Login y Autenticacion basado en Cookies (Cookie Authentication) y proteccion de API mediante tokens JWT.
- Gestion de usuarios con contraseñas cifradas utilizando BCrypt.Net (Hash y Salt).
- Diferenciacion de roles: El administrador posee privilegios exclusivos para gestionar otras cuentas desde el Panel de Usuario.

## Tecnologias Utilizadas

- Framework: ASP.NET Core MVC.
- Persistencia: Entity Framework Core (Enfoque Code First).
- Base de Datos: SQL Server.
- Frontend: Razor Views, Bootstrap para diseño responsivo y TagHelpers.
- Seguridad: BCrypt.Net y Autenticacion JWT.

## Estructura de Datos (Modelado)

- Animal: Identificador, datos descriptivos, contenido de foto (byte[]), tipo MIME de foto, referencia al adoptante y fecha de adopcion.
- Adoptante: Identificador, datos de contacto y fecha de alta en el sistema.
- Usuario: Credenciales de acceso, PasswordHash+Salt y rol asignado.

## Instrucciones de Ejecucion

Siga estos pasos para configurar el proyecto en su entorno local:

1. Abrir la solucion en Visual Studio 2022 o superior.
2. Antes de iniciar, es necesario crear la base de datos aplicando las migraciones existentes. En la Consola del Administrador de Paquetes, ejecute:
   `Update-Database`
3. Iniciar la aplicacion (Build & Run). El sistema ejecutara un Seed de datos cargando automaticamente:
   - 3 animales con imagenes inicializadas desde `/SeedImages`.
   - 3 adoptantes de prueba.
   - 2 usuarios preconfigurados.

## Datos de Acceso para Pruebas

Puede utilizar las siguientes cuentas para explorar las funcionalidades:

- Administrador:
  - Usuario: `admin`
  - Contraseña: `admin123`
- Usuario Normal:
  - Usuario: `user`
  - Contraseña: `user123`

---

Proyecto desarrollado para la asignatura de Desarrollo Web en Entorno Servidor del Ciclo Superior de Desarrollo de Aplicaciones Web.
