using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using RefugioAnimales.ViewModel;
using System.Collections.Generic;

namespace RefugioAnimales.Models.BBDD
{
    public class PanelManager
    {
        private readonly RefugioContext _context;
        public PanelManager(RefugioContext context)
        {
            _context = context;
        }

        public List<Usuario> GetAllUsers()
        {
            return _context.Usuarios.ToList();
        }

        public PanelUsuariosViewModel GetAllUsersConUser(int id)
        {
            return new PanelUsuariosViewModel
            {
                Usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id),
                Usuarios = _context.Usuarios.ToList()
            };
        }

        public Usuario? AccesoUsuario(string nombreUsuario, string password)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
            if (usuario == null || password == null)
            {
                return null;
            }

            var isValid = BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash);

            if (isValid)
                return usuario;

            return null;
        }

        public bool ComprobarUsuario(int id, string nombreUsuario)
        {
            return _context.Usuarios.Any(u => u.NombreUsuario == nombreUsuario && u.Id != id);
        }
        public bool ComprobarUsuario(string nombreUsuario)
        {
            return _context.Usuarios.Any(u => u.NombreUsuario == nombreUsuario);
        }

        public void RegistroUsuario(string user, string pass, string rol = Roles.User)
        {
            var _user = new Usuario
            {
                NombreUsuario = user,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(pass),
                Rol = rol
            };

            _context.Usuarios.Add(_user);
            _context.SaveChanges();
        }

        public void CambiarPass(int id, string pass)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(pass);

            _context.Usuarios.Update(user);
            _context.SaveChanges();

            /*
             _context.Animales.Update(animal);
            if (animal.FotoContenido == null) //Al hacer Update, marcada toda la entidad como modificada, si no hay foto, se marcan como no modificadas para no sobrescribir.
            {
                _context.Entry(animal).Property(a => a.FotoContenido).IsModified = false;
                _context.Entry(animal).Property(a => a.FotoMimeType).IsModified = false;
            }
            _context.Entry(animal).Property(a => a.AdoptanteId).IsModified = false;

            _context.SaveChanges();
             
             */
        }

        public Usuario? GetUsuarioById(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public PanelUsuarioEditViewModel? GetUsuarioConUserById(int id, int idEdit)
        {
            var viewmodel = new PanelUsuarioEditViewModel
            {
                Usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id),
                UsuarioEdit = _context.Usuarios.FirstOrDefault(u => u.Id == idEdit),
            };

            viewmodel.Roles = Roles.All.Select(r => new SelectListItem
            {
                Value = r,
                Text = r
            }).ToList();

            return viewmodel;
        }

        public PanelContrasenaViewModel? GetUsuarioContrasenaById(int id)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (user == null) return null;

            return new PanelContrasenaViewModel
            {
                Id = user.Id,
                Usuario = user.NombreUsuario
            };
        }

        public void DeteleUser(int id)
        {
            var user = _context.Usuarios.FirstOrDefault(a => a.Id == id);
            if (user != null)
            {
                _context.Usuarios.Remove(user);
                _context.SaveChanges();
            }
        }

        public void UpdateUser(Usuario user)
        {
            _context.Usuarios.Update(user);

            if (user.PasswordHash == null) //Si password es null, es porque solo se debe actualizar nombre y rol
            {
                _context.Entry(user).Property(a => a.PasswordHash).IsModified = false;
            }
            else
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            }

                _context.SaveChanges();
        }

    }
}
