Tarea 3 - Refugio de Animales

Descripción del proyecto :
	Aplicación web para la gestión de un refugio de animales. Permite registrar animales disponibles para adopción (incluyendo datos básicos y su foto), gestionar
	adoptantes (información basica de adoptante y datos de contacto), gestionar cada adopción / desadopción y los usuarios del sistema.
	En cada adoptante, es posible visualizar los animales que ha adoptado.

Antes de iniciar, necesario crear la base de datos aplicando las migraciones. [Migraciones ya creadas]
En la consola del Administrador de paquetes de Visual Studio, ejecutar :
	· Update-Database

Datos iniciales :
	· Tres animales con imágenes que se inicializan desde /SeedImages
	· Tres adoptantes
	· Dos usuarios :
		- Admin (User: admin | Pass: admin123)
		- Usuario normal (User: user | Pass: user123)
		# La diferencia entre usuarios es que el admin puede gestionar los demás usuarios desde el Panel usuario.

Tecnologías utilizadas :
	· ASP.NET Core MVC
	· Entity Framework Core
	· SQL Server
	· Bootstrap
	· BCrypt.Net
	· Cookie Authentication