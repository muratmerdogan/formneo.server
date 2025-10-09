using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using formneo.core.Models.CRM;

namespace formneo.core.Models
{
	public class TenantProject : BaseEntity
	{
		[Required]
		[StringLength(200)]
		public string Name { get; set; }

		[StringLength(2000)]
		public string? Description { get; set; }

		[ForeignKey("Customer")]
		public Guid? CustomerId { get; set; }
		public virtual Customer? Customer { get; set; }

		public bool IsPrivate { get; set; }
	}
}


