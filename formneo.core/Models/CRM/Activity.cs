using System;
using System.ComponentModel.DataAnnotations;

namespace formneo.core.Models.CRM
{
	public enum ActivityType
	{
		Call,
		Email,
		Task,
		Meeting,
		Note
	}

	public enum ActivityStatus
	{
		Open,
		Completed,
		Canceled
	}

	public class Activity : formneo.core.Models.BaseEntity
	{
		public Guid CustomerId { get; set; }
		public Guid? OpportunityId { get; set; }
		public ActivityType Type { get; set; }
		public ActivityStatus Status { get; set; }
		public DateTime? DueDate { get; set; }
		public string Subject { get; set; }
		public string Description { get; set; }
		public string AssignedToUserId { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }

		public Customer Customer { get; set; }
		public Opportunity Opportunity { get; set; }
	}
}


