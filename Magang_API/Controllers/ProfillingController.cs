﻿using Magang_API.Base;
using Magang_API.Models;
using Magang_API.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Magang_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfillingController : BaseController<Profiling, IProfillingRepository, string>

    {
        public ProfillingController(IProfillingRepository repository) : base(repository)
        {
        }
    }
}
