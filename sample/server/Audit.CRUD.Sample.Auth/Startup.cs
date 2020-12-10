using Audit.CRUD.Sample.Auth.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Audit.CRUD.Sample.Auth
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Setting DBContexts
			services.AddDatabaseConfiguration(Configuration);

			// AutoMapper Settings
			services.AddAutoMapperConfiguration();

			services.AddMediatrConfiguration();

			// .NET Native DI Abstraction
			services.AddDependencyInjectionConfiguration();

			// Setting JWT Server
			services
				.AddJwtServer()
				.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

			// Setting MVC
			services.AddMvc(options => options.EnableEndpointRouting = false);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseJWTServer(Configuration);
		}
	}
}
