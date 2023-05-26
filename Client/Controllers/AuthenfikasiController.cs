using Client.Repository.Interface;
using Client.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Client.Controllers
{
    public class AuthenfikasiController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountStudentRepository _accountStudentRepository;

        public AuthenfikasiController(IAccountRepository accountRepository,
                              IAccountStudentRepository accountStudentRepository)
        {
            _accountRepository = accountRepository;
            _accountStudentRepository = accountStudentRepository;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
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
                var role = jwt.Claims.First(d => d.Type == ClaimTypes.Role).Value;

                HttpContext.Session.SetString("JWToken", token);
                HttpContext.Session.SetString("Email", email);
                HttpContext.Session.SetString("FullName", fullname);
                HttpContext.Session.SetString("NIM", nim);
                HttpContext.Session.SetString("Role", role);
                return RedirectToAction("Index", "Student");
            }
            catch
            {
                var message = "Email or Password is incorrect ";
                ViewBag.Message = message;
            }
            return View();
        }

        [AllowAnonymous, HttpPost]
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
            return View("Login");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [AllowAnonymous, HttpPost]
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

                return RedirectToAction("Login", "Auth");
            }
            catch
            {
                var message = "Email or Password is incorrect ";
                ViewBag.messagge = message;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmployee(RegisterVM registerVM)
        {
            try
            {
                var result = await _accountRepository.RegisterEmployee(registerVM);
                if (result.StatusCode == "400")
                {
                    ViewBag.Message = "Account Hasn't Registed";
                    return View("Register");
                }

                if (result.StatusCode == "500")
                {
                    ViewBag.Message = "Server Error";
                    return View("Register");
                }

                return RedirectToAction("Login", "Authenfikasi");
            }
            catch
            {
                var message = "Email or Password is incorrect ";
                ViewBag.messagge = message;
            }
            return View("Register");
        }
    }
}

