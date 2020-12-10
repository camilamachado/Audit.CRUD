using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Audit.CRUD.Sample.Auth.Configurations
{
    public static class MediatrConfig
    {
        public static void AddMediatrConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddMediatR(typeof(Startup));
        }
    }
}
