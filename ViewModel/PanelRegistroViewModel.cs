using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RefugioAnimales.ViewModel
{
    public class PanelRegistroViewModel
    {
        [DisplayName("Usuario")]
        [Required(ErrorMessage = "Debes poner un nombre de usuario")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Debe tener entre 3 y 50 caracteres")]
        public string Usuario { get; set; }

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "Debes poner una contraseña")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres.")]
        public string Pass1 { get; set; }

        [DisplayName("Contraseña (repetir)")]
        [Required(ErrorMessage = "Debes poner una contraseña")]
        [Compare("Pass1", ErrorMessage = "Las contraseñas no coinciden.")]
        [MinLength(5, ErrorMessage = "Debe debe tener al menos 5 caracteres.")]
        public string Pass2 { get; set; }
    }
}
