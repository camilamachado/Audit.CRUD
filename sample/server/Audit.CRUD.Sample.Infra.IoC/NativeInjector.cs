using Audit.CRUD.Sample.Domain.Features.Students;
using Audit.CRUD.Sample.Domain.Features.Users;
using Audit.CRUD.Sample.Infra.Data.Context;
using Audit.CRUD.Sample.Infra.Data.Features.Students;
using Audit.CRUD.Sample.Infra.Data.Features.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.CRUD.Sample.Infra.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Infra - Data
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<AuditCRUDSampleDbContext>();
        }
    }
}
