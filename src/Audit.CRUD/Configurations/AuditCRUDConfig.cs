using Audit.CRUD.Application;
using Audit.CRUD.Domain;
using Audit.CRUD.Repository;
using Audit.CRUD.Repository.Context;
using Audit.CRUD.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.CRUD.Configurations
{
	public static class AuditCRUDConfig
	{
        /// <summary>
        /// Main extension method for configuring Audit.CRUD and its dependencies
        /// </summary>
		/// <param name="elasticsearchSettings">Connection settings with elasticsearch pool</param>
        public static void UseAuditCRUD(this IServiceCollection services, ElasticsearchSettings elasticsearchSettings)
        {
            services.AddElasticsearchConfiguration(elasticsearchSettings);

            services.AddScoped<AuditCRUDDbContext>();

            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
			services.AddScoped<IAuditCRUD, AuditCRUDService>();
		}
    }
}
