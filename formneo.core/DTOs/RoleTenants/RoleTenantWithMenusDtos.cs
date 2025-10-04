using System;
using System.Collections.Generic;
using formneo.core.DTOs;

namespace formneo.core.DTOs.RoleTenants
{
	public class RoleWithMenusItemDto
	{
		public string RoleId { get; set; }
		public string RoleName { get; set; }
		public bool IsActive { get; set; }
		public bool IsLocked { get; set; }
		public bool Selected { get; set; }
		public List<MenuPermissionDto> MenuPermissions { get; set; } = new List<MenuPermissionDto>();
	}

	public class RoleTenantWithMenusBulkSaveDto
	{
		public Guid TenantId { get; set; }
		public List<RoleWithMenusItemDto> Items { get; set; } = new List<RoleWithMenusItemDto>();
	}

	public class RoleTenantWithMenusGetDto
	{
		public Guid TenantId { get; set; }
		public List<RoleWithMenusItemDto> Items { get; set; } = new List<RoleWithMenusItemDto>();
	}
}




