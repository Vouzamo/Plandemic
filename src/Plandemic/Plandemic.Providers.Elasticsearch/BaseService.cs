using Nest;
using Plandemic.Common.Models;
using Plandemic.Common.Services;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Plandemic.Providers.Elasticsearch
{
    public class BaseService : IBaseService
    {
        protected IElasticClient ElasticClient { get; }

        public BaseService(IElasticClient elasticClient)
        {
            ElasticClient = elasticClient;
        }

        public async Task<ApiResponse<Paginated<T>>> GetPageAsync<T>(int page = 1, int? size = null) where T : Identifiable
        {
            if(!size.HasValue)
            {
                size = 20;
            }

            var from = (page - 1) * size.Value;

            var response = await ElasticClient.SearchAsync<T>(search => search
                .MatchAll()
                .From(from)
                .Size(size)
            );

            if(response.IsValid)
            {
                var paginated = new Paginated<T>(page, size.Value, response.Documents.ToList(), response.Total);

                return new ApiResponse<Paginated<T>>(HttpStatusCode.OK, paginated);
            }
            else
            {
                var errorResponse = new ApiResponse<Paginated<T>>(HttpStatusCode.InternalServerError);

                errorResponse.Errors.Add("elasticsearch", response.DebugInformation);

                return errorResponse;
            }
        }

        public async Task<ApiResponse<T>> GetAsync<T>(Guid id) where T : Identifiable
        {
            var response = await ElasticClient.GetAsync<T>(id);

            if (response.IsValid)
            {
                return new ApiResponse<T>(HttpStatusCode.OK, response.Source);
            }
            else
            {
                var errorResponse = new ApiResponse<T>(HttpStatusCode.InternalServerError);

                errorResponse.Errors.Add("elasticsearch", response.DebugInformation);

                return errorResponse;
            }
        }

        public async Task<ApiResponse> PutAsync<T>(Guid id, T source) where T : Identifiable
        {
            var response = await ElasticClient.UpdateAsync<T, T>(id, update => update.Doc(source));

            if (response.IsValid)
            {
                return new ApiResponse(HttpStatusCode.Accepted);
            }
            else
            {
                var errorResponse = new ApiResponse(HttpStatusCode.InternalServerError);

                errorResponse.Errors.Add("elasticsearch", response.DebugInformation);

                return errorResponse;
            }
        }

        public async Task<ApiResponse<Guid>> PostAsync<T>(T source) where T : Identifiable
        {
            var response = await ElasticClient.IndexDocumentAsync(source);

            if(response.IsValid)
            {
                return new ApiResponse<Guid>(HttpStatusCode.Created, source.Id);
            }
            else
            {
                var errorResponse = new ApiResponse<Guid>(HttpStatusCode.InternalServerError);

                errorResponse.Errors.Add("elasticsearch", response.DebugInformation);

                return errorResponse;
            }
        }

        public async Task<ApiResponse> DeleteAsync<T>(Guid id, bool strict = false) where T : Identifiable
        {
            var response = await ElasticClient.DeleteAsync<T>(id);

            if(response.IsValid || !strict)
            {
                return new ApiResponse(HttpStatusCode.Accepted);
            }
            else
            {
                var errorResponse = new ApiResponse(HttpStatusCode.InternalServerError);

                errorResponse.Errors.Add("elasticsearch", response.DebugInformation);

                return errorResponse;
            }
        }
    }
}
