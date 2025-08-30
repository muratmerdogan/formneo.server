using System;
using System.Collections.Generic;

namespace vesa.core.DTOs.RoleTenants
{
    // Tenant'taki tüm rolleri kullanıcının sahip olup olmadığını işaretleyerek döner
    public class UserRoleAssignmentGetDto
    {
        public string UserId { get; set; }
        public Guid TenantId { get; set; }
        public List<UserRoleAssignmentItemDto> Roles { get; set; } = new List<UserRoleAssignmentItemDto>();
    }

    // Her rol için kullanıcının sahip olup olmadığını belirten item
    public class UserRoleAssignmentItemDto
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public bool IsAssignedToUser { get; set; } // Kullanıcı bu role sahip mi?
        public Guid? UserTenantRoleId { get; set; } // Eğer sahipse UserTenantRole ID'si
    }

    // Kullanıcıya rol atama/çıkarma için
    public class UserRoleAssignmentSaveDto
    {
        public string UserId { get; set; }
        public Guid TenantId { get; set; }
        public List<UserRoleAssignmentSaveItemDto> RoleAssignments { get; set; } = new List<UserRoleAssignmentSaveItemDto>();
    }

    // Her rol için atama/çıkarma bilgisi
    public class UserRoleAssignmentSaveItemDto
    {
        public string RoleId { get; set; }
        public bool ShouldAssign { get; set; } // true: ata, false: çıkar
    }
}
