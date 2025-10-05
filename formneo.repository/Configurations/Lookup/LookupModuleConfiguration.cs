using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using formneo.core.Models.Lookup;

namespace formneo.repository.Configurations.Lookup
{
	public class LookupModuleConfiguration : IEntityTypeConfiguration<TenantLookupModule>
	{
		public void Configure(EntityTypeBuilder<TenantLookupModule> builder)
		{
			builder.Property(p => p.Key).IsRequired().HasMaxLength(128);
			builder.Property(p => p.Name).HasMaxLength(256).IsRequired(false);

		}
	}
}



