using Audit.CRUD.Sample.Application;
using Audit.CRUD.Sample.Application.Features.Users;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Audit.CRUD.Sample.WebApi.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(Startup), typeof(MappingProfile));
        }
    }
}