using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vesa.core.Models.CRM
{
	public class Meeting : vesa.core.Models.BaseEntity
	{
		public Guid CustomerId { get; set; }
		public Guid? OpportunityId { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string Subject { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
		public string OrganizerUserId { get; set; }
		public string AttendeesJson { get; set; }

		public Customer Customer { get; set; }
		public Opportunity Opportunity { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}


