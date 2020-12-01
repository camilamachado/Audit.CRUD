using Audit.CRUD.Settings;
using Elasticsearch.Net;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Nest.JsonNetSerializer;
using System;

namespace Audit.CRUD.Configurations
{
	public static class ElasticsearchConfig
	{
        public static void AddElasticsearch(this IServiceCollection services, ElasticsearchSettings elasticsearchSettings)
        {
            var uri = new Uri(elasticsearchSettings.Uri);

            var pool = new SingleNodeConnectionPool(uri);
            var connectionSettings = new ConnectionSettings(pool, JsonNetSerializer.Default).DefaultIndex(elasticsearchSettings.Index);

            var client = new ElasticClient(connectionSettings);

            services.Add(ServiceDescriptor.Singleton<IElasticClient>(client));
        }
    }
}
