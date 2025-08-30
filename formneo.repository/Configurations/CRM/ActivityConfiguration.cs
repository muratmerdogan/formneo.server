using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vesa.core.Models.CRM;

namespace vesa.repository.Configurations.CRM
{
	public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
	{
		public void Configure(EntityTypeBuilder<Activity> builder)
		{
			builder.Property(p => p.Subject).HasMaxLength(256);
			builder.HasOne(p => p.Customer).WithMany().HasForeignKey(p => p.CustomerId);
			builder.HasOne(p => p.Opportunity).WithMany(x => x.Activities).HasForeignKey(p => p.OpportunityId);
		}
	}
}


