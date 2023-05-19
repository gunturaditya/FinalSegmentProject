using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Magang_API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity, TIRepository, TKey> : ControllerBase
    where TEntity : class
    where TIRepository : IBaseRepository<TEntity, TKey>
    {
        protected readonly TIRepository _repository;

        public BaseController(TIRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var results = await _repository.GetAllAsync();
            if (results.Count() is 0)
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
                data = results
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(TKey id)
        {
            var result = await _repository.GetByIdAsync(id);
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

        [HttpPost]
       virtual public async Task<ActionResult> InsertAsync(TEntity entity)
        {
            var result = await _repository.InsertAsync(entity);
            if (result is 0)
            {
                return Conflict(new
                {
                    statusCode = HttpStatusCode.Conflict,
                    message = "Data Fail to Insert!"
                });
            }
            return Ok(new
            {
                statusCode = 200,
                message = "Data Saved Successfully!"
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(TEntity entity, TKey id)
        {
            var result = await _repository.IsExist(id);
            if (!result)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Data Not Found!"
                });
            }

            var update = await _repository.UpdateAsync(entity);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(TKey id)
        {
            var result = await _repository.DeleteAsync(id);

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
