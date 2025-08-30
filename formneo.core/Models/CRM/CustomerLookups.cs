using System;
using System.ComponentModel.DataAnnotations;

namespace vesa.core.Models.CRM
{
	public class CustomerTag : vesa.core.Models.BaseEntity
	{
		public Guid CustomerId { get; set; }
		public string Tag { get; set; }
		public Customer Customer { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}

	public class CustomerEmail : vesa.core.Models.BaseEntity
	{
		public Guid CustomerId { get; set; }
		public string Email { get; set; }
		public string Description { get; set; }
		public bool Notify { get; set; } = false;
		public bool Bulk { get; set; } = false;
		public bool IsActive { get; set; } = true;
		public bool IsPrimary { get; set; } = false;
		public Customer Customer { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}

	public class CustomerDocument : vesa.core.Models.BaseEntity
	{
		public Guid CustomerId { get; set; }
		public string FileName { get; set; }
		public string FilePath { get; set; }
		public string ContentType { get; set; }
		public long FileSize { get; set; }
		public string Description { get; set; }
		public string Category { get; set; } // Sözleşme, Fatura, Diğer vb.
		public Customer Customer { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}

	public class CustomerSector : vesa.core.Models.BaseEntity
	{
		public Guid CustomerId { get; set; }
		public string Sector { get; set; }
		public Customer Customer { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}

	public class CustomerCustomField : vesa.core.Models.BaseEntity
	{
		public Guid CustomerId { get; set; }
		public string FieldId { get; set; }
		public string FieldType { get; set; }
		public string Label { get; set; }
		public string ValueJson { get; set; }
		public Customer Customer { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}

	public class CustomerPhone : vesa.core.Models.BaseEntity
	{
		public Guid CustomerId { get; set; }
		public string Label { get; set; }
		public string Number { get; set; }
		public bool IsPrimary { get; set; } = false;
		public bool IsActive { get; set; } = true;
		public Customer Customer { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}

	public class CustomerNote : vesa.core.Models.BaseEntity
	{
		public Guid CustomerId { get; set; }
		public DateTime Date { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public Customer Customer { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}


