using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

 namespace vesa.core.Models
 {
 	public class RoleTenant : BaseEntity
	{
		[ForeignKey(nameof(Role))]
		public string RoleId { get; set; }
		public virtual IdentityRole Role { get; set; }

		[ForeignKey(nameof(Tenant))]
		public Guid TenantId { get; set; }
		public virtual MainClient Tenant { get; set; }

		public bool IsActive { get; set; }
		public bool IsLocked { get; set; }
	}
}


