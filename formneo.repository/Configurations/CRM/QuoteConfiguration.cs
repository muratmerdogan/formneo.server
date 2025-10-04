using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using formneo.core.Models.CRM;

namespace formneo.repository.Configurations.CRM
{
	public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
	{
		public void Configure(EntityTypeBuilder<Quote> builder)
		{
			builder.Property(p => p.QuoteNo).IsRequired().HasMaxLength(64);
			builder.Property(p => p.Currency).HasMaxLength(8);
			builder.HasIndex(p => p.QuoteNo).IsUnique();
			builder.HasOne(p => p.Customer).WithMany().HasForeignKey(p => p.CustomerId);
			builder.HasMany(p => p.Lines).WithOne(x => x.Quote).HasForeignKey(x => x.QuoteId);
		}
	}

	public class QuoteLineConfiguration : IEntityTypeConfiguration<QuoteLine>
	{
		public void Configure(EntityTypeBuilder<QuoteLine> builder)
		{
			builder.Property(p => p.ItemCode).HasMaxLength(64);
			builder.Property(p => p.ItemName).HasMaxLength(256);
			builder.Property(p => p.Unit).HasMaxLength(16);
		}
	}
}


