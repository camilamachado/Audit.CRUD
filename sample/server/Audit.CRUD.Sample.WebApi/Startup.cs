using Audit.CRUD.Sample.Application;
using Audit.CRUD.Sample.WebApi.Configurations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Audit.CRUD.Sample.WebApi
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", true, true);

			Configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			// WebAPI Config
			services.AddControllers();

			// Setting DBContexts
			services.AddDatabaseConfiguration(Configuration);

			// AutoMapper Settings
			services.AddAutoMapperConfiguration();

			// Swagger Config
			services.AddSwaggerConfiguration();

			// .NET Native DI Abstraction
			services.AddDependencyInjectionConfiguration();

			services.AddMediatR(typeof(Startup), typeof(AppModule));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSwaggerSetup();
		}
	}
}
