using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RefugioAnimales.ViewModel
{
    public class AnimalEditarViewModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debes poner un nombre")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Debe tener entre 3 y 50 caracteres")]
        public string Nombre { get; set; }

        [DisplayName("Especie")]
        [Required(ErrorMessage = "Debes poner una especie")]
        public string Especie { get; set; }

        [DisplayName("Edad")]
        [Required(ErrorMessage = "Debes poner una edad")]
        [Range(0, 100, ErrorMessage = "Debes poner un número entre 0 y 100")]
        public int Edad { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "Debes seleccionar un estado")]
        public int EstadoId { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Debes poner una descripción"), StringLength(255)]
        public string Descripcion { get; set; }

        [DisplayName("Foto")]
        public byte[]? FotoContenido { get; set; }

        public string? FotoMimeType { get; set; }

        [DisplayName("Fecha de adopción")]
        public DateTime? FechaAdopcion { get; set; }

        public IEnumerable<SelectListItem> EstadoLista { get; set; }

    }
}
