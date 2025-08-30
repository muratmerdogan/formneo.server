using System;
using System.ComponentModel.DataAnnotations;

namespace vesa.core.Models.CRM
{
	public class SpecialDay : vesa.core.Models.BaseEntity
	{
		public Guid CustomerId { get; set; }
		public string Title { get; set; }
		public DateTime Date { get; set; } // Asıl gün
		public bool IsAnnual { get; set; } // Her yıl tekrarlansın mı
		public int AdvanceNotifyDays { get; set; } // Kaç gün önce hatırlat
		public string Channel { get; set; } // Email,SMS,Push
		public string MessageTemplate { get; set; }

		public Customer Customer { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}


