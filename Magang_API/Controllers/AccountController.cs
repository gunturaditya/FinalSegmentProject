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

        public AccountController(IAccountRepository repository,
                                    ITokenService tokenService,
                                    IAccountRoleRepository accountRoleRepository,
                                    IEmployeeRepository employeeRepository)
                                    : base(repository)
        {
            _tokenService = tokenService;
            _accountRoleRepository = accountRoleRepository;
            _employeeRepository = employeeRepository;
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

                var userdata = await _employeeRepository.GetUserDataByEmailAsync(loginVM.Email!);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, userdata.Email!),
                    new Claim(ClaimTypes.NameIdentifier, userdata.FullName!),
                    new Claim("NIK", userdata.Nik)
                };

                var getRoles = await _accountRoleRepository.GetRolesByNikAsync(userdata.Nik);

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
        public async Task<IActionResult> RegisterEmployeeAsync(RegisterVM registerVM)
        {
            try
            {
                await _repository.RegisterAsync(registerVM);
                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    StatusMessage = HttpStatusCode.OK.ToString(),
                    Data = new
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
