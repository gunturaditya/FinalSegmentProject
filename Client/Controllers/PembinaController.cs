using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
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
    }
}
