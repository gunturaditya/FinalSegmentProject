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
    }
}
