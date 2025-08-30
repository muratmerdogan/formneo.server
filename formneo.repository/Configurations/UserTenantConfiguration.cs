using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vesa.core.Models;

namespace vesa.repository.Configurations
{
    internal class UserTenantConfiguration : IEntityTypeConfiguration<UserTenant>
    {
        public void Configure(EntityTypeBuilder<UserTenant> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsActive).HasDefaultValue(true);

            // Defaults for tenant-scoped flags
            builder.Property(x => x.HasTicketPermission).HasDefaultValue(false);
            builder.Property(x => x.HasDepartmentPermission).HasDefaultValue(false);
            builder.Property(x => x.HasOtherCompanyPermission).HasDefaultValue(false);
            builder.Property(x => x.HasOtherDeptCalendarPerm).HasDefaultValue(false);
            builder.Property(x => x.canEditTicket).HasDefaultValue(false);
            builder.Property(x => x.DontApplyDefaultFilters).HasDefaultValue(false);

            builder.HasIndex(x => new { x.UserId, x.TenantId }).IsUnique();

            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Tenant)
                .WithMany()
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("UserTenants");
        }
    }
}


