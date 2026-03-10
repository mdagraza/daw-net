using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RefugioAnimales.ViewModel
{
    public class AnimalViewModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Nombre")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Debe tener entre 3 y 50 caracteres")]
        public string Nombre { get; set; }
        [DisplayName("Especie")]
        public string Especie { get; set; }
        [DisplayName("Edad")]
        public int Edad { get; set; }
        [DisplayName("Estado")]
        public string Estado { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        [DisplayName("Foto")]
        public byte[] FotoContenido { get; set; }
        public string? FotoMimeType { get; set; }
        public int? AdoptanteId { get; set; }
        [DisplayName("Adoptante")]
        public string? AdoptanteNombre { get; set; }
        public DateTime? FechaAdopcion { get; set; }
    }
}
