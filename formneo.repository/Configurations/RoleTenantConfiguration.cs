using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vesa.core.Models;

namespace vesa.repository.Configurations
{
	internal class RoleTenantConfiguration : IEntityTypeConfiguration<RoleTenant>
	{
		public void Configure(EntityTypeBuilder<RoleTenant> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.IsActive).HasDefaultValue(true);
			builder.Property(x => x.IsLocked).HasDefaultValue(false);
			builder.HasIndex(x => new { x.RoleId, x.TenantId }).IsUnique();

			builder
				.HasOne(x => x.Role)
				.WithMany()
				.HasForeignKey(x => x.RoleId)
				.OnDelete(DeleteBehavior.NoAction);

			builder
				.HasOne(x => x.Tenant)
				.WithMany()
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.ToTable("RoleTenants");
		}
	}
}



