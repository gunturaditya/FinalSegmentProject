using Magang_API.Base;
using Magang_API.Model;

using Magang_API.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Magang_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : BaseController<Status, IStatusRepository, string>

    {
        public StatusController(IStatusRepository repository) : base(repository)
        {
        }

        [HttpGet("CountTrueStatus")]
        public async Task<ActionResult> getStatusTrueCount()
        {
            var result = await _repository.GetStatusTrueCountAsync();

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
        [HttpGet("CountFalseStatus")]
        public async Task<ActionResult> getStatusFalseCount()
        {
            var result = await _repository.GetStatusFalseCountAprovalAsync();

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
    }
}
