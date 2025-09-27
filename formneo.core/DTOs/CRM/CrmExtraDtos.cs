using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vesa.core.DTOs.CRM
{
	// Base DTO for common fields
	public class OpportunityDto
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public string Title { get; set; }
		public int Stage { get; set; }
		public string StageText { get; set; }
		public decimal? Amount { get; set; }
		public string Currency { get; set; }
		public int? Probability { get; set; }
		public DateTime? ExpectedCloseDate { get; set; }
		public string Source { get; set; }
		public string OwnerUserId { get; set; }
		public string Description { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
	}

	public class OpportunityListDto
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public string CustomerName { get; set; }
		public string Title { get; set; }
		public int Stage { get; set; }
		public string StageText { get; set; }
		public decimal? Amount { get; set; }
		public string Currency { get; set; }
		public int? Probability { get; set; }
		public DateTime? ExpectedCloseDate { get; set; }
		public string OwnerUserId { get; set; }
		public DateTime CreatedDate { get; set; }
	}

	public class OpportunityInsertDto
	{
		[Required]
		public Guid CustomerId { get; set; }
		
		[Required]
		[StringLength(256, ErrorMessage = "Title cannot exceed 256 characters")]
		public string Title { get; set; }
		
		[Range(0, 5, ErrorMessage = "Stage must be between 0-5")]
		public int Stage { get; set; } = 0; // Default: New
		
		[Range(0, double.MaxValue, ErrorMessage = "Amount must be positive")]
		public decimal? Amount { get; set; }
		
		[StringLength(8, ErrorMessage = "Currency code cannot exceed 8 characters")]
		public string Currency { get; set; } = "TRY";
		
		[Range(0, 100, ErrorMessage = "Probability must be between 0-100")]
		public int? Probability { get; set; }
		
		public DateTime? ExpectedCloseDate { get; set; }
		
		[StringLength(128, ErrorMessage = "Source cannot exceed 128 characters")]
		public string? Source { get; set; }
		
		[Required]
		public string OwnerUserId { get; set; }
		
		[StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
		public string? Description { get; set; }
	}

	public class OpportunityUpdateDto
	{
		[Required]
		public Guid Id { get; set; }
		
		[Required]
		public Guid CustomerId { get; set; }
		
		[Required]
		[StringLength(256, ErrorMessage = "Title cannot exceed 256 characters")]
		public string Title { get; set; }
		
		[Range(0, 5, ErrorMessage = "Stage must be between 0-5")]
		public int Stage { get; set; }
		
		[Range(0, double.MaxValue, ErrorMessage = "Amount must be positive")]
		public decimal? Amount { get; set; }
		
		[StringLength(8, ErrorMessage = "Currency code cannot exceed 8 characters")]
		public string Currency { get; set; }
		
		[Range(0, 100, ErrorMessage = "Probability must be between 0-100")]
		public int? Probability { get; set; }
		
		public DateTime? ExpectedCloseDate { get; set; }
		
		[StringLength(128, ErrorMessage = "Source cannot exceed 128 characters")]
		public string? Source { get; set; }
		
		[Required]
		public string OwnerUserId { get; set; }
		
		[StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
		public string? Description { get; set; }
		
	}

	public class OpportunityPagedResultDto
	{
		public List<OpportunityListDto> Items { get; set; }
		public int TotalCount { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
		public bool HasNextPage { get; set; }
		public bool HasPreviousPage { get; set; }
	}

	public class OpportunityDashboardDto
	{
		public OpportunityMetricsDto Metrics { get; set; }
		public List<OpportunityStageStatsDto> StageStats { get; set; }
		public List<OpportunityTrendDto> MonthlyTrends { get; set; }
		public List<OpportunityTopCustomerDto> TopCustomers { get; set; }
	}

	public class OpportunityMetricsDto
	{
		public int TotalOpportunities { get; set; }
		public decimal TotalAmount { get; set; }
		public decimal AverageAmount { get; set; }
		public decimal WeightedAmount { get; set; } // Amount * Probability
		public int WonCount { get; set; }
		public int LostCount { get; set; }
		public decimal WinRate { get; set; } // Win percentage
		public decimal AverageDaysToClose { get; set; }
	}

	public class OpportunityStageStatsDto
	{
		public int Stage { get; set; }
		public string StageName { get; set; }
		public int Count { get; set; }
		public decimal TotalAmount { get; set; }
		public decimal AverageAmount { get; set; }
		public decimal Percentage { get; set; }
	}

	public class OpportunityTrendDto
	{
		public int Year { get; set; }
		public int Month { get; set; }
		public string MonthName { get; set; }
		public int CreatedCount { get; set; }
		public int WonCount { get; set; }
		public int LostCount { get; set; }
		public decimal CreatedAmount { get; set; }
		public decimal WonAmount { get; set; }
		public decimal LostAmount { get; set; }
	}

	public class OpportunityTopCustomerDto
	{
		public Guid CustomerId { get; set; }
		public string CustomerName { get; set; }
		public int OpportunityCount { get; set; }
		public decimal TotalAmount { get; set; }
		public decimal WonAmount { get; set; }
		public int WonCount { get; set; }
		public decimal WinRate { get; set; }
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
