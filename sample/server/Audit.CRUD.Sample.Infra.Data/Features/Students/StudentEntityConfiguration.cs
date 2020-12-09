using Audit.CRUD.Sample.Domain.Features.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.CRUD.Sample.Infra.Data.Features.Students
{
	public class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
	{
		public void Configure(EntityTypeBuilder<Student> builder)
		{
			builder.ToTable("Students");

			builder.HasKey(sc => sc.Id);

			builder.Property(y => y.LastName).IsRequired().HasMaxLength(50);
			builder.Property(y => y.FirstName).IsRequired().HasMaxLength(100);
			builder.Property(y => y.Age).IsRequired();
		}
	}
}
