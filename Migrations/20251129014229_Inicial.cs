using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RefugioAnimales.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adoptantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAlta = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adoptantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoAnimal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoAnimal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Animales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Especie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FotoContenido = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FotoMimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdoptanteId = table.Column<int>(type: "int", nullable: true),
                    FechaAdopcion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animales_Adoptantes_AdoptanteId",
                        column: x => x.AdoptanteId,
                        principalTable: "Adoptantes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Animales_EstadoAnimal_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "EstadoAnimal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Adoptantes",
                columns: new[] { "Id", "Email", "FechaAlta", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "JuanPerez@email.com", new DateOnly(2024, 12, 22), "Juan Pérez", "612345678" },
                    { 2, "MariaPerez@email.com", new DateOnly(2024, 5, 11), "María Pérez", "612345679" },
                    { 3, "JuanMariaPerez@email.com", new DateOnly(2023, 7, 3), "Juan María Pérez", "712345678" }
                });

            migrationBuilder.InsertData(
                table: "EstadoAnimal",
                columns: new[] { "Id", "Estado" },
                values: new object[,]
                {
                    { 1, "Disponible" },
                    { 2, "Adoptado" },
                    { 3, "En cuarentena" }
                });

            migrationBuilder.InsertData(
                table: "Animales",
                columns: new[] { "Id", "AdoptanteId", "Descripcion", "Edad", "Especie", "EstadoId", "FechaAdopcion", "FotoContenido", "FotoMimeType", "Nombre" },
                values: new object[,]
                {
                    { 1, null, "Le gusta volar, no puede parar.", 2, "Colibrí", 1, null, new byte[0], null, "Picudo" },
                    { 2, 1, "Siempre esta pinchando cuando puede.", 4, "Erizo", 2, new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[0], null, "Pinchitos" },
                    { 3, null, "Canta la opera como si fuera su último día.", 6, "Loro", 1, null, new byte[0], null, "Nemo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animales_AdoptanteId",
                table: "Animales",
                column: "AdoptanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Animales_EstadoId",
                table: "Animales",
                column: "EstadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animales");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Adoptantes");

            migrationBuilder.DropTable(
                name: "EstadoAnimal");
        }
    }
}
