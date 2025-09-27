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
 
		[StringLength(64, ErrorMessage = "Durum en fazla 64 karakter olabilir")]
		public string Status { get; set; }

		[Required(ErrorMessage = "Müşteri adı zorunludur")]
		[StringLength(256, ErrorMessage = "Müşteri adı en fazla 256 karakter olabilir")]
		public string Name { get; set; }

		[StringLength(256, ErrorMessage = "Yasal adı en fazla 256 karakter olabilir")]
		public string? LegalName { get; set; }

		[StringLength(128, ErrorMessage = "Şirket türü en fazla 128 karakter olabilir")]
		public string? CompanyType { get; set; }

		[Required(ErrorMessage = "Müşteri kodu zorunludur")]
		[StringLength(64, ErrorMessage = "Müşteri kodu en fazla 64 karakter olabilir")]
		public string Code { get; set; }

		[StringLength(128, ErrorMessage = "Vergi dairesi en fazla 128 karakter olabilir")]
		public string? TaxOffice { get; set; }

		[StringLength(64, ErrorMessage = "Vergi numarası en fazla 64 karakter olabilir")]
		public string? TaxNumber { get; set; }

		public bool IsReferenceCustomer { get; set; }

		[StringLength(512, ErrorMessage = "Logo dosya yolu en fazla 512 karakter olabilir")]
		public string? LogoFilePath { get; set; }

		public string? Note { get; set; }

		[Url(ErrorMessage = "Geçerli bir web sitesi adresi giriniz")]
		[StringLength(512, ErrorMessage = "Web sitesi adresi en fazla 512 karakter olabilir")]
		public string? Website { get; set; }

		[Url(ErrorMessage = "Geçerli bir Twitter URL'si giriniz")]
		[StringLength(512, ErrorMessage = "Twitter URL'si en fazla 512 karakter olabilir")]
		public string? TwitterUrl { get; set; }

		[Url(ErrorMessage = "Geçerli bir Facebook URL'si giriniz")]
		[StringLength(512, ErrorMessage = "Facebook URL'si en fazla 512 karakter olabilir")]
		public string? FacebookUrl { get; set; }

		[Url(ErrorMessage = "Geçerli bir LinkedIn URL'si giriniz")]
		[StringLength(512, ErrorMessage = "LinkedIn URL'si en fazla 512 karakter olabilir")]
		public string? LinkedinUrl { get; set; }

		[Url(ErrorMessage = "Geçerli bir Instagram URL'si giriniz")]
		[StringLength(512, ErrorMessage = "Instagram URL'si en fazla 512 karakter olabilir")]
		public string? InstagramUrl { get; set; }

		// Yeni alanlar
		[StringLength(256, ErrorMessage = "Sahip ID'si en fazla 256 karakter olabilir")]
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
	}
}


