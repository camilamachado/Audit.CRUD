using Audit.CRUD.Sample.Auth.Providers;
using Audit.CRUD.Sample.Infra.Extensions;
using Audit.CRUD.Sample.Infra.Settings;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Audit.CRUD.Sample.Auth.Configurations
{
	public static class JWTServerConfig
	{
        public static void UseJWTServer(this IApplicationBuilder app, IConfiguration configuration)
        {
            var settings = configuration.LoadSettings<AuthSettings>("AuthSettings");

            app.UseJwtServer(
                options =>
                {
                    options.TokenEndpointPath = settings.Endpoint;
                    options.AccessTokenExpireTimeSpan = new TimeSpan(settings.Expiration, 0, 0);
                    options.Issuer = settings.Issuer;
                    options.IssuerSigningKey = settings.Secret;
                    options.AuthorizationServerProvider = new CustomAuthorizationServerProvider(app.ApplicationServices.CreateScope().ServiceProvider.GetService<IMediator>());
                }
            );
        }
    }
}
