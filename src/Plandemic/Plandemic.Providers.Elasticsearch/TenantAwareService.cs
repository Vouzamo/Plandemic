using Nest;
using Plandemic.Common.Models;
using Plandemic.Common.Utilities;
using System;

namespace Plandemic.Providers.Elasticsearch
{
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
