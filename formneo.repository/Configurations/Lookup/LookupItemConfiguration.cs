using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using formneo.core.Models.Lookup;

namespace formneo.repository.Configurations.Lookup
{
	public class LookupItemConfiguration : IEntityTypeConfiguration<LookupItem>
	{
		public void Configure(EntityTypeBuilder<LookupItem> builder)
		{
			builder.Property(p => p.Code).HasMaxLength(64).IsRequired();
			builder.Property(p => p.Name).HasMaxLength(256).IsRequired();
			builder.Property(p => p.NameLocalizedJson).IsRequired(false);
			builder.Property(p => p.ExternalKey).HasMaxLength(128).IsRequired(false);
			builder.HasIndex(p => new { p.TenantId, p.CategoryId, p.Code }).IsUnique();
			builder.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
		}
	}
}


