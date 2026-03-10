using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

/*
    • Una ruta que permita acceder al listado de animales escribiendo la URL /Animales.
    • Otra ruta que permita acceder al detalle de un animal concreto (por ejemplo /Animales/Detalle/1).
*/

/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Refugio}/{action=Inicio}/{id?}");
*/

//Se crea una ruta para cada Action en particular, más tedioso, pero más controlado
app.MapControllerRoute(
    name: "default",
    pattern: "",
    defaults: new { controller = "Refugio", action = "Inicio" }
);
app.MapControllerRoute(
    name: "animales",
    pattern: "Animales",
    defaults: new { controller = "Refugio", action = "Animales" }
);
app.MapControllerRoute(
    name: "detalle",
    pattern: "Animales/Detalle/{id?}",
    defaults: new { controller = "Refugio", action = "Detalle" }
);

//Crea las rutas, pero al ser tan generico, permite rutas como Animales/Animales, o Detalle sin más
/*app.MapControllerRoute(
    name: "default",
    pattern: "{action=Inicio}",
    defaults: new { controller = "Refugio" }
);
app.MapControllerRoute(
    name: "default",
    pattern: "Animales/{action=Detalle}/{id?}",
    defaults: new { controller = "Refugio" }
);*/


//app.MapControllers(); // Para Attribute Routing

app.Run();
