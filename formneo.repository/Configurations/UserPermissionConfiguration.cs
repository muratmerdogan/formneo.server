using formneo.core.Models.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace formneo.repository.Configurations
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("UserPermissions");
            builder.Property(x => x.ResourceKey).IsRequired().HasMaxLength(128);
            builder.Property(x => x.UserId).IsRequired();
            builder.HasIndex(x => new { x.MainClientId, x.UserId, x.ResourceKey }).IsUnique();
        }
    }
}


