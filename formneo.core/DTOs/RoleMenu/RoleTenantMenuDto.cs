using System;
using System.Collections.Generic;

namespace formneo.core.DTOs
{
    public class RoleTenantMenuListDto
    {
        public Guid Id { get; set; }
        public string RoleId { get; set; }
        public Guid TenantId { get; set; }
        public Guid MenuId { get; set; }
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class RoleTenantMenuBulkSaveDto
    {
        public string RoleId { get; set; }
        public Guid TenantId { get; set; }
        public List<MenuPermissionDto> MenuPermissions { get; set; }
    }
}


