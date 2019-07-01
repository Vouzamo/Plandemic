using Plandemic.Common.Models;
using Plandemic.Common.Models.People;
using Plandemic.Common.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Plandemic.Providers.Elasticsearch
{
    public class PeopleService : IPeopleService
    {
        public async Task<ApiResponse<T>> GetByIdAsync<T>(Guid id) where T : Identifiable
        {
            var response = new ApiResponse<T>(HttpStatusCode.NotImplemented);

            response.Errors.Add("method", "Not Implemented");

            return response;
        }

        public async Task<ApiResponse> Delete<T>(Guid id, bool strict = false) where T : Identifiable
        {
            return new ApiResponse(HttpStatusCode.NotImplemented);
        }

        public async Task<ApiResponse> PutAsync<T, TId>(T source) where T : Identifiable
        {
            return new ApiResponse(HttpStatusCode.NotImplemented);
        }

        public async Task<ApiResponse<Guid>> PostAsync<T>(T source) where T : Identifiable
        {
            return new ApiResponse<Guid>(HttpStatusCode.Created, Guid.NewGuid());
        }

        public async Task<ApiResponse<Individual>> GetIndividualByEmail(string email)
        {
            return new ApiResponse<Individual>(HttpStatusCode.NotImplemented);
        }

        public async Task<ApiResponse<Skill>> GetSkillBySlug(string slug)
        {
            return new ApiResponse<Skill>(HttpStatusCode.NotImplemented);
        }
    }
}
