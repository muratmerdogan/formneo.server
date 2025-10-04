using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using formneo.core.Models;

namespace formneo.repository.Configurations
{
    internal class ClientConfiguration : IEntityTypeConfiguration<MainClient>
    {
        public void Configure(EntityTypeBuilder<MainClient> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            //builder.Property(x => x.Slug).IsRequired().HasMaxLength(120);
            //builder.Property(x => x.Timezone).IsRequired().HasMaxLength(64);
            //builder.Property(x => x.Subdomain).IsRequired().HasMaxLength(120);
            builder.Property(x => x.CustomDomain).HasMaxLength(255);
            builder.Property(x => x.LogoUrl).HasMaxLength(1024);

            // Indexes & uniqueness
            //builder.HasIndex(x => x.Slug).IsUnique();
            //builder.HasIndex(x => x.Subdomain).IsUnique();
            //builder.HasIndex(x => x.CustomDomain).IsUnique();

            // Enum conversions to int
            builder.Property(x => x.Status).HasConversion<int>();
            builder.Property(x => x.Plan).HasConversion<int>();
            builder.Property(x => x.SsoType).HasConversion<int?>();

            // JSON strings
            builder.Property(x => x.FeatureFlags).IsRequired();
            builder.Property(x => x.Quotas).IsRequired();





            //    builder
            //.HasIndex(a => a.Guid)
            //.IsUnique();

            builder.ToTable("Clients");



        }
    }
}
