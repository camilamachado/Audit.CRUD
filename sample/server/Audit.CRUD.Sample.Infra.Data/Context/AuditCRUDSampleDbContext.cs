using Audit.CRUD.Sample.Domain.Users;
using Audit.CRUD.Sample.Infra.Data.Features.Users;
using Microsoft.EntityFrameworkCore;

namespace Audit.CRUD.Sample.Infra.Data.Context
{
	public class AuditCRUDSampleDbContext : DbContext
	{
		public AuditCRUDSampleDbContext(DbContextOptions<AuditCRUDSampleDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}