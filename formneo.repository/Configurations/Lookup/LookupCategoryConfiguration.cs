using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vesa.core.Models.Lookup;

namespace vesa.repository.Configurations.Lookup
{
	public class LookupCategoryConfiguration : IEntityTypeConfiguration<LookupCategory>
	{
		public void Configure(EntityTypeBuilder<LookupCategory> builder)
		{
			builder.Property(p => p.Key).IsRequired().HasMaxLength(128);
			builder.Property(p => p.Description).HasMaxLength(512).IsRequired(false);
			builder.HasIndex(p => new { p.MainClientId, p.ModuleId, p.Key }).IsUnique();
			builder.HasOne(p => p.Module).WithMany().HasForeignKey(p => p.ModuleId);
		}
	}
}


