using System;
using System.Collections.Generic;

namespace vesa.core.DTOs.CRM
{
	public class CustomerAddressDto
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public int Type { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public string PostalCode { get; set; }
		public string Line1 { get; set; }
		public string Line2 { get; set; }
		public bool IsDefaultBilling { get; set; }
		public bool IsDefaultShipping { get; set; }
		public bool IsBilling { get; set; }
		public bool IsShipping { get; set; }
		public bool IsActive { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class CustomerAddressInsertDto
	{
		public Guid CustomerId { get; set; }
		public int Type { get; set; }
		public string Country { get; set; } = "TR";
		public string City { get; set; }
		public string District { get; set; }
		public string PostalCode { get; set; }
		public string Line1 { get; set; }
		public string Line2 { get; set; }
		public bool IsDefaultBilling { get; set; } = false;
		public bool IsDefaultShipping { get; set; } = false;
		public bool IsBilling { get; set; } = true;
		public bool IsShipping { get; set; } = true;
		public bool IsActive { get; set; } = true;
	}

	public class CustomerAddressUpdateDto : CustomerAddressInsertDto
	{
		public Guid Id { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class CustomerEmailDto
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public string Email { get; set; }
		public string Description { get; set; }
		public bool Notify { get; set; }
		public bool Bulk { get; set; }
		public bool IsActive { get; set; }
		public bool IsPrimary { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class CustomerEmailInsertDto
	{
		public Guid CustomerId { get; set; }
		public string Email { get; set; }
		public string Description { get; set; }
		public bool Notify { get; set; } = false;
		public bool Bulk { get; set; } = false;
		public bool IsActive { get; set; } = true;
		public bool IsPrimary { get; set; } = false;
	}

	public class CustomerEmailUpdateDto : CustomerEmailInsertDto
	{
		public Guid Id { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class CustomerPhoneDto
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public string Label { get; set; }
		public string Number { get; set; }
		public bool IsPrimary { get; set; }
		public bool IsActive { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class CustomerPhoneInsertDto
	{
		public Guid CustomerId { get; set; }
		public string Label { get; set; }
		public string Number { get; set; }
		public bool IsPrimary { get; set; } = false;
		public bool IsActive { get; set; } = true;
	}

	public class CustomerPhoneUpdateDto : CustomerPhoneInsertDto
	{
		public Guid Id { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class CustomerNoteDto
	{
		public Guid Id { get; set; }
		public DateTime Date { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class CustomerNoteInsertDto
	{
		public Guid CustomerId { get; set; }
		public DateTime Date { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
	}

	public class CustomerNoteUpdateDto
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public DateTime Date { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class CustomerOfficialDto
	{
		public Guid Id { get; set; }
		public string FullName { get; set; }
		public string Title { get; set; }
		public string Department { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public int Role { get; set; }
		public bool IsPrimary { get; set; }
		public bool KvkkConsent { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class CustomerInsertDto
	{
		public string Name { get; set; }
		public string LegalName { get; set; }
		public string Code { get; set; }
		public int CustomerTypeId { get; set; }
		public int CategoryId { get; set; }
		public int Status { get; set; }
		public List<string> Sectors { get; set; }
		public string EmailPrimary { get; set; }
		public List<string> EmailSecondary { get; set; }
		public List<CustomerEmailDto> Emails { get; set; }
		public List<CustomerAddressDto> Addresses { get; set; }
		public List<CustomerPhoneDto> Phones { get; set; }
		public List<CustomerNoteDto> Notes { get; set; }
		public string Website { get; set; }
		public string TaxOffice { get; set; }
		public string TaxNumber { get; set; }
		public bool IsReferenceCustomer { get; set; }
		public List<string> Tags { get; set; }
		public string DefaultNotificationEmail { get; set; }
		public string TwitterUrl { get; set; }
		public string FacebookUrl { get; set; }
		public string LinkedinUrl { get; set; }
		public string InstagramUrl { get; set; }
		public string OwnerId { get; set; }
		public int LifecycleStage { get; set; }
		public DateTime? NextActivityDate { get; set; }

		// Eski alanlar (geriye uyumluluk için)
		public string CompanyType { get; set; }
		public string LogoFilePath { get; set; }
		public string Note { get; set; }
		public string Phone { get; set; }
		public string Mobile { get; set; }
		public string Fax { get; set; }
		public string PreferredContact { get; set; }
		public string UtsInstitutionNumber { get; set; }
		public string ConnectionCode { get; set; }
		public List<CustomerOfficialDto> Officials { get; set; }
		public string PaymentMethod { get; set; }
		public int? TermDays { get; set; }
		public string Currency { get; set; }
		public decimal? Discount { get; set; }
		public decimal? CreditLimit { get; set; }
		public bool EInvoice { get; set; }
		public string IBAN { get; set; }
		public string TaxExemptionCode { get; set; }
		public string ContractNo { get; set; }
		public DateTime? ContractStart { get; set; }
		public DateTime? ContractEnd { get; set; }
		public List<string> Documents { get; set; }
		public string SectorDetailsJson { get; set; }
		public List<CustomFieldDto> CustomFields { get; set; }
		public string RichNote { get; set; }
	}

	public class CustomerUpdateDto : CustomerInsertDto
	{
		public Guid Id { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class CustomerListDto
	{
		public Guid Id { get; set; }
		public int CustomerType { get; set; }
		public int Category { get; set; }
		public int Status { get; set; }
		public string Name { get; set; }
		public string LegalName { get; set; }
		public string Code { get; set; }
		public string TaxOffice { get; set; }
		public string TaxNumber { get; set; }
		public bool IsReferenceCustomer { get; set; }
		public string OwnerId { get; set; }
		public int LifecycleStage { get; set; }
		public DateTime? NextActivityDate { get; set; }

		// İlişkili tablolar
		public List<CustomerAddressDto> Addresses { get; set; }
		public List<CustomerEmailDto> Emails { get; set; }
		public List<CustomerPhoneDto> Phones { get; set; }
		public List<CustomerNoteDto> Notes { get; set; }
		public List<CustomerOfficialDto> Officials { get; set; }
		public List<string> Tags { get; set; }
		public List<string> Sectors { get; set; }
		public List<string> Documents { get; set; }
		public List<CustomFieldDto> CustomFields { get; set; }
	}

	public class CustomFieldDto
	{
		public string Id { get; set; }
		public string Type { get; set; }
		public string Label { get; set; }
		public string ValueJson { get; set; }
	}

	public class CustomerDocumentDto
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public string FileName { get; set; }
		public string FilePath { get; set; }
		public string ContentType { get; set; }
		public long FileSize { get; set; }
		public string DownloadUrl { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class CustomerDocumentInsertDto
	{
		public Guid CustomerId { get; set; }
		public string FileName { get; set; }
		public string ContentType { get; set; }
		public long FileSize { get; set; }
		public string Description { get; set; }
		public string Category { get; set; } // Sözleşme, Fatura, Diğer vb.
	}

	public class CustomerDocumentUpdateDto
	{
		public Guid Id { get; set; }
		public string FileName { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class CustomerDocumentUploadDto
	{
		public Guid CustomerId { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
	}
}


