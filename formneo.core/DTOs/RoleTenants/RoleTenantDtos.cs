using System;

namespace vesa.core.DTOs.RoleTenants
{
	public class RoleTenantListDto
	{
		public Guid Id { get; set; }
		public string RoleId { get; set; }
		public Guid TenantId { get; set; }
		public bool IsActive { get; set; }
		public bool IsLocked { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
	}

	public class RoleTenantInsertDto
	{
		public string RoleId { get; set; }
		public Guid TenantId { get; set; }
		public bool IsActive { get; set; } = true;
		public bool IsLocked { get; set; } = false;
	}

	public class RoleTenantUpdateDto
	{
		public Guid Id { get; set; }
		public bool IsActive { get; set; }
		public bool IsLocked { get; set; }
	}

	public class RoleTenantBulkItemDto
	{
		public Guid TenantId { get; set; }
		public bool IsActive { get; set; }
		public bool IsLocked { get; set; }
	}

	public class RoleTenantBulkSaveDto
	{
		public string RoleId { get; set; }
		public RoleTenantBulkItemDto[] Items { get; set; }
	}
}



