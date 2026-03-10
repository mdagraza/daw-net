using Microsoft.AspNetCore.Mvc.Rendering;
using RefugioAnimales.ViewModel;

namespace RefugioAnimales.Models.BBDD
{
    public class AdoptanteManager
    {
        private readonly RefugioContext _context;
        public AdoptanteManager(RefugioContext context)
        {
            _context = context;
        }

        public List<Adoptante> GetAllAdoptantes()
        {
            return _context.Adoptantes.ToList();
        }

        public Adoptante? GetAdoptanteById(int id)
        {
            return _context.Adoptantes.FirstOrDefault(a => a.Id == id);
        }

        public AdoptanteAnimalesViewModel? GetAdoptanteConAnimalesById(int id)
        {
            var animales = _context.Animales.Where(a => a.AdoptanteId == id).ToList();
            var adoptante = _context.Adoptantes.FirstOrDefault(a => a.Id == id);

            if(adoptante == null)
            {
                return null;
            }

            return new AdoptanteAnimalesViewModel
            {
                Id = adoptante.Id,
                Nombre = adoptante.Nombre,
                Email = adoptante.Email,
                Telefono = adoptante.Telefono,
                FechaAlta = adoptante.FechaAlta,
                Animales = animales
            };
        }

        public void AddAdoptante(Adoptante adoptante)
        {
            _context.Adoptantes.Add(adoptante);
            _context.SaveChanges();
        }

        public void UpdateAdoptante(Adoptante adoptante)
        {
            _context.Adoptantes.Update(adoptante);
            _context.SaveChanges();
        }

        public void DeleteAdoptante(int id)
        {
            var adoptante = _context.Adoptantes.FirstOrDefault(a => a.Id == id);
            if (adoptante != null)
            {
                _context.Adoptantes.Remove(adoptante);
                _context.SaveChanges();
            }
        }

        public int ComprobarEmailYTelefono(int id, string email, string telefono)
        {
            var emailExiste = _context.Adoptantes.Where(a => a.Id != id).Any(a => a.Email == email) ? 1 : 0;
            var telefonoExiste = _context.Adoptantes.Where(a => a.Id != id).Any(a => a.Telefono == telefono) ? 2 : 0;
            return emailExiste + telefonoExiste;
        }
    }
}
