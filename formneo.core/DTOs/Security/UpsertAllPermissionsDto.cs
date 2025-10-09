using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using formneo.core.Models.Security;

namespace formneo.core.DTOs.Security
{
    public class UpsertAllPermissionsDto
    {
        [Required]
        public UpdateResourcePermissionDto Resource { get; set; } = default!;
        public string? ResourceKey => Resource?.ResourceKey;
        public List<UserPermissionItem>? Users { get; set; }
    }
}


