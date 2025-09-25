using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vesa.core.Models.CRM;

namespace vesa.repository.Configurations.CRM
{
	public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
			builder.Property(p => p.LegalName).HasMaxLength(256).IsRequired(false);
			builder.Property(p => p.CompanyType).HasMaxLength(128).IsRequired(false);
			builder.Property(p => p.Code).IsRequired().HasMaxLength(64);
			builder.Property(p => p.TaxOffice).HasMaxLength(128).IsRequired(false);
			builder.Property(p => p.TaxNumber).HasMaxLength(64).IsRequired(false);
			builder.Property(p => p.IsReferenceCustomer).IsRequired();
			builder.Property(p => p.LogoFilePath).HasMaxLength(512).IsRequired(false);
			builder.Property(p => p.Note).IsRequired(false);
			builder.Property(p => p.Website).HasMaxLength(512).IsRequired(false);
			builder.Property(p => p.TwitterUrl).HasMaxLength(512).IsRequired(false);
			builder.Property(p => p.FacebookUrl).HasMaxLength(512).IsRequired(false);
			builder.Property(p => p.LinkedinUrl).HasMaxLength(512).IsRequired(false);
			builder.Property(p => p.InstagramUrl).HasMaxLength(512).IsRequired(false);

			// Yeni alanlar
			builder.Property(p => p.OwnerId).HasMaxLength(256).IsRequired(false);
			builder.Property(p => p.LifecycleStage).HasConversion<int>();
			builder.Property(p => p.NextActivityDate).IsRequired(false);

			// Enum konversiyonları
			//builder.Property(p => p.CustomerType).HasConversion<int>(); // Kaldırıldı: Artık Lookup FK kullanılıyor ve alan [NotMapped]
			//builder.Property(p => p.Category).HasConversion<int>(); // Kaldırıldı: Artık Lookup FK kullanılıyor ve alan [NotMapped]
			//builder.Property(p => p.Status).HasConversion<int>(); // Status string olarak tutuluyor

			//// Lookup ilişkileri (nullable FK)
			//builder
			//	.HasOne(p => p.CustomerTypeItem)
			//	.WithMany()
			//	.HasForeignKey(p => p.CustomerTypeId)
			//	.OnDelete(DeleteBehavior.SetNull);

			//builder
			//	.HasOne(p => p.CategoryItem)
			//	.WithMany()
			//	.HasForeignKey(p => p.CategoryId)
			//	.OnDelete(DeleteBehavior.SetNull);

			builder.HasIndex(p => p.Code).IsUnique(); // Otomatik olarak tenant-aware yapılacak

			builder.HasMany(p => p.Addresses)
				.WithOne(x => x.Customer)
				.HasForeignKey(x => x.CustomerId);

			builder.HasMany(p => p.Officials)
				.WithOne(x => x.Customer)
				.HasForeignKey(x => x.CustomerId);

			builder.HasMany(p => p.SecondaryEmails)
				.WithOne(x => x.Customer)
				.HasForeignKey(x => x.CustomerId);

			builder.HasMany(p => p.Tags)
				.WithOne(x => x.Customer)
				.HasForeignKey(x => x.CustomerId);

			builder.HasMany(p => p.Documents)
				.WithOne(x => x.Customer)
				.HasForeignKey(x => x.CustomerId);

			builder.HasMany(p => p.Sectors)
				.WithOne(x => x.Customer)
				.HasForeignKey(x => x.CustomerId);

			builder.HasMany(p => p.CustomFields)
				.WithOne(x => x.Customer)
				.HasForeignKey(x => x.CustomerId);

			builder.HasMany(p => p.Phones)
				.WithOne(x => x.Customer)
				.HasForeignKey(x => x.CustomerId);

			builder.HasMany(p => p.Notes)
				.WithOne(x => x.Customer)
				.HasForeignKey(x => x.CustomerId);
		}
	}
}


