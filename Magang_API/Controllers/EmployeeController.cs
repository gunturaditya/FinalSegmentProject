using Magang_API.Base;
using Magang_API.Model;

using Magang_API.Repository.Contracts;
using Magang_API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Magang_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController<Employee, IEmployeeRepository, string>

    {
        public EmployeeController(IEmployeeRepository repository) : base(repository)
        {
        }
        [HttpGet("ProfileEmployee")]
        public async Task<ActionResult> ProfileEmployee()
        {
            var result = await _repository.GetDataEmployePembina();
            if (result == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Data Not Found!"
                });
            }
            return Ok(new
            {
                statusCode = 200,
                message = "Data Found!",
                data = result
            });
        }
        [HttpGet("Profil/{nik}")]
        public async Task<ActionResult<EmployeeProfileVM>> ProfileEmployeeGetByNik(string nik)
        {
            var result = await _repository.GetDataProfileBynik(nik);

            
            if (result.Count() is 0)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Data Not Found!"
                });
            }
            return Ok(new
            {
                statusCode = 200,
                message = "Data Found!",
                data = result
            });
        }
    }
}
