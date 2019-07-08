using Plandemic.Common.Models.People;
using Plandemic.Common.Services;

namespace Plandemic.App.Controllers.Api
{
    public class IndividualController : BaseController<Individual>
    {
        public IndividualController(IPeopleService peopleService) : base(peopleService)
        {
            
        }
    }
}