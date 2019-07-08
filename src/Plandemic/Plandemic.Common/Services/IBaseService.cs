using Plandemic.Common.Models;
using System;
using System.Threading.Tasks;

namespace Plandemic.Common.Services
{
    public interface IBaseService
    {
        Task<ApiResponse<T>> GetAsync<T>(Guid id) where T : Identifiable;
        Task<ApiResponse<Paginated<T>>> GetPageAsync<T>(int page = 1, int? size = null) where T : Identifiable;
        Task<ApiResponse> PutAsync<T>(Guid id, T source) where T : Identifiable;
        Task<ApiResponse<Guid>> PostAsync<T>(T source) where T : Identifiable;
        Task<ApiResponse> DeleteAsync<T>(Guid id, bool strict = false) where T : Identifiable;
    }
}
