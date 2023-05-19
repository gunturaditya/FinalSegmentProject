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
    public class StudentController : BaseController<Student, IStudentRepository, string>

    {
        public StudentController(IStudentRepository repository) : base(repository)
        {

        }
        [HttpGet("CountNullAproval")]
        public async Task<ActionResult> getStudentCount()
        {
            var result = await _repository.GetStudentCountAsync();

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
        [HttpGet("StudentNullAproval")]
        public async Task<ActionResult> getStudentListAproval()
        {
            var result = await _repository.GetAllStudentsNoAproval();

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

        [HttpPut("AprovalTrue/{nim}")]
        public async Task<ActionResult> StudentAprovalTrue(StudentAproval student,string nim)
        {
            var result = await _repository.IsExist(nim);
            if (!result)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Data Not Found!"
                });
            }

            var update = await _repository.AprovalTrue(student);
            if(update is 0)
            {
                return Conflict(new
                {
                    statusCode = 409,
                    message = "Data Fail to Update!"
                });
            }
            return Ok(new
            {
                statusCode = 200,
                message = "Data Updated!"
            });
        }

        [HttpPut("AprovalFalse/{nim}")]
        public async Task<ActionResult> StudentAprovalFalse(StudentAproval student, string nim)
        {
            var result = await _repository.IsExist(nim);
            if (!result)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Data Not Found!"
                });
            }

            var update = await _repository.AprovalFalse(student);
            if (update is 0)
            {
                return Conflict(new
                {
                    statusCode = 409,
                    message = "Data Fail to Update!"
                });
            }
            return Ok(new
            {
                statusCode = 200,
                message = "Data Updated!"
            });
        }
    }
}
