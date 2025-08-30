using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vesa.core.Models.CRM;

namespace vesa.repository.Configurations.CRM
{
	public class OpportunityConfiguration : IEntityTypeConfiguration<Opportunity>
	{
		public void Configure(EntityTypeBuilder<Opportunity> builder)
		{
			builder.Property(p => p.Title).IsRequired().HasMaxLength(256);
			builder.Property(p => p.Currency).HasMaxLength(8);
			builder.HasOne(p => p.Customer).WithMany().HasForeignKey(p => p.CustomerId);
		}
	}
}


