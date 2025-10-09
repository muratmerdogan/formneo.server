using formneo.core.Models.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace formneo.repository.Configurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("Resources");
            builder.HasIndex(x => x.ResourceKey).IsUnique();
            builder.Property(x => x.ResourceKey).IsRequired().HasMaxLength(128);
            builder.Property(x => x.DefaultMask).IsRequired().HasDefaultValue((int)Actions.Full);
        }
    }
}


