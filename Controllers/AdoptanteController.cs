using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RefugioAnimales.Models;
using RefugioAnimales.Models.BBDD;

namespace RefugioAnimales.Controllers
{
    [Authorize]
    [Route("adoptante")]
    public class AdoptanteController : Controller
    {
        //Dependencias
        private readonly AdoptanteManager _adoptanteManager;
        public AdoptanteController(AdoptanteManager adoptanteManager)
        {
            _adoptanteManager = adoptanteManager;
        }

        [Route("lista")]
        public IActionResult Adoptantes()
        {
            return View(_adoptanteManager.GetAllAdoptantes());
        }

        [Route("crear")]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [Route("crear")]
        public IActionResult Crear(Adoptante adoptante)
        {
            if (!ModelState.IsValid)
            {
                return View(adoptante);
            }

            //Comprobar email y número
            ComprobarEmailYTelefono(adoptante);
            if (TempData.ContainsKey("Error"))
            {
                return View(adoptante);
            }

            _adoptanteManager.AddAdoptante(adoptante);
            return RedirectToAction("Detalle", new { id = adoptante.Id });
        }

        [Route("detalle/{id}")]
        public IActionResult Detalle(int id)
        {
            var adoptante = _adoptanteManager.GetAdoptanteConAnimalesById(id);
            if (adoptante == null)
            {
                return RedirectToAction("Adoptantes");
            }
            return View(adoptante);
        }

        [Route("editar/{id}")]
        public IActionResult Editar(int id)
        {
            var adoptante = _adoptanteManager.GetAdoptanteById(id);
            if (adoptante == null)
            {
                return RedirectToAction("Adoptantes");
            }

            return View(adoptante);
        }

        [HttpPost]
        [Route("editar/{id}")]
        public IActionResult Editar(int id, Adoptante adoptante)
        {
            if (!ModelState.IsValid)
            {
                return View(adoptante);
            }

            //Comprobar email y número
            ComprobarEmailYTelefono(adoptante);
            if (TempData.ContainsKey("Error"))
            {
                return View(adoptante);
            }

            _adoptanteManager.UpdateAdoptante(adoptante);

            TempData["Mensaje"] = "El adoptante se actualizó correctamente";
            return RedirectToAction("Editar", new { id = adoptante.Id });
        }

        [Route("eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            var adoptante = _adoptanteManager.GetAdoptanteById(id);
            if (adoptante == null)
            {
                return RedirectToAction("Adoptantes");
            }

            return View(adoptante);
        }

        [HttpPost]
        [Route("eliminar/{id}")]
        public IActionResult Eliminar(Animal animal)
        {
            _adoptanteManager.DeleteAdoptante(animal.Id);
            return RedirectToAction("Adoptantes");
        }

        [NonAction]
        public void ComprobarEmailYTelefono(Adoptante adoptante)
        {
            if (_adoptanteManager.ComprobarEmailYTelefono(adoptante.Id, adoptante.Email, adoptante.Telefono) == 1)
            {
                TempData["Error"] = "Existe otro adoptante con esta dirección de correo";
            }
            else if (_adoptanteManager.ComprobarEmailYTelefono(adoptante.Id, adoptante.Email, adoptante.Telefono) == 2)
            {
                TempData["Error"] = "Existe otro adoptante con este número de teléfono";
            }
            else if (_adoptanteManager.ComprobarEmailYTelefono(adoptante.Id, adoptante.Email, adoptante.Telefono) == 3)
            {
                TempData["Error"] = "Existe otro adoptante con esta dirección de correo y este número de teléfono";
            }
        }

    }
}
