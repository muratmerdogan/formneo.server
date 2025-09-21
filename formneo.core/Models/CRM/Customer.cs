using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vesa.core.Models.Lookup;

namespace vesa.core.Models.CRM
{

	public enum LifecycleStage
	{
		Lead,
		Qualified,
		Opportunity,
		Customer,
		Evangelist
	}

	public class Customer : vesa.core.Models.BaseEntity
	{
		// Lookup FK + Navigation (örnek: CustomerType)
		public Guid? CustomerTypeId { get; set; }
		[ForeignKey(nameof(CustomerTypeId))]
		public LookupItem? CustomerTypeItem { get; set; }

        public Guid? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public LookupItem? CategoryItem { get; set; }
 
		public string Status { get; set; }

		public string Name { get; set; }
		public string? LegalName { get; set; }
		public string? CompanyType { get; set; }
		public string Code { get; set; }
		public string? TaxOffice { get; set; }
		public string? TaxNumber { get; set; }
		public bool IsReferenceCustomer { get; set; }
		public string? LogoFilePath { get; set; }
		public string? Note { get; set; }
		public string? Website { get; set; }
		public string? TwitterUrl { get; set; }
		public string? FacebookUrl { get; set; }
		public string? LinkedinUrl { get; set; }
		public string? InstagramUrl { get; set; }

		// Yeni alanlar
		public string? OwnerId { get; set; }
		public LifecycleStage LifecycleStage { get; set; } = LifecycleStage.Lead;
		public DateTime? NextActivityDate { get; set; }

		// Navigations
		public ICollection<CustomerAddress> Addresses { get; set; }
		public ICollection<CustomerOfficial> Officials { get; set; }
		public ICollection<CustomerEmail> SecondaryEmails { get; set; }
		public ICollection<CustomerTag> Tags { get; set; }
		public ICollection<CustomerDocument> Documents { get; set; }
		public ICollection<CustomerSector> Sectors { get; set; }
		public ICollection<CustomerCustomField> CustomFields { get; set; }
		public ICollection<CustomerPhone> Phones { get; set; }
		public ICollection<CustomerNote> Notes { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}


