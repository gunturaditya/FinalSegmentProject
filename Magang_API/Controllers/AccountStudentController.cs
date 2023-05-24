using Magang_API.Base;
using Magang_API.Handler;
using Magang_API.Model;
using Magang_API.Repository.Contracts;
using Magang_API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Magang_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountStudentController : BaseController<AccountStudent, IAccountStudentRepository, string>

    {
        private readonly ITokenService _tokenService;
        private readonly IAccountStudentRoleRepository _accountStudentRoleRepository;
        private readonly IStudentRepository _studentRepository;
        public AccountStudentController(IAccountStudentRepository repository, ITokenService tokenService,
                                    IAccountStudentRoleRepository accountStudentRoleRepository,
                                    IStudentRepository studentRepository) : base(repository)
        {
            _tokenService = tokenService;
            _accountStudentRoleRepository = accountStudentRoleRepository;
            _studentRepository = studentRepository;
        }
        [HttpDelete("student/{id}")]
        public async Task<ActionResult> DeleteAccount(string id)
        {
            var result = await _repository.DeleteAccount(id);

            if (result is 0)
            {
                return Conflict(new
                {
                    statusCode = 409,
                    message = "Data Fail to Delete!"
                });
            }

            return Ok(new
            {
                statusCode = 200,
                message = "Data Deleted!"
            });
        }
        [AllowAnonymous]
        [HttpPost("Auth")]
        public async Task<IActionResult> LoginAsync(LoginVM loginVM)
        {
            try
            {
                var result = await _repository.LoginAsync(loginVM);
                if (!result)
                {
                    return NotFound(new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = HttpStatusCode.NotFound.ToString(),
                        Data = "Data Not Found!",
                    });
                }

                var userdata = await _studentRepository.GetUserDataByEmailAsync(loginVM.Email!);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, userdata.Email!),
                    new Claim(ClaimTypes.NameIdentifier, userdata.FullName!),
                    new Claim("NIM", userdata.Nik)
                };

                var getRoles = await _accountStudentRoleRepository.GetRolesByNimAsync(userdata.Nik);

                foreach (var item in getRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var accessToken = _tokenService.GenerateAccessToken(claims);

                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = HttpStatusCode.OK.ToString(),
                    Data = accessToken,
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new
                                  {
                                      StatusCode = StatusCodes.Status500InternalServerError,
                                      Message = HttpStatusCode.InternalServerError.ToString(),
                                      Data = "Internal Server Error!",
                                  });
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterStudentVM registerStudentVM)
        {
            try
            {
                await _repository.RegisterStudentAsync(registerStudentVM);
                return Ok(new
                {
                    code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    data = new
                    {
                        Message = "Data Successfully Registed",
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
