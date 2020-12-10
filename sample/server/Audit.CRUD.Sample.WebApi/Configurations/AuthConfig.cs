using Audit.CRUD.Sample.Infra.Extensions;
using Audit.CRUD.Sample.Infra.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Audit.CRUD.Sample.WebApi.Configurations
{
    public static class AuthConfig
    {
        public static void AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var authSettings = configuration.LoadSettings<AuthSettings>("AuthSettings", services);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateActor = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authSettings.Issuer,
                        ValidAudience = authSettings.ClientId,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.Secret))
                    };
                });
        }
    }
}