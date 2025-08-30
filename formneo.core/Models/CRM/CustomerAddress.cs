using System;
using System.ComponentModel.DataAnnotations;

namespace vesa.core.Models.CRM
{
	public enum AddressType
	{
		Fatura,
		Teslimat,
		Merkez,
		Sube,
		Diger
	}

	public class CustomerAddress : vesa.core.Models.BaseEntity
	{
		public Guid CustomerId { get; set; }
		public AddressType Type { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public string PostalCode { get; set; }
		public string Line1 { get; set; }
		public string Line2 { get; set; }
		public bool IsDefaultBilling { get; set; }
		public bool IsDefaultShipping { get; set; }
		public bool IsBilling { get; set; } = false;
		public bool IsShipping { get; set; } = false;
		public bool IsActive { get; set; } = true;

		public Customer Customer { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}


