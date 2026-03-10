using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RefugioAnimales.ViewModel;

namespace RefugioAnimales.Models.BBDD
{
    public class AnimalManager
    {
        private readonly RefugioContext _context;
        public AnimalManager(RefugioContext context)
        {
            _context = context;
        }

        public List<Animal> GetAllAnimals()
        {
            return _context.Animales.ToList();
        }

        public List<AnimalViewModel> GetAllAnimalsConAdoptantes()
        {
            var animales = _context.Animales.Include(a => a.Adoptante).Include(a => a.Estado).ToList();
            return animales.Select(a => new AnimalViewModel
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Especie = a.Especie,
                Edad = a.Edad,
                Estado = a.Estado != null ? a.Estado.Estado : "---",
                Descripcion = a.Descripcion,
                FotoContenido = a.FotoContenido,
                FotoMimeType = a.FotoMimeType,
                FechaAdopcion = a.FechaAdopcion,
                AdoptanteId = a.AdoptanteId,
                AdoptanteNombre = a.Adoptante != null ? a.Adoptante.Nombre : "---"
            }).ToList();
        }

        public Animal? GetAnimalById(int id)
        {
            return _context.Animales.FirstOrDefault(a => a.Id == id);
        }

        public AnimalViewModel? GetAnimalConAdoptanteById(int id)
        {
            var animal = _context.Animales
                .Include(a => a.Adoptante)
                .Include(a => a.Estado)
                .FirstOrDefault(a => a.Id == id);
            if (animal == null)
            {
                return null;
            }

            return new AnimalViewModel
            {
                Id = animal.Id,
                Nombre = animal.Nombre,
                Especie = animal.Especie,
                Edad = animal.Edad,
                Estado = animal.Estado != null ? animal.Estado.Estado : "---",
                Descripcion = animal.Descripcion,
                FotoContenido = animal.FotoContenido,
                FotoMimeType = animal.FotoMimeType,
                FechaAdopcion = animal.FechaAdopcion,
                AdoptanteId = animal.AdoptanteId,
                AdoptanteNombre = animal.Adoptante != null ? animal.Adoptante.Nombre : "---"
            };
        }

        public AnimalEditarViewModel? GetAnimalConEstadosById(int id)
        {
            var animal = _context.Animales
                .Include(a => a.Adoptante)
                .Include(a => a.Estado)
                .FirstOrDefault(a => a.Id == id);

            if (animal == null)
            {
                return null;
            }

            var idsIgnorados = new int[] { 2 }; // Por defecto se ignora adoptado, ya que en Editar no se adopta. Pero si esta adoptado, no se puede cambiar a otro estado.
            if (animal.EstadoId == 2)
            {
                idsIgnorados = new int[] { 1, 3 };
            }

            var estadosLista = _context.EstadoAnimal.Where(e => !idsIgnorados.Contains(e.Id)).Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Estado,
                Selected = (e.Id == animal.EstadoId)
            }).ToList();

            return new AnimalEditarViewModel
            {
                Id = animal.Id,
                Nombre = animal.Nombre,
                Especie = animal.Especie,
                Edad = animal.Edad,
                EstadoId = animal.Estado.Id,
                Descripcion = animal.Descripcion,
                FotoContenido = animal.FotoContenido,
                FotoMimeType = animal.FotoMimeType,
                FechaAdopcion = animal.FechaAdopcion,
                EstadoLista = estadosLista
            };
        }

        public IEnumerable<SelectListItem> GetEstados()
        {
            //No se pasa Adoptado
            return _context.EstadoAnimal.Where(e => e.Id != 2).Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Estado,
                Selected = e.Id == 1
            }).ToList();

            
        }

        public AnimalAdoptarViewModel? GetAnimalConAdoptantes(int id)
        {
            var animal = _context.Animales
                .Include(a => a.Adoptante)
                .Include(a => a.Estado)
                .FirstOrDefault(a => a.Id == id);

            if (animal == null)
            {
                return null;
            }

                var adoptantes = _context.Adoptantes.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Nombre
            }).ToList();

            adoptantes.Insert(0, new SelectListItem
            {
                Value = "",
                Text = " "
            });

            return new AnimalAdoptarViewModel
            {
                Id = animal.Id,
                Nombre = animal.Nombre,
                AdoptanteId = animal.AdoptanteId,
                Adoptantes = adoptantes
            };
        }

        public void AddAnimal(Animal animal)
        {
            _context.Animales.Add(animal);
            if (animal.FotoContenido == null)
            {
                animal.FotoContenido = new byte[] { 0x00 };
            }
            _context.SaveChanges();
        }

        public void UpdateAnimal(Animal animal)
        {

            _context.Animales.Update(animal);
            if (animal.FotoContenido == null) //Al hacer Update, marcada toda la entidad como modificada, si no hay foto, se marcan como no modificadas para no sobrescribir.
            {
                _context.Entry(animal).Property(a => a.FotoContenido).IsModified = false;
                _context.Entry(animal).Property(a => a.FotoMimeType).IsModified = false;
            }
            _context.Entry(animal).Property(a => a.AdoptanteId).IsModified = false;

            _context.SaveChanges();
        }

        public void UpdateAdoptanteAnimal(AnimalAdoptarViewModel animal)
        {   
            var _animal = _context.Animales.FirstOrDefault(a => a.Id == animal.Id);

            if (_animal == null)
            {
                return;
            }

            _animal.AdoptanteId = animal.AdoptanteId;

            if(animal.AdoptanteId == null)
            {
                _animal.EstadoId = 1;
                _animal.FechaAdopcion = null;
            }
            else
            {
                _animal.EstadoId = 2;
                _animal.FechaAdopcion = animal.FechaAdopcion;
            }

            _context.SaveChanges();
        }

        public void DeleteAnimal(int id)
        {
            var animal = GetAnimalById(id);
            if (animal != null)
            {
                _context.Animales.Remove(animal);
                _context.SaveChanges();
            }
        }

    }
}
