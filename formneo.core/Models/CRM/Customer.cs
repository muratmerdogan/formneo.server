using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vesa.core.Models.CRM
{
	public enum CustomerType
	{
		Bireysel,
		Kurumsal,
		Kamu,
		Diger
	}

	public enum CustomerCategory
	{
		Standart,
		Gold,
		Premium,
		VIP
	}

	public enum CustomerStatus
	{
		Aktif,
		Pasif,
		Potansiyel,
		KaraListe
	}

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
		public CustomerType CustomerType { get; set; }
		public CustomerCategory Category { get; set; }
		public CustomerStatus Status { get; set; }
		public string Name { get; set; }
		public string? LegalName { get; set; }
		public string? CompanyType { get; set; }
		public string Code { get; set; }
		public string? TaxOffice { get; set; }
		public string? TaxNumber { get; set; }
		public bool IsReferenceCustomer { get; set; }
		public string? LogoFilePath { get; set; }
		public string? Note { get; set; }

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


