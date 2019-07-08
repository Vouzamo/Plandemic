using Elasticsearch.Net;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Nest.JsonNetSerializer;
using Plandemic.Common.Models.People;
using Plandemic.Common.Services;
using System;

namespace Plandemic.Providers.Elasticsearch
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddElasticsearchProvider(this IServiceCollection services)
        {
            var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

            var connectionSettings = new ConnectionSettings(connectionPool, JsonNetSerializer.Default);

            connectionSettings.DefaultMappingFor<Individual>(mapping => mapping
                .IndexName("plandemic.individuals")
                .IdProperty(doc => doc.CompositeId)
            );

            var elasticClient = new ElasticClient(connectionSettings);

            services.AddSingleton<IElasticClient>(elasticClient);

            services.AddSingleton<IPeopleService, PeopleService>();

            return services;
        }
    }
}
