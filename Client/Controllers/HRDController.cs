using Client.Repository.Interface;
using Client.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Client.Controllers
{
    public class HRDController : Controller
    {
        private readonly IStudentRepository studentRepository;

        public HRDController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> StudentNeedAproval()
        {
            var result = await studentRepository.GetStudentNullAproval();

            var student = new List<StudentNullAprovalVM>();

            if (result.Data != null)
            {
                student = result.Data?.Select(e => new StudentNullAprovalVM
                {
                  Nim = e.Nim,
                  Email = e.Email,
                 FullName = e.FullName,
                 Name = e.Name,
                  Major = e.Major,
                  Degree = e.Degree,
                  Gpa = e.Gpa,
                  PhoneNumber = e.PhoneNumber
                }).ToList();
            }

            return View(student);
        }

        public IActionResult Aproval()
        {
            return View();
        }

        public IActionResult Employee()
        {
            return View();
        }
    }
}
