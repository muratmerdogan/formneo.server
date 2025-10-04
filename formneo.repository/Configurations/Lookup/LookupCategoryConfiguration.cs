using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using formneo.core.Models.Lookup;

namespace formneo.repository.Configurations.Lookup
{
	public class LookupCategoryConfiguration : IEntityTypeConfiguration<LookupCategory>
	{
		public void Configure(EntityTypeBuilder<LookupCategory> builder)
		{
			builder.Property(p => p.Key).IsRequired().HasMaxLength(128);
			builder.Property(p => p.Description).HasMaxLength(512).IsRequired(false);
			builder.HasIndex(p => new { p.TenantId, p.ModuleId, p.Key }).IsUnique();
			builder.HasOne(p => p.Module).WithMany().HasForeignKey(p => p.ModuleId);
		}
	}
}


