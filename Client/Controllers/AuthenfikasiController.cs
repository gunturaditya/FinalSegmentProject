using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AuthenfikasiController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
