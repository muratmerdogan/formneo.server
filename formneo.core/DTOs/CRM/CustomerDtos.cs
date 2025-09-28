using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using vesa.core.Models.CRM;
using vesa.core.DTOs;

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
		public uint ConcurrencyToken { get; set; }
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
		public uint ConcurrencyToken { get; set; }
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
		public uint ConcurrencyToken { get; set; }
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
		public uint ConcurrencyToken { get; set; }
	}

	public class CustomerPhoneDto
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public string Label { get; set; }
		public string Number { get; set; }
		public bool IsPrimary { get; set; }
		public bool IsActive { get; set; }
		public uint ConcurrencyToken { get; set; }
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
		public uint ConcurrencyToken { get; set; }
	}

	public class CustomerNoteDto
	{
		public Guid Id { get; set; }
		public DateTime Date { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public uint ConcurrencyToken { get; set; }
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
		public uint ConcurrencyToken { get; set; }
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
	}

	public class CustomerInsertDto
	{
		[StringLength(512, ErrorMessage = "Logo dosya yolu en fazla 512 karakter olabilir")]
        public string? LogoFilePath { get; set; }

		[Required(ErrorMessage = "Müşteri adı zorunludur")]
		[StringLength(256, ErrorMessage = "Müşteri adı en fazla 256 karakter olabilir")]
        public string Name { get; set; }

		[Required(ErrorMessage = "Yasal adı zorunludur")]
		[StringLength(256, ErrorMessage = "Yasal adı en fazla 256 karakter olabilir")]
		public string LegalName { get; set; }

		[Required(ErrorMessage = "Müşteri kodu zorunludur")]
		[StringLength(64, ErrorMessage = "Müşteri kodu en fazla 64 karakter olabilir")]
		public string Code { get; set; }

        public Guid? CustomerTypeId { get; set; }
		public Guid? CategoryId { get; set; }

		[Required(ErrorMessage = "Vergi dairesi zorunludur")]
		[StringLength(128, ErrorMessage = "Vergi dairesi en fazla 128 karakter olabilir")]
		public string TaxOffice { get; set; }

		[Required(ErrorMessage = "Vergi numarası zorunludur")]
		[StringLength(64, ErrorMessage = "Vergi numarası en fazla 64 karakter olabilir")]
		public string TaxNumber { get; set; }

		public bool IsReferenceCustomer { get; set; }

		[Required(ErrorMessage = "Web sitesi adresi zorunludur")]
		[Url(ErrorMessage = "Geçerli bir web sitesi adresi giriniz")]
		[StringLength(512, ErrorMessage = "Web sitesi adresi en fazla 512 karakter olabilir")]
        public string Website { get; set; }

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


		[StringLength(256, ErrorMessage = "Sahip ID'si en fazla 256 karakter olabilir")]
		public string? OwnerId { get; set; }

		[Range(0, 4, ErrorMessage = "Yaşam döngüsü aşaması 0-4 arasında olmalıdır")]
		public int? LifecycleStage { get; set; }

        public DateTime? NextActivityDate { get; set; }

		[Range(0, int.MaxValue, ErrorMessage = "Durum geçerli bir değer olmalıdır")]
		public int Status { get; set; }

		[EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
		[StringLength(256, ErrorMessage = "E-posta adresi en fazla 256 karakter olabilir")]
        public string? DefaultNotificationEmail { get; set; }
		public List<CustomerOfficialDto>? Officials { get; set; }

		public List<CustomFieldDto>? CustomFields { get; set; }
        public List<CustomerEmailDto>? Emails { get; set; }
        public List<CustomerAddressDto>? Addresses { get; set; }
        public List<CustomerPhoneDto>? Phones { get; set; }
        public List<CustomerNoteDto>? Notes { get; set; }

        public List<CustomerDocument>? Documents { get; set; }


    }

	public class CustomerUpdateDto : BaseUpdateDto
	{
		[StringLength(512, ErrorMessage = "Logo dosya yolu en fazla 512 karakter olabilir")]
        public string? LogoFilePath { get; set; }

		[Required(ErrorMessage = "Müşteri adı zorunludur")]
		[StringLength(256, ErrorMessage = "Müşteri adı en fazla 256 karakter olabilir")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Yasal adı zorunludur")]
		[StringLength(256, ErrorMessage = "Yasal adı en fazla 256 karakter olabilir")]
		public string LegalName { get; set; }

		[Required(ErrorMessage = "Müşteri kodu zorunludur")]
		[StringLength(64, ErrorMessage = "Müşteri kodu en fazla 64 karakter olabilir")]
		public string Code { get; set; }

		public Guid? CustomerTypeId { get; set; }
		public Guid? CategoryId { get; set; }

		[Range(0, int.MaxValue, ErrorMessage = "Durum geçerli bir değer olmalıdır")]
		public int Status { get; set; }

		[Required(ErrorMessage = "Web sitesi adresi zorunludur")]
		[Url(ErrorMessage = "Geçerli bir web sitesi adresi giriniz")]
		[StringLength(512, ErrorMessage = "Web sitesi adresi en fazla 512 karakter olabilir")]
		public string Website { get; set; }

		[Required(ErrorMessage = "Vergi dairesi zorunludur")]
		[StringLength(128, ErrorMessage = "Vergi dairesi en fazla 128 karakter olabilir")]
		public string TaxOffice { get; set; }

		[Required(ErrorMessage = "Vergi numarası zorunludur")]
		[StringLength(64, ErrorMessage = "Vergi numarası en fazla 64 karakter olabilir")]
		public string TaxNumber { get; set; }

		public bool IsReferenceCustomer { get; set; }

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

		[StringLength(256, ErrorMessage = "Sahip ID'si en fazla 256 karakter olabilir")]
		public string? OwnerId { get; set; }

		[Range(0, 4, ErrorMessage = "Yaşam döngüsü aşaması 0-4 arasında olmalıdır")]
		public int LifecycleStage { get; set; }

		public DateTime? NextActivityDate { get; set; }

	}
	public class CustomerListDto : BaseListDto
	{
		public Guid? CustomerTypeId { get; set; }
		public string? CustomerTypeText { get; set; }
		public Guid? CategoryId { get; set; }
		public string? CategoryText { get; set; }
		public int Status { get; set; }
		public string Name { get; set; }
		public string LegalName { get; set; }
		public string Code { get; set; }
		public string TaxOffice { get; set; }
		public string TaxNumber { get; set; }
		public bool IsReferenceCustomer { get; set; }
		public string? LogoFilePath { get; set; }
		public string? Note { get; set; }
		public string? Website { get; set; }
		public string? TwitterUrl { get; set; }
		public string? FacebookUrl { get; set; }
		public string? LinkedinUrl { get; set; }
		public string? InstagramUrl { get; set; }
		public string? OwnerId { get; set; }
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
	}

	public class CustomerDocumentUploadDto
	{
		public Guid CustomerId { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
	}

	// Performans için optimize edilmiş DTO'lar
	public class CustomerBasicDto : BaseListDto
	{
		public Guid? CustomerTypeId { get; set; }
		public string? CustomerTypeText { get; set; }
		public Guid? CategoryId { get; set; }
		public string? CategoryText { get; set; }
		public string Name { get; set; }
		public string LegalName { get; set; }
		public string Code { get; set; }
		public string TaxNumber { get; set; }
		public bool IsReferenceCustomer { get; set; }
		public string? LogoFilePath { get; set; }
		public string? Website { get; set; }
		public string? TwitterUrl { get; set; }
		public string? FacebookUrl { get; set; }
		public string? LinkedinUrl { get; set; }
		public string? InstagramUrl { get; set; }
		public int LifecycleStage { get; set; }
		public DateTime? NextActivityDate { get; set; }
		public string? OwnerId { get; set; }
		public int Status { get; set; }
	}

	public class CustomerPagedResultDto
	{
		public List<CustomerBasicDto> Items { get; set; }
		public int TotalCount { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
		public bool HasNextPage { get; set; }
		public bool HasPreviousPage { get; set; }
	}
}


