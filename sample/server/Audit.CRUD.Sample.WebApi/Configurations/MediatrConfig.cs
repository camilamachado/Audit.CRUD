using Audit.CRUD.Sample.Application;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.CRUD.Sample.WebApi.Configurations
{
	public static class MediatrConfig
	{
        public static void AddMediatrConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup), typeof(AppModule));
        }
    }
}
