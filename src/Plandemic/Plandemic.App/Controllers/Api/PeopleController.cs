using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plandemic.App.Extensions;
using Plandemic.Common.Models.People;
using Plandemic.Common.Services;

namespace Plandemic.App.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        protected IPeopleService PeopleService { get; }

        public PeopleController(IPeopleService peopleService)
        {
            PeopleService = peopleService;
        }

        [HttpGet("individual/{id}")]
        public async Task<IActionResult> GetIndividualByIdAsync(Guid id)
        {
            var response = await PeopleService.GetByIdAsync<Individual>(id);

            return response.CreateResult();
        }
    }
}