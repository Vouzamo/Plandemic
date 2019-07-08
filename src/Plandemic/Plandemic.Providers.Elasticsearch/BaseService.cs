using Nest;
using Plandemic.Common.Models;
using Plandemic.Common.Models.Multitenancy;
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

        protected virtual string MutateId<T>(Guid id) where T : Identifiable
        {
            return id.ToString();
        }

        protected virtual T MutateSource<T>(T source) where T : Identifiable
        {
            return source;
        }

        protected virtual SearchDescriptor<T> GetPageQuery<T>(SearchDescriptor<T> search) where T : Identifiable
        {
            return search.MatchAll();
        }

        public async Task<ApiResponse<Paginated<T>>> GetPageAsync<T>(int page = 1, int? size = null) where T : Identifiable
        {
            if (!size.HasValue)
            {
                size = 20;
            }

            var from = (page - 1) * size.Value;

            var response = await ElasticClient.SearchAsync<T>(search => 
                GetPageQuery(search)
                .From(from)
                .Size(size)
            );

            if (response.IsValid)
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
            var response = await ElasticClient.GetAsync<T>(MutateId<T>(id));

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
            source = MutateSource(source);

            var response = await ElasticClient.UpdateAsync<T, T>(MutateId<T>(id), update => update.Doc(source));

            if (response.IsValid)
            {
                return new ApiResponse(HttpStatusCode.OK);
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
            source = MutateSource(source);

            var response = await ElasticClient.IndexDocumentAsync(source);

            if (response.IsValid)
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
            var response = await ElasticClient.DeleteAsync<T>(MutateId<T>(id));

            if (response.IsValid || !strict)
            {
                return new ApiResponse(HttpStatusCode.OK);
            }
            else
            {
                var errorResponse = new ApiResponse(HttpStatusCode.InternalServerError);

                errorResponse.Errors.Add("elasticsearch", response.DebugInformation);

                return errorResponse;
            }
        }
    }

    public class TenantAwareService : BaseService
    {
        protected ITenantAccessor TenantAccessor { get; }

        public TenantAwareService(ITenantAccessor tenantAccessor, IElasticClient elasticClient) : base(elasticClient)
        {
            TenantAccessor = tenantAccessor;
        }

        protected override string MutateId<T>(Guid id)
        {
            var tenantId = TenantAccessor.GetTenantId();

            var model = default(T);
            
            switch(model)
            {
                case IdentifiableMultitenant temp:
                    temp.Id = id;
                    temp.TenantId = tenantId;

                    return temp.CompositeId;
            }

            return base.MutateId<T>(id);
        }

        protected override T MutateSource<T>(T source)
        {
            var tenantId = TenantAccessor.GetTenantId();

            switch(source)
            {
                case IdentifiableMultitenant newSource:
                    newSource.TenantId = tenantId;
                    break;
            }

            return source;
        }

        protected override SearchDescriptor<T> GetPageQuery<T>(SearchDescriptor<T> search)
        {
            var tenantId = TenantAccessor.GetTenantId();

            return search.Query(query => query
                .Bool(b => b
                    .Filter(filter => filter
                        .Term(term => term
                            .Field(new Field("tenantId.keyword"))
                            .Value(tenantId)
                        )
                    )
                )
            );
        }
    }
}
