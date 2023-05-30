using Client.Models;
using Client.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class UniversityController : Controller
    {
        private readonly IUniversityRepository repository;

        public UniversityController(IUniversityRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var universities = new List<University>();

            if (result.Data != null)
            {
                universities = result.Data?.Select(e => new University
                {
                    Id = e.Id,
                    Name = e.Name,
                }).ToList();
            }

            return View(universities);
        }
    }
}
