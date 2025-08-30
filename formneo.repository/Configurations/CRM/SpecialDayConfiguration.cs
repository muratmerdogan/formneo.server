using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vesa.core.Models.CRM;

namespace vesa.repository.Configurations.CRM
{
	public class SpecialDayConfiguration : IEntityTypeConfiguration<SpecialDay>
	{
		public void Configure(EntityTypeBuilder<SpecialDay> builder)
		{
			builder.Property(p => p.Title).IsRequired().HasMaxLength(128);
			builder.Property(p => p.Channel).HasMaxLength(32);
			builder.Property(p => p.MessageTemplate).HasMaxLength(1024);
			builder.HasOne(p => p.Customer).WithMany().HasForeignKey(p => p.CustomerId);
		}
	}
}


