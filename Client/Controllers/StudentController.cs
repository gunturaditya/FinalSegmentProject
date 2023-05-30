using Client.Models;
using Client.Repository.Interface;
using Client.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class StudentController : Controller
    {
        IWebHostEnvironment _webHostEnvironment = null;
        private readonly IStudentRepository _studentRepository;

        public StudentController(IWebHostEnvironment webHostEnvironment, IStudentRepository studentRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _studentRepository = studentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
         
        [HttpGet]
        public IActionResult UploadCv(string fileName = "")
        {
            var nim = HttpContext.Session.GetString("NIM");
            var result = _studentRepository.Get(nim);
            var document = result.Result.Data.Document;
            FileVM fileVM = new FileVM();
            fileVM.FileName = fileName;

            string path = $"{_webHostEnvironment.WebRootPath}\\files\\";
            var documentPath = path + result.Result.Data.Document;
            int nId = 1;
            fileVM.Files.Add(new FileVM()
            {
                FileId = nId++,
                FileName = Path.GetFileName(document),
                FilePath = documentPath
            });
            return View(fileVM);
        }

        [HttpPost]
        public IActionResult UploadCv(IFormFile file, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            try
            {
                var nim = HttpContext.Session.GetString("NIM");
                var result = _studentRepository.Get(nim);
                var fileName = nim + file.FileName;
                string fileNamePath = $"{webHostEnvironment.WebRootPath}\\files\\{fileName}";
                using (FileStream fileStream = System.IO.File.Create(fileNamePath))
                {
                    file.CopyToAsync(fileStream);
                    fileStream.Flush();
                }
                var student = new Student
                {
                    Nim = result.Result.Data.Nim,
                    FirstName = result.Result.Data.FirstName,
                    LastName = result.Result.Data.LastName,
                    BirthDate = result.Result.Data.BirthDate,
                    Major = result.Result.Data.Major,
                    Degree = result.Result.Data.Degree,
                    Gpa = result.Result.Data.Gpa,
                    IsApproval = result.Result.Data.IsApproval,
                    Score = result.Result.Data.Score,
                    Email = result.Result.Data.Email,
                    PhoneNumber = result.Result.Data.PhoneNumber,
                    UniversitasId = result.Result.Data.UniversitasId,
                    Document = fileName
                };
                _studentRepository.Put(nim, student);
            }
            catch
            {
                return UploadCv();
            }            
            return UploadCv();
        }

        public IActionResult PDFViewerNewTab(string fileName)
        {
            string path = _webHostEnvironment.WebRootPath + "\\files\\" + fileName;
            return File(System.IO.File.ReadAllBytes(path),"application/pdf");
        }
    }
}
