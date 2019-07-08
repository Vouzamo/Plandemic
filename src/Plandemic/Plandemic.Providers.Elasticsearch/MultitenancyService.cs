using Nest;
using Plandemic.Common.Services;

namespace Plandemic.Providers.Elasticsearch
{
    public class MultitenancyService : BaseService, IMultitenancyService
    {
        public MultitenancyService(IElasticClient elasticClient) : base(elasticClient)
        {

        }
    }
}
