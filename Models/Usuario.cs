using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RefugioAnimales.Models
{
    public class Usuario
    {
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Nombre de Usuario")]
        [Required(ErrorMessage = "Debes poner un nombre")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Debe tener entre 3 y 50 caracteres")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Debes poner una contraseña")]
        [DisplayName("Contraseña")]
        [MinLength(5, ErrorMessage = "Debe tener al menos 5 caracteres.")]
        public string PasswordHash { get; set; }

        //public string Salt { get; set; }

        [Required]
        [DisplayName("Rol")]
        public string Rol { get; set; }
    }
}
