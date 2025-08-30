using System;
using System.Collections.Generic;

namespace vesa.core.DTOs.CRM
{
	public class OpportunityDto
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public string Title { get; set; }
		public int Stage { get; set; }
		public decimal? Amount { get; set; }
		public string Currency { get; set; }
		public int? Probability { get; set; }
		public DateTime? ExpectedCloseDate { get; set; }
		public string Source { get; set; }
		public string OwnerUserId { get; set; }
		public string Description { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class ActivityDto
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public Guid? OpportunityId { get; set; }
		public int Type { get; set; }
		public int Status { get; set; }
		public DateTime? DueDate { get; set; }
		public string Subject { get; set; }
		public string Description { get; set; }
		public string AssignedToUserId { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class MeetingDto
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public Guid? OpportunityId { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string Subject { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
		public string OrganizerUserId { get; set; }
		public string AttendeesJson { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class ReminderDto
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public Guid? ActivityId { get; set; }
		public DateTime RemindAt { get; set; }
		public string Message { get; set; }
		public string Channel { get; set; }
		public bool IsSent { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class QuoteLineDto
	{
		public Guid Id { get; set; }
		public string ItemCode { get; set; }
		public string ItemName { get; set; }
		public string Unit { get; set; }
		public decimal Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal DiscountRate { get; set; }
		public decimal TaxRate { get; set; }
		public decimal LineTotal { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class QuoteDto
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public Guid? OpportunityId { get; set; }
		public string QuoteNo { get; set; }
		public DateTime QuoteDate { get; set; }
		public DateTime? ValidUntil { get; set; }
		public int Status { get; set; }
		public string Currency { get; set; }
		public decimal Subtotal { get; set; }
		public decimal DiscountTotal { get; set; }
		public decimal TaxTotal { get; set; }
		public decimal GrandTotal { get; set; }
		public string Notes { get; set; }
		public List<QuoteLineDto> Lines { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class SpecialDayDto
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public string Title { get; set; }
		public DateTime Date { get; set; }
		public bool IsAnnual { get; set; }
		public int AdvanceNotifyDays { get; set; }
		public string Channel { get; set; }
		public string MessageTemplate { get; set; }
		public byte[] RowVersion { get; set; }
	}

	public class CrmChangeLogDto
	{
		public Guid Id { get; set; }
		public string EntityName { get; set; }
		public Guid EntityId { get; set; }
		public Guid? CustomerId { get; set; }
		public int Action { get; set; }
		public string PropertyName { get; set; }
		public string OldValue { get; set; }
		public string NewValue { get; set; }
		public string ChangedBy { get; set; }
		public DateTime ChangedDate { get; set; }
	}
}
