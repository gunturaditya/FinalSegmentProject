using Client.Repository.Interface;
using Client.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client.Controllers
{
    /*[Authorize(Roles = "HRD")]*/
    public class HRDController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAccountRepository _accountRepository;
        private IWebHostEnvironment _webHostEnvironment = null;

        public HRDController(IStudentRepository studentRepository, IAccountRepository accountRepository, IWebHostEnvironment webHostEnvironment)
        {
            this._studentRepository = studentRepository;
            this._accountRepository = accountRepository;
            this._webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> StudentNeedAproval()
        {
            var result = await _studentRepository.GetStudentNullAproval();

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
                    PhoneNumber = e.PhoneNumber,
                    Document = e.Document
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

        public IActionResult CreateEmployee()
        {
            var gender = new List<SelectListItem>(){
            new SelectListItem{
                Text = "Male",
                Value = "0",
            },
            new SelectListItem{
                Text = "Female",
                Value = "1",
            }};

            ViewBag.Gender = gender;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(RegisterVM registerVM)
        {
            try
            {
                var result = await _accountRepository.RegisterEmployee(registerVM);
                if (result.StatusCode == "400")
                {
                    ViewBag.Message = "Account Fail to Registed";
                    return View();
                }

                if (result.StatusCode == "500")
                {
                    ViewBag.Message = "Server Error";
                    return View();
                }

                return RedirectToAction("Index", "HRD");
            }
            catch
            {
                var message = "Email or Password is incorrect ";
                ViewBag.messagge = message;
            }

            var gender = new List<SelectListItem>(){
            new SelectListItem{
                Text = "Male",
                Value = "0",
            },
            new SelectListItem{
                Text = "Female",
                Value = "1",
            }};

            ViewBag.Gender = gender;
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult PDFViewerNewTab(string fileName)
        {
            try
            {
                if(fileName == "undefined")
                {
                    return Index();
                }
                string path = _webHostEnvironment.WebRootPath + "\\files\\" + fileName;
                return File(System.IO.File.ReadAllBytes(path), "application/pdf");
            }
            catch
            {
                return Index();
            }
            
        }
    }
}
