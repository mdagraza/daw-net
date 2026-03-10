using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RefugioAnimales.ViewModel
{
    public class AdoptanteAnimalesViewModel
    {
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debes poner un nombre")]
        [DisplayName("Nombre")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Debe tener entre 3 y 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debes poner una dirección de correo")]
        [DisplayName("Dirección de correo electrónico")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", ErrorMessage = "La dirección de correo no es válida")] //[EmailAddress] Permite un correo tipo aaa@aaa (No comprueba el punto)
        public string Email { get; set; }

        [Required(ErrorMessage = "Debes poner un teléfono")]
        [DisplayName("Teléfono")]
        [RegularExpression(@"^[6789][0-9]{8}$", ErrorMessage = "Debe ser un número de teléfono válido")] //Solo números de 9 dígitos que empiecen por 6,7,8 o 9
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Debes poner una fecha de alta")]
        [DataType(DataType.Date)]
        [DisplayName("Fecha de alta")]
        public DateOnly FechaAlta { get; set; }

        [DisplayName("Animales adoptados")]
        public List<Models.Animal>? Animales { get; set; }
    }
}
