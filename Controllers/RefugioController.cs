using Microsoft.AspNetCore.Mvc;
using RefugioAnimales.Models;

namespace RefugioAnimales.Controllers
{
    /*
     Controllers : Dónde se reciben las peticiones del usuario y/o datos, por lo tanto se decide que hacer con ellos y como tratarlos. Además de elegir la vista adecuada, etc.
     Models : Dónde se manejan los datos y lógica.
     Views : Parte visual de la aplicación y de interacción con el usuario.
     */
    
    public class RefugioController : Controller
    {
        //[Route("")]
        public IActionResult Inicio()
        {
            // Mostrará la página principal.
            //Crear Animales
            Animal.crearAnimales();

            return View();
        }

        //[Route("Animales")]
        public IActionResult Animales()
        {
            // Devolverá un listado de animales almacenados en una lista en memoria.

            return View(Animal.getAnimales());
        }

        //[Route("Animales/Detalle/{id?}")]
        public IActionResult Detalle(int id)
        {
            // Mostrará la ficha completa de un animal según su id.

            return View(Animal.getAnimal(id));
        }
    }
}
