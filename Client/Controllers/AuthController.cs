using Client.Repository.Interface;
using Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Client.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountStudentRepository _accountStudentRepository;
        public AuthController(IAccountRepository accountRepository,
                              IAccountStudentRepository accountStudentRepository)
        {
            _accountRepository = accountRepository;
            _accountStudentRepository = accountStudentRepository;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            try
            {
                var result = await _accountStudentRepository.LoginStudent(loginVM);
                if (result.StatusCode == "400")
                {
                    ViewBag.Message = "Account Hasn't Registed";
                    return View();
                }

                if (result.StatusCode == "500")
                {
                    ViewBag.Message = "Server Error";
                    return View();
                }

                //encode token
                var token = result.Data;
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var email = jwt.Claims.First(d => d.Type == ClaimTypes.Email).Value;
                var fullname = jwt.Claims.First(d => d.Type == ClaimTypes.NameIdentifier).Value;
                var nim = jwt.Claims.First(d => d.Type == "NIM").Value;
                var status = jwt.Claims.First(d => d.Type == "Status").Value;
                var role = jwt.Claims.First(d => d.Type == ClaimTypes.Role).Value;

                HttpContext.Session.SetString("JWToken", token);
                HttpContext.Session.SetString("Email", email);
                HttpContext.Session.SetString("FullName", fullname);
                HttpContext.Session.SetString("NIM", nim);
                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("Status", status);
                return RedirectToAction("Index", "Student");
            }
            catch
            {
                var message = "Email or Password is incorrect ";
                ViewBag.Message = message;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginEmployee(LoginVM loginVM)
        {
            try
            {
                var result = await _accountRepository.LoginEmployee(loginVM);
                if (result.StatusCode == "400")
                {
                    ViewBag.Message = "Account Hasn't Registed";
                    return View();
                }

                if (result.StatusCode == "500")
                {
                    ViewBag.Message = "Server Error";
                    return View();
                }

                //encode token
                var token = result.Data;
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var email = jwt.Claims.First(d => d.Type == ClaimTypes.Email).Value;
                var fullname = jwt.Claims.First(d => d.Type == ClaimTypes.NameIdentifier).Value;
                var nik = jwt.Claims.First(d => d.Type == "NIK").Value;
                var role = jwt.Claims.First(d => d.Type == ClaimTypes.Role).Value;

                HttpContext.Session.SetString("JWToken", token);
                HttpContext.Session.SetString("Email", email);
                HttpContext.Session.SetString("FullName", fullname);
                HttpContext.Session.SetString("NIK", nik);
                HttpContext.Session.SetString("Role", role);
                if (role == "HRD")
                {
                    return RedirectToAction("Index", "HRD");
                }

                return RedirectToAction("Index", "Pembina");
            }
            catch
            {
                var message = "Email or Password is incorrect ";
                ViewBag.Message = message;
            }
            return View();
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterStudentVM registerStudentVM)
        {
            try
            {
                var result = await _accountStudentRepository.RegisterStudent(registerStudentVM);
                if (result.StatusCode == "400")
                {
                    ViewBag.Message = "Account Hasn't Registed";
                    return View();
                }

                if (result.StatusCode == "500")
                {
                    ViewBag.Message = "Server Error";
                    return View();
                }

                return RedirectToAction("Login", "Authenfikasi");
            }
            catch
            {
                var message = "Email or Password is incorrect ";
                ViewBag.messagge = message;
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            HttpContext.Response.Cookies.Delete("JWToken");
            HttpContext.Response.Cookies.Delete("Email");
            HttpContext.Response.Cookies.Delete("FullName");
            HttpContext.Response.Cookies.Delete("NIK");
            HttpContext.Response.Cookies.Delete("NIM");
            HttpContext.Response.Cookies.Delete("Role");
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}

