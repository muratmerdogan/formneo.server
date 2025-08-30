using System;
using System.ComponentModel.DataAnnotations;

namespace vesa.core.Models.CRM
{
	public enum OfficialRole
	{
		SatinAlma,
		Teknik,
		Finans,
		KararVerici,
		Diger
	}

	public class CustomerOfficial : vesa.core.Models.BaseEntity
	{
		public Guid CustomerId { get; set; }
		public string FullName { get; set; }
		public string Title { get; set; }
		public string Department { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public OfficialRole Role { get; set; }
		public bool IsPrimary { get; set; }
		public bool KvkkConsent { get; set; }

		public Customer Customer { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}


