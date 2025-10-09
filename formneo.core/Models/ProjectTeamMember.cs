using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace formneo.core.Models
{
	public class ProjectTeamMember : BaseEntity
	{
		[ForeignKey("TenantProject")]
		public Guid ProjectId { get; set; }
		public virtual TenantProject TenantProject { get; set; }

		[ForeignKey("UserApp")]
		public string UserId { get; set; }
		public virtual UserApp User { get; set; }

		public string? Role { get; set; }
		public bool IsActive { get; set; } = true;
	}
}


