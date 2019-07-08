using Nest;
using Plandemic.Common.Models;
using Plandemic.Common.Models.Multitenancy;
using Plandemic.Common.Models.People;
using Plandemic.Common.Services;
using System.Net;
using System.Threading.Tasks;

namespace Plandemic.Providers.Elasticsearch
{
    public class PeopleService : TenantAwareService, IPeopleService
    {
        public PeopleService(IElasticClient elasticClient, ITenantAccessor tenantAccessor) : base(tenantAccessor, elasticClient)
        {

        }

        public async Task<ApiResponse<Skill>> GetSkillBySlugAsync(string slug)
        {
            return new ApiResponse<Skill>(HttpStatusCode.NotImplemented);
        }

        public async Task<ApiResponse<Individual>> GetIndividualByEmailAsync(string email)
        {
            return new ApiResponse<Individual>(HttpStatusCode.NotImplemented);
        }
    }
}
