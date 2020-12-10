using Audit.CRUD.Sample.Domain.Features.Users;
using Audit.CRUD.Sample.Infra.Data.Features.Users;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Audit.CRUD.Sample.Auth.Configurations
{
	public static class DependencyInjectionConfig 
	{
		public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddScoped<IUserRepository, UserRepository>();
		}
	}
}
