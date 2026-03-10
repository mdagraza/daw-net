using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using RefugioAnimales.Models;
using RefugioAnimales.Models.BBDD;
using RefugioAnimales.ViewModel;
using System.Collections.Generic;

namespace RefugioAnimales.Controllers
{
    [Route("panel")]
    public class PanelController : Controller
    {
        private readonly PanelManager _panelManager;
        private readonly AuthManager _auth;
        public PanelController(PanelManager panelManager, AuthManager auth)
        {
            _panelManager = panelManager;
            _auth = auth;
        }

        [Route("")]
        public IActionResult Panel()
        {
            var userId = _auth.GetCurrentUserId(User);
            if (userId is null) return Forbid();

            return View(_panelManager.GetUsuarioContrasenaById(userId.Value));
        }

        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return RedirectToAction("Index", "Home");
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("login")]
        public IActionResult Login(Usuario user, string? returnUrl = null)
        {
            var userOk = _panelManager.AccesoUsuario(user.NombreUsuario, user.PasswordHash);
            if (userOk != null)
            {
                _auth.SignIn(HttpContext, userOk);

                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }

            TempData["Mensaje"] = "Credenciales inválidas.";
            user.PasswordHash = string.Empty;
            return View(user);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("logout")]
        public IActionResult Logout()
        {
            _auth.SignOut(HttpContext);
            return RedirectToAction("Login");
        }

        [Route("Registro")]
        public IActionResult Registro()
        {
            return View();
        }

        [Route("Registro")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Registro(PanelRegistroViewModel user)
        {
            if (!ModelState.IsValid)
            {

                user.Pass1 = string.Empty;
                user.Pass1 = string.Empty;
                return View(user);
            }

            //Filtros
            if (user.Usuario.ToLower().Contains("admin"))
            {
                TempData["Mensaje"] = "Nombre de usuario inválido.";
                user.Pass1 = string.Empty;
                user.Pass1 = string.Empty;
                return View(user);
            }
            if (_panelManager.ComprobarUsuario(user.Usuario))
            {
                TempData["Mensaje"] = "Nombre de usuario ya en uso.";
                user.Pass1 = string.Empty;
                user.Pass1 = string.Empty;
                return View(user);
            }

            _panelManager.RegistroUsuario(user.Usuario, user.Pass1);
            TempData["MensajeOk"] = "El registro se ha completado.";
            return RedirectToAction("Login");
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("cambiarPass")]
        public IActionResult CambiarContrasena(PanelContrasenaViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View("Panel", user);
            }

            //Comprobar que el Id y el usuario concuerdan
            var userCheck = _panelManager.GetUsuarioById(user.Id);
            if (userCheck == null || (user.Usuario != userCheck.NombreUsuario && user.Id != userCheck.Id)) {
                TempData["Mensaje"] = "Ha habido algún error. Vuelve a intentarlo.";
                return RedirectToAction("Panel");
            }

            //Comprobar contraseña ok
            if (_panelManager.AccesoUsuario(user.Usuario, user.Pass) == null)
            {
                TempData["Mensaje"] = "Contraseña incorrecta.";
                return View("Panel", user);
            }

            //Guardar nueva contraseña
            _panelManager.CambiarPass(user.Id, user.Pass1);
            TempData["MensajeOk"] = "Cambio de contraseña realizado.";
            return RedirectToAction("Panel");
        }

        [Authorize(Roles = Roles.Admin)]
        [Route("usuarios")]
        public IActionResult Usuarios()
        {
            var userId = _auth.GetCurrentUserId(User);
            if (userId is null) return Forbid();

            return View(_panelManager.GetAllUsersConUser(userId.Value));
        }

        [Authorize(Roles = Roles.Admin)]
        [Route("usuarios/detalle/{id}")]
        public IActionResult Detalle(int id)
        {
            var userId = _auth.GetCurrentUserId(User);
            if (userId is null) return Forbid();

            var user = _panelManager.GetUsuarioConUserById(userId.Value, id);
            if (user.UsuarioEdit is null)
            {
                return RedirectToAction("Usuarios");
            }

            return View(user);
        }

        [Authorize(Roles = Roles.Admin)]
        [Route("usuarios/editar/{id}")]
        public IActionResult Editar(int id)
        {
            var userId = _auth.GetCurrentUserId(User);
            if (userId is null) return Forbid();

            var user = _panelManager.GetUsuarioConUserById(userId.Value, id);
            if (user.UsuarioEdit is null)
            {
                return RedirectToAction("Usuarios");
            }

            //Limpiar contraseñas
            user.Usuario.PasswordHash = String.Empty;
            user.UsuarioEdit.PasswordHash = String.Empty;

            return View(user);
        }

        [Authorize(Roles = Roles.Admin)]
        [Route("usuarios/editar/{id}")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Editar(Usuario UsuarioEdit)
        {
            var userId = _auth.GetCurrentUserId(User);
            if (userId is null) return Forbid();

            ModelState.Remove("UsuarioEdit.PasswordHash");
            if (!ModelState.IsValid)
            {
                return View(_panelManager.GetUsuarioConUserById(userId.Value, UsuarioEdit.Id));
            }

            //Comprobar que usuario no existe
            if (_panelManager.ComprobarUsuario(UsuarioEdit.Id, UsuarioEdit.NombreUsuario))
            {
                TempData["Mensaje"] = "Nombre de usuario ya en uso.";
                return View(_panelManager.GetUsuarioConUserById(userId.Value, UsuarioEdit.Id));
            }

            //Editar
            _panelManager.UpdateUser(UsuarioEdit);

            //Si el usuario se cambia a si mismo el rol de Admin a User, habria que volver a hacer la cookie para actualizar el claim
            if (UsuarioEdit.Id == userId.Value && UsuarioEdit.Rol == Roles.User)
            {
                //_auth.SignOut(HttpContext);
                _auth.SignIn(HttpContext, _panelManager.GetUsuarioById(UsuarioEdit.Id));
                return RedirectToAction("Index", "Home");
            }

            TempData["MensajeOk"] = "Usuario actualizado.";
            return RedirectToAction("Editar", UsuarioEdit.Id);
        }

        [Authorize(Roles = Roles.Admin)]
        [Route("usuarios/eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            var userId = _auth.GetCurrentUserId(User);
            if (userId is null) return Forbid();

            var user = _panelManager.GetUsuarioConUserById(userId.Value, id);
            if (user.UsuarioEdit is null)
            {
                return RedirectToAction("Usuarios");
            }

            if (id == userId.Value) //El admin no se puede eliminar a si mismo
            {
                return RedirectToAction("Usuarios");
            }

            return View(user);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost, ValidateAntiForgeryToken]
        [Route("usuarios/eliminar/{id}")]
        public IActionResult ConfirmarEliminar(int id)
        {
            var userId = _auth.GetCurrentUserId(User);
            if (userId is null) return Forbid();

            if (id != userId.Value) //El admin no se puede eliminar a si mismo
            {
                _panelManager.DeteleUser(id);
            }

            return RedirectToAction("Usuarios");
        }

        [Authorize(Roles = Roles.Admin)]
        [Route("usuarios/crear")]
        public IActionResult Crear(int id)
        {
            var userId = _auth.GetCurrentUserId(User);
            if (userId is null) return Forbid();

            return View(_panelManager.GetUsuarioConUserById(userId.Value, 0));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost, ValidateAntiForgeryToken]
        [Route("usuarios/crear")]
        public IActionResult Crear(Usuario UsuarioEdit)
        {
            var userId = _auth.GetCurrentUserId(User);
            if (userId is null) return Forbid();

            //ModelState.Remove("Estado");
            if (!ModelState.IsValid)
            {
                return View(_panelManager.GetUsuarioConUserById(userId.Value, UsuarioEdit.Id));
            }

            //Comprobar que usuario no existe
            if (_panelManager.ComprobarUsuario(UsuarioEdit.NombreUsuario))
            {
                TempData["Mensaje"] = "Nombre de usuario ya en uso.";
                return View(_panelManager.GetUsuarioConUserById(userId.Value, UsuarioEdit.Id));
            }

            _panelManager.RegistroUsuario(UsuarioEdit.NombreUsuario, UsuarioEdit.PasswordHash, UsuarioEdit.Rol);

            return RedirectToAction("Usuarios");
        }
    }
}
