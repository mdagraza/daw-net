using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using RefugioAnimales.Models.BBDD;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<AnimalManager>();
builder.Services.AddScoped<AdoptanteManager>();
builder.Services.AddScoped<PanelManager>();
builder.Services.AddScoped<AuthManager>();

builder.Services.AddDbContext<RefugioContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection")));

// Cookie Auth
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Panel/Login";
        opt.LogoutPath = "/Panel/Logout";
        opt.AccessDeniedPath = "/Panel/AccessDenied";
        opt.SlidingExpiration = true;
        opt.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddAuthorization();

var app = builder.Build();

//Seed images
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<RefugioContext>();

if (db.Animales.Any(a => a.Id == 1 && a.FotoContenido.Length == 0))
{

    var animales = db.Animales.ToList();

    //Ubicacion imagenes
    var basePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedImages");

    foreach (var animal in animales)
    {
        //Buscar la imagen
        var imagen = Directory.GetFiles(basePath, $"{animal.Id}_*")[0];

        //Convertir imagen
        animal.FotoContenido = File.ReadAllBytes(imagen);

        //Obtener mime
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(imagen, out string mime))
        {
            mime = "application/octet-stream"; // por si falla, añade este [un tipo genérico que se usa cuando no se conoce el tipo real del contenido]
        }
        animal.FotoMimeType = mime;

    }

    db.SaveChanges();

}

//Seed contraseña admin y user
if (db.Usuarios.Any(u => u.NombreUsuario == "admin" && u.PasswordHash == ""))
{
    var uAdmin = db.Usuarios.First(u => u.NombreUsuario == "admin");
    string adminHash = BCrypt.Net.BCrypt.HashPassword("admin123");
    uAdmin.PasswordHash = adminHash;

    db.SaveChanges();
}
if (db.Usuarios.Any(u => u.NombreUsuario == "user" && u.PasswordHash == ""))
{
    var uAdmin = db.Usuarios.First(u => u.NombreUsuario == "user");
    string adminHash = BCrypt.Net.BCrypt.HashPassword("user123");
    uAdmin.PasswordHash = adminHash;

    db.SaveChanges();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); //Necesario para usar los archivos de wwwroot

app.UseRouting();

app.UseAuthorization();

app.MapControllers(); //Attribute routing

app.Run();
