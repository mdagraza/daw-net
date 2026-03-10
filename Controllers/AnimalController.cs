using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RefugioAnimales.Models;
using RefugioAnimales.Models.BBDD;
using RefugioAnimales.ViewModel;

namespace RefugioAnimales.Controllers
{
    [Authorize]
    [Route("animales")]
    public class AnimalController : Controller
    {
        //Dependencias
        private readonly AnimalManager _animalManager;
        public AnimalController(AnimalManager animalManager)
        {
            _animalManager = animalManager;
        }

        [Route("lista")]
        public IActionResult Animales()
        {
            return View(_animalManager.GetAllAnimalsConAdoptantes());
        }

        [Route("detalle/{id}")]
        public IActionResult Detalle(int id)
        {
            var animal = _animalManager.GetAnimalConAdoptanteById(id);

            if (animal == null)
            {
                return RedirectToAction("Animales");
            }

            return View(animal);
        }

        [Route("editar/{id}")]
        public IActionResult Editar(int id)
        {
            var animal = _animalManager.GetAnimalConEstadosById(id);
            if (animal == null)
            {
                return RedirectToAction("Animales");
            }

            return View(animal);
        }

        [HttpPost]
        [Route("editar/{id}")]
        public IActionResult Editar(int id, Animal animal, IFormFile FotoContenido)
        {
            //Eliminar campos que no son necesarios validar
            ModelState.Remove("Estado");
            ModelState.Remove("FotoContenido");

            if (!ModelState.IsValid)
            {
                var animalVm = _animalManager.GetAnimalConEstadosById(id);
                if (animalVm == null)
                {
                    return RedirectToAction("Animales");
                }
                animalVm.Nombre = animal.Nombre;
                animalVm.Especie = animal.Especie;
                animalVm.Edad = animal.Edad;
                animalVm.EstadoId = animal.EstadoId;
                animalVm.Descripcion = animal.Descripcion;
                animalVm.FechaAdopcion = animal.FechaAdopcion;
                return View(animalVm);
            }

            //Convertir archivo a byte[]
            if (FotoContenido != null && FotoContenido.Length > 0)
            {
                var ms = new MemoryStream();
                FotoContenido.CopyTo(ms);
                animal.FotoContenido = ms.ToArray();
                animal.FotoMimeType = FotoContenido.ContentType;
            }

            _animalManager.UpdateAnimal(animal);

            TempData["Mensaje"] = "El animal se actualizó correctamente";
            return RedirectToAction("Editar", id);
        }

        [Route("crear")]
        public IActionResult Crear()
        {
            ViewBag.Estados = _animalManager.GetEstados();
            return View();
        }

        [HttpPost]
        [Route("crear")]
        public IActionResult Crear(Animal animal, IFormFile FotoContenido)
        {
            ModelState.Remove("FotoContenido");
            ModelState.Remove("Estado");
            if (!ModelState.IsValid || FotoContenido == null)
            {
                ViewBag.Estados = _animalManager.GetEstados();
                return View(animal);
            }

            //Convertir archivo a byte[]
            if (FotoContenido != null && FotoContenido.Length > 0)
            {
                var ms = new MemoryStream();
                FotoContenido.CopyTo(ms);
                animal.FotoContenido = ms.ToArray();
                animal.FotoMimeType = FotoContenido.ContentType;
            }
            _animalManager.AddAnimal(animal);
            return RedirectToAction("Detalle", new { id = animal.Id });
        }

        [Route("eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            var animal = _animalManager.GetAnimalConAdoptanteById(id);
            if (animal == null)
            {
                return RedirectToAction("Animales");
            }

            return View(animal);
        }

        [HttpPost]
        [Route("eliminar/{id}")]
        public IActionResult Eliminar(Animal animal)
        {
            _animalManager.DeleteAnimal(animal.Id);
            return RedirectToAction("Animales");
        }

        [Route("adoptar/{id}")]
        public IActionResult Adoptar(int id)
        {
            var animal = _animalManager.GetAnimalConAdoptantes(id);

            if (animal == null)
            {
                return RedirectToAction("Animales");
            }

            if (animal.AdoptanteId != null)
            {
                return RedirectToAction("Desadoptar", new { id = animal.Id });
            }

            return View(animal);
        }

        [HttpPost]
        [Route("adoptar/{id}")]
        public IActionResult Adoptar(int id, AnimalAdoptarViewModel animal)
        {
            ModelState.Remove("Nombre");
            if (!ModelState.IsValid)
            {
                animal.Adoptantes = _animalManager.GetAnimalConAdoptantes(id).Adoptantes;
                return View(animal);
            }

            _animalManager.UpdateAdoptanteAnimal(animal);

            return RedirectToAction("Detalle", new { id = animal.Id });
        }

        [Route("desadoptar/{id}")]
        public IActionResult Desadoptar(int id)
        {
            var animal = _animalManager.GetAnimalConAdoptanteById(id);

            if (animal == null)
            {
                return RedirectToAction("Animales");
            }

            if (animal.AdoptanteId == null)
            {
                return RedirectToAction("Adoptar", new { id = animal.Id });
            }

            return View(animal);
        }

        [HttpPost]
        [Route("desadoptar/{id}")]
        public IActionResult Desadoptar(int id, AnimalAdoptarViewModel animal) //En desadoptar se usa el AnimalViewModel
        {

            animal.AdoptanteId = null;
            _animalManager.UpdateAdoptanteAnimal(animal);

            return RedirectToAction("Animales");
        }

    }
}
