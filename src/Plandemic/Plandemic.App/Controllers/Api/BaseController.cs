using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Plandemic.App.Extensions;
using Plandemic.Common.Models;
using Plandemic.Common.Services;

namespace Plandemic.App.Controllers.Api
{
    [ApiController, Route("api/[controller]")]
    public class BaseController<T> : ControllerBase where T : Identifiable
    {
        protected IBaseService BaseService { get; }

        public BaseController(IBaseService baseService)
        {
            BaseService = baseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPageAsync(int page = 1, int? size = null)
        {
            var response = await BaseService.GetPageAsync<T>(page, size);

            return response.CreateResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var response = await BaseService.GetAsync<T>(id);

            return response.CreateResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] T source)
        {
            var response = await BaseService.PutAsync(id, source);

            return response.CreateResult();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] T source, Guid id)
        {
            var response = await BaseService.PostAsync(source);

            return response.CreateResult();
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await BaseService.DeleteAsync<T>(id);

            return response.CreateResult();
        }
    }
}