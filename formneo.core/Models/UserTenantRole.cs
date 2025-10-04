using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace formneo.core.Models
{
	public class UserTenantRole : GlobalBaseEntity
	{
		[ForeignKey(nameof(User))]
		public string UserId { get; set; }
		public virtual UserApp User { get; set; }

		[ForeignKey(nameof(RoleTenant))]
		public Guid RoleTenantId { get; set; }
		public virtual RoleTenant RoleTenant { get; set; }

		public bool IsActive { get; set; } = true;
	}
}


