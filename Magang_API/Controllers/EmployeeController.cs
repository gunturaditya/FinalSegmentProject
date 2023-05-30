using Magang_API.Base;
using Magang_API.Models;

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
        private readonly IStudentRepository _studentRepository;
        public EmployeeController(IEmployeeRepository repository, IStudentRepository studentRepository) : base(repository)
        {
            _studentRepository = studentRepository;
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
		[HttpGet("getEmployee/{nim}")]
		public async Task<ActionResult<EmployeeProfileVM>> EmployeeGetByNim(string nim)
		{
			var result = await _repository.GetEmployeeByNim(nim);


			if (result is null)
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

		[HttpGet("employe/{DepartmentId}")]
        public async Task<ActionResult<EmployeeProfileVM>> EmployeeGetByDepartment(int DepartmentId)
        {
            var result = await _repository.GetEmployeeByIdDepartment(DepartmentId);


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

        [HttpPut("Penilaian/{nim}")]
        public async Task<ActionResult> StudentAprovalTrue(Penilaian penilaian, string nim)
        {
            var result = await _studentRepository.IsExist(nim);
            if (!result)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Data Not Found!"
                });
            }

            var update = await _repository.StudentScore(penilaian);
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
