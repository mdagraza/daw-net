using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RefugioAnimales.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [Route("/")]
        public IActionResult Index()
        {
            TempData["MensajeOk"] = null;
            TempData["Mensaje"] = null;

            return View();
        }
    }
}
