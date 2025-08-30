using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vesa.core.Models;

namespace vesa.repository.Configurations
{
	internal class UserTenantRoleConfiguration : IEntityTypeConfiguration<UserTenantRole>
	{
		public void Configure(EntityTypeBuilder<UserTenantRole> builder)
		{
			builder.HasIndex(x => new { x.UserId, x.RoleTenantId }).IsUnique();
			builder.HasIndex(x => x.UserId);
			builder.HasIndex(x => x.RoleTenantId);

			builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
			builder.HasOne(x => x.RoleTenant).WithMany().HasForeignKey(x => x.RoleTenantId);
		}
	}
}


