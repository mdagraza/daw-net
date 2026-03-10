using Microsoft.AspNetCore.Mvc.Rendering;
using RefugioAnimales.Models;
using System.ComponentModel;

namespace RefugioAnimales.ViewModel
{
    public class PanelUsuarioEditViewModel
    {
        public Usuario Usuario { get; set; }

        public Usuario UsuarioEdit { get; set; }

        [DisplayName("Roles")]
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
