using Audit.CRUD.Sample.Infra.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Audit.CRUD.Sample.WebApi.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjector.RegisterServices(services);
        }
    }
}
