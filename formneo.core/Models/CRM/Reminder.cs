using System;
using System.ComponentModel.DataAnnotations;

namespace vesa.core.Models.CRM
{
	public class Reminder : vesa.core.Models.BaseEntity
	{
		public Guid CustomerId { get; set; }
		public Guid? ActivityId { get; set; }
		public DateTime RemindAt { get; set; }
		public string Message { get; set; }
		public string Channel { get; set; } // Email, SMS, Push
		public bool IsSent { get; set; }

		public Customer Customer { get; set; }
		public Activity Activity { get; set; }
	}
}


