using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using formneo.core.Models.CRM;

namespace formneo.core.Models
{
	public enum ProjectTaskStatus
	{
		Bekliyor = 0,
		Islemde = 1,
		BeklemeyeAlindi = 2,
		Tamamlandi = 3,
		IptalEdildi = 4
	}

	public class ProjectTask : BaseEntity
	{
		[Required]
		[ForeignKey("TenantProject")]
		public Guid ProjectId { get; set; }
		public virtual TenantProject TenantProject { get; set; }

		[ForeignKey("Customer")]
		public Guid? CustomerId { get; set; }
		public virtual Customer? Customer { get; set; }

		[Required]
		[StringLength(200)]
		public string Name { get; set; }

		[StringLength(2000)]
		public string? Description { get; set; }

		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		[Required]
		public ProjectTaskStatus Status { get; set; }

		[ForeignKey("UserApp")] 
		public string? AssigneeId { get; set; }
		public virtual UserApp? Assignee { get; set; }
	}
}


