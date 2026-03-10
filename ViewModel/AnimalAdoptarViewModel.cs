using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RefugioAnimales.ViewModel
{
    public class AnimalAdoptarViewModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Nombre")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Debe tener entre 3 y 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un adoptante")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un adoptante")]
        [DisplayName("Adoptante")]
        public int? AdoptanteId { get; set; }
        public IEnumerable<SelectListItem>? Adoptantes { get; set; }

        [Required(ErrorMessage = "Debes seleccionar una fecha")]
        [DisplayName("Fecha de adopción")]
        public DateTime? FechaAdopcion { get; set; }
    }
}
