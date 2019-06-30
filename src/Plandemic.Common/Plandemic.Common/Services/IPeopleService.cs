using Plandemic.Common.Models;
using System.Threading.Tasks;

namespace Plandemic.Common.Services
{
    public interface IBaseService
    {
        Task<bool> TryGetByIdAsync<T, TId>(TId id, out T source) where T : IIdentifiable<TId>;
        Task<bool> TryPutAsync<T, TId>(T source) where T : IIdentifiable<TId>;
        Task<bool> TryPostAsync<T, TId>(T source, out TId id) where T : IIdentifiable<TId>;
        Task<bool> TryDelete<T, TId>(TId id, bool scrict = false) where T : IIdentifiable<TId>;
    }

    public interface IPeopleService : IBaseService
    {
        
    }
}
