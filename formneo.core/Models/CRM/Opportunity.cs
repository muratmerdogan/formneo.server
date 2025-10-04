using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace formneo.core.Models.CRM
{
	public enum OpportunityStage
	{
		New,
		Qualified,
		Proposal,
		Negotiation,
		Won,
		Lost
	}

	public class Opportunity : formneo.core.Models.BaseEntity
	{
		public Guid CustomerId { get; set; }
		public string Title { get; set; }
		public OpportunityStage Stage { get; set; }
		public decimal? Amount { get; set; }
		public string Currency { get; set; }
		public int? Probability { get; set; }
		public DateTime? ExpectedCloseDate { get; set; }
		public string Source { get; set; }
		public string OwnerUserId { get; set; }
		public string Description { get; set; }

		public Customer Customer { get; set; }
		public ICollection<Activity> Activities { get; set; }
	}
}


