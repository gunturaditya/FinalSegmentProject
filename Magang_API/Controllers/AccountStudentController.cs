using Magang_API.Base;
using Magang_API.Model;
using Magang_API.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Magang_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountStudentController : BaseController<AccountStudent, IAccountStudentRepository, string>

    {
        public AccountStudentController(IAccountStudentRepository repository) : base(repository)
        {
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
    }
}
