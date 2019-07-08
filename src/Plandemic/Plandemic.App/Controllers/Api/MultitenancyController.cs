using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plandemic.Common.Models.Multitenancy;
using Plandemic.Common.Services;

namespace Plandemic.App.Controllers.Api
{
    public class MultitenancyController : BaseController<Tenant>
    {
        public MultitenancyController(IMultitenancyService multitenancyService) : base(multitenancyService)
        {

        }

        [HttpGet, Authorize("Admin")]
        public override Task<IActionResult> GetPageAsync(int page = 1, int? size = null)
        {
            return base.GetPageAsync(page, size);
        }
    }
}