using System;
using System.Collections.Generic;

namespace formneo.core.DTOs.RoleForm
{
    public class UserTenantFormRoleListDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid FormTenantRoleId { get; set; }
        public string? FormTenantRoleName { get; set; }
        public bool IsActive { get; set; }
    }

    public class UserTenantFormRoleBulkSaveDto
    {
        public string UserId { get; set; }
        public List<Guid> FormTenantRoleIds { get; set; } = new();
    }
}


