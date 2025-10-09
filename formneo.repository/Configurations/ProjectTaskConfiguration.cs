using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using formneo.core.Models;

namespace formneo.repository.Configurations
{
	public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
	{
		public void Configure(EntityTypeBuilder<ProjectTask> builder)
		{
			builder.ToTable("ProjectTask");
			// Ensure FK points to CRM Customers table and is nullable (SET NULL on delete)
			builder
				.HasOne(pt => pt.Customer)
				.WithMany()
				.HasForeignKey(pt => pt.CustomerId)
				.OnDelete(DeleteBehavior.SetNull)
				.HasConstraintName("FK_ProjectTask_Customers_CustomerId");

			builder
				.HasOne(pt => pt.TenantProject)
				.WithMany()
				.HasForeignKey(pt => pt.ProjectId)
				.OnDelete(DeleteBehavior.Cascade)
				.HasConstraintName("FK_ProjectTask_TenantProjects_ProjectId");
		}
	}
}


