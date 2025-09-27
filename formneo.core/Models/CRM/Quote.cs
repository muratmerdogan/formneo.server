using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vesa.core.Models.CRM
{
	public enum QuoteStatus
	{
		Draft,
		Sent,
		Accepted,
		Rejected,
		Expired
	}

	public class Quote : vesa.core.Models.BaseEntity
	{
		public Guid CustomerId { get; set; }
		public Guid? OpportunityId { get; set; }
		public string QuoteNo { get; set; }
		public DateTime QuoteDate { get; set; }
		public DateTime? ValidUntil { get; set; }
		public QuoteStatus Status { get; set; }
		public string Currency { get; set; }
		public decimal Subtotal { get; set; }
		public decimal DiscountTotal { get; set; }
		public decimal TaxTotal { get; set; }
		public decimal GrandTotal { get; set; }
		public string Notes { get; set; }

		public Customer Customer { get; set; }
		public Opportunity Opportunity { get; set; }
		public ICollection<QuoteLine> Lines { get; set; }
	}

	public class QuoteLine : vesa.core.Models.BaseEntity
	{
		public Guid QuoteId { get; set; }
		public string ItemCode { get; set; }
		public string ItemName { get; set; }
		public string Unit { get; set; }
		public decimal Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal DiscountRate { get; set; }
		public decimal TaxRate { get; set; }
		public decimal LineTotal { get; set; }

		public Quote Quote { get; set; }
	}
}


