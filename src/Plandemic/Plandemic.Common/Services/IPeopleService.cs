using Plandemic.Common.Models;
using Plandemic.Common.Models.People;
using System;
using System.Threading.Tasks;

namespace Plandemic.Common.Services
{
    public interface IBaseService
    {
        Task<ApiResponse<T>> GetByIdAsync<T>(Guid id) where T : Identifiable;
        Task<ApiResponse> PutAsync<T, TId>(T source) where T : Identifiable;
        Task<ApiResponse<Guid>> PostAsync<T>(T source) where T : Identifiable;
        Task<ApiResponse> Delete<T>(Guid id, bool strict = false) where T : Identifiable;
    }

    public interface IPeopleService : IBaseService
    {
        Task<ApiResponse<Skill>> GetSkillBySlug(string slug);
        Task<ApiResponse<Individual>> GetIndividualByEmail(string email);
    }
}
