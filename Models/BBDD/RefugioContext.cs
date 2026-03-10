using Microsoft.EntityFrameworkCore;
using RefugioAnimales.ViewModel;

namespace RefugioAnimales.Models.BBDD
{
    public class RefugioContext : DbContext
    {
        public RefugioContext(DbContextOptions<RefugioContext> options) : base(options)
        {
        }
        public DbSet<Animal> Animales { get; set; }
        public DbSet<Adoptante> Adoptantes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<EstadoAnimal> EstadoAnimal { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed Animales
            modelBuilder.Entity<Animal>().HasData(
                new Animal
                {
                    Id = 1,
                    Nombre = "Picudo",
                    Especie = "Colibrí",
                    Edad = 2,
                    EstadoId = 1,
                    Descripcion = "Le gusta volar, no puede parar.",
                    FotoContenido = Array.Empty<byte>(),
                    FotoMimeType = null,
                    AdoptanteId = null,
                    FechaAdopcion = null
                },
                new Animal
                {
                    Id = 2,
                    Nombre = "Pinchitos",
                    Especie = "Erizo",
                    Edad = 4,
                    EstadoId = 2,
                    Descripcion = "Siempre esta pinchando cuando puede.",
                    FotoContenido = Array.Empty<byte>(),
                    FotoMimeType = null,
                    AdoptanteId = 1,
                    FechaAdopcion = new DateTime(2025, 10, 25, 0, 0, 0)
                },
                new Animal
                {
                    Id = 3,
                    Nombre = "Nemo",
                    Especie = "Loro",
                    Edad = 6,
                    EstadoId = 1,
                    Descripcion = "Canta la opera como si fuera su último día.",
                    FotoContenido = Array.Empty<byte>(),
                    FotoMimeType = null,
                    AdoptanteId = null,
                    FechaAdopcion = null
                }
            );

            //Seed Adoptantes
            modelBuilder.Entity<Adoptante>().HasData(
                new Adoptante
                {
                    Id = 1,
                    Nombre = "Juan Pérez",
                    Email = "JuanPerez@email.com",
                    Telefono = "612345678",
                    FechaAlta = new DateOnly(2024, 12, 22)
                },
                new Adoptante
                {
                    Id = 2,
                    Nombre = "María Pérez",
                    Email = "MariaPerez@email.com",
                    Telefono = "612345679",
                    FechaAlta = new DateOnly(2024, 5, 11)
                },
                new Adoptante
                {
                    Id = 3,
                    Nombre = "Juan María Pérez",
                    Email = "JuanMariaPerez@email.com",
                    Telefono = "712345678",
                    FechaAlta = new DateOnly(2023, 7, 3)
                });

            //Seed Estado
            modelBuilder.Entity<EstadoAnimal>().HasData(
                new EstadoAnimal
                {
                    Id = 1,
                    Estado = "Disponible"
                },
                new EstadoAnimal
                {
                    Id = 2,
                    Estado = "Adoptado"
                },
                new EstadoAnimal
                {
                    Id = 3,
                    Estado = "En cuarentena"
                });

            //Seed Usuario
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    NombreUsuario = "admin",
                    PasswordHash = "", //Se añade a posterior, ya que poner aqui el HashPassword da problemas
                    Rol = Roles.Admin
                },
                new Usuario
                {
                    Id = 2,
                    NombreUsuario = "user",
                    PasswordHash = "", //Se añade a posterior, ya que poner aqui el HashPassword da problemas
                    Rol = Roles.User
                });

        }
    }
}
