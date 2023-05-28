using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize(Roles ="Pembina")]
    public class PembinaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NilaiStudent()
        {
            return View();
        }

        public IActionResult Profil()
        {
            return View();
        }
    }
}
