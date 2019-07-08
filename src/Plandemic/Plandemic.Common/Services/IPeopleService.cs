using Plandemic.Common.Models;
using Plandemic.Common.Models.People;
using System.Threading.Tasks;

namespace Plandemic.Common.Services
{

    public interface IPeopleService : IBaseService
    {
        Task<ApiResponse<Skill>> GetSkillBySlugAsync(string slug);
        Task<ApiResponse<Individual>> GetIndividualByEmailAsync(string email);
    }
}
