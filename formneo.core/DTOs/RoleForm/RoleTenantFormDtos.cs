using System;
using System.Collections.Generic;

namespace formneo.core.DTOs.RoleForm
{
    public class RoleTenantFormListDto
    {
        public Guid Id { get; set; }
        public Guid FormTenantRoleId { get; set; }
        public Guid FormId { get; set; }
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class RoleTenantFormBulkSaveDto
    {
        public Guid FormTenantRoleId { get; set; }
        public List<RoleTenantFormPermissionDto> FormPermissions { get; set; } = new();
    }

    public class RoleTenantFormPermissionDto
    {
        public Guid FormId { get; set; }
        public string? FormName { get; set; }
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    // Tek adımda rol oluştur/güncelle ve formları bağla
    public class RoleTenantFormSaveWithRoleDto
    {
        // Mevcut rol güncellenecekse gönderilir; boşsa yeni rol oluşturulur
        public Guid? FormTenantRoleId { get; set; }

        public string RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public bool? RoleIsActive { get; set; }

        public List<RoleTenantFormPermissionDto> FormPermissions { get; set; } = new();
    }

    // Insert: yeni rol oluşturur ve formları bağlar (role id almaz)
    public class RoleTenantFormInsertDto
    {
        public string RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public bool? RoleIsActive { get; set; }
        public List<RoleTenantFormPermissionDto> FormPermissions { get; set; } = new();
    }

    // Update: mevcut rol id'si ile formları günceller (rol adı opsiyonel)
    public class RoleTenantFormUpdateDto
    {
        public Guid FormTenantRoleId { get; set; }
        public string? RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public bool? RoleIsActive { get; set; }
        public List<RoleTenantFormPermissionDto> FormPermissions { get; set; } = new();
    }

    // Detay: bir rol ve ona bağlı n form
    public class RoleTenantFormDetailDto
    {
        public Guid FormTenantRoleId { get; set; }
        public string RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public bool IsActive { get; set; }
        public List<RoleTenantFormPermissionDto> Forms { get; set; } = new();
    }
}


