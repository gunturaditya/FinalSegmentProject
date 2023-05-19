using Magang_API.Base;
using Magang_API.Handler;
using Magang_API.Model;
using Magang_API.Repository.Contracts;
using Magang_API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Net;
using System.Security.Claims;

namespace Magang_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, IAccountRepository, string>
    {
        private readonly ITokenService _tokenService;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountStudentRepository _accountStudentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IAccountStudentRoleRepository _studentRoleRepository;
        public AccountController(IAccountRepository repository,
                                    ITokenService tokenService,
                                    IAccountRoleRepository accountRoleRepository,
                                    IEmployeeRepository employeeRepository,
                                    IAccountStudentRepository accountStudentRepository,
                                    IStudentRepository studentRepository,
                                    IAccountStudentRoleRepository accountStudentRoleRepository)
                                    : base(repository)
        {
            _tokenService = tokenService;
            _accountRoleRepository = accountRoleRepository;
            _employeeRepository = employeeRepository;
            _accountStudentRepository = accountStudentRepository;
            _accountStudentRepository = accountStudentRepository;
            _studentRepository = studentRepository;
            _studentRoleRepository = accountStudentRoleRepository;
        }

        [AllowAnonymous]
        [HttpPost("Auth")]
        public async Task<ActionResult<Account>> LoginAsync(LoginVM loginVM)
        {
            try
            {
                var resultEmployee = await _repository.LoginAsync(loginVM);
                if (!resultEmployee)
                {
                    var resultStudent = await _accountStudentRepository.LoginAsync(loginVM);

                    if (!resultStudent)
                    {
                        return NotFound(new
                        {
                            statusCode = 404,
                            message = "Data Not Found!"
                        });
                    }
                    var userdataStudent = await _studentRepository.GetUserDataByEmailAsync(loginVM.Email);
                    var claim = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email, userdataStudent.Email),
                        new Claim(ClaimTypes.Name, userdataStudent.Email),
                        new Claim(ClaimTypes.NameIdentifier, userdataStudent.FullName),
                        new Claim("NIM", userdataStudent.Nik)
                    };
                    var getRolesStudent = await _studentRoleRepository.GetRolesByNimAsync(userdataStudent.Nik);
                    foreach (var item in getRolesStudent)
                    {
                        claim.Add(new Claim(ClaimTypes.Role, item));
                    }
                    var accessTokenstudent = _tokenService.GenerateAccessToken(claim);
                    return Ok(new
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Data = accessTokenstudent
                    });
                }

                var userdata = await _employeeRepository.GetUserDataByEmailAsync(loginVM.Email);
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userdata.Email),
                new Claim(ClaimTypes.Name, userdata.Email),
                new Claim(ClaimTypes.NameIdentifier, userdata.FullName),
                new Claim("NIK", userdata.Nik)
            };

                var getRoles = await _accountRoleRepository.GetRolesByNikAsync(userdata.Nik);

                foreach (var item in getRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var accessToken = _tokenService.GenerateAccessToken(claims);
                //var refreshToken = _tokenService.GenerateRefreshToken();

                //await _repository.UpdateToken(userdata.Email, refreshToken, DateTime.Now.AddDays(1)); // Token will expired in a day

                return Ok(new
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Data = accessToken
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new
                                  {
                                      Code = StatusCodes.Status500InternalServerError,
                                      Status = "Internal Server Error",
                                      Errors = new
                                      {
                                          Message = "Invalid Salt Version"
                                      },
                                  });
            }
        }

        [AllowAnonymous]
        [HttpPost("RegisterEmployee")]
        public async Task<IActionResult> RegisterEmployeeAsync(RegisterVM registerVM)
        {
            try
            {
                await _repository.RegisterAsync(registerVM);
                return Ok(new
                {
                    code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    data = new
                    {
                        Message = "Data Has Successfully Saved",
                    }
                });
            }
            catch
            {
                return NotFound(new
                {
                    code = StatusCodes.Status400BadRequest,
                    status = HttpStatusCode.BadRequest.ToString(),
                    data = new
                    {
                        message = "Server Cannot Process Request"
                    }
                });
            }
        }
        [AllowAnonymous]
        [HttpPost("RegisterStudent")]
        public async Task<IActionResult> RegisterStudentAsync(RegisterStudentVM registerstudentVM)
        {
            try
            {
                await _accountStudentRepository.RegisterStudentAsync(registerstudentVM);
                return Ok(new
                {
                    code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    data = new
                    {
                        Message = "Data Has Successfully Saved",
                    }
                });
            }
            catch
            {
                return NotFound(new
                {
                    code = StatusCodes.Status400BadRequest,
                    status = HttpStatusCode.BadRequest.ToString(),
                    data = new
                    {
                        message = "Server Cannot Process Request"
                    }
                });
            }
        }
    }
}
