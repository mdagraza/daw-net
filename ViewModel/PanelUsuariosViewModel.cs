using RefugioAnimales.Models;
using System.ComponentModel;

namespace RefugioAnimales.ViewModel
{
    public class PanelUsuariosViewModel
    {
        public Usuario Usuario { get; set; }

        public List<Usuario>? Usuarios { get; set; }
    }
}
