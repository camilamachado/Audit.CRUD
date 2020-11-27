using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Audit.CRUD.Sample.Domain.Users;

namespace Audit.CRUD.Sample.Infra.Data.Features.Users
{
	public class UserEntityConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");

			builder.HasKey(sc => sc.Id);

			builder.Property(y => y.Name).IsRequired().HasMaxLength(50);
			builder.Property(y => y.Email).IsRequired().HasMaxLength(100);
			builder.Property(y => y.Password).IsRequired();
		}
	}
}
