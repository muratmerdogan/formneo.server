using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace formneo.core.DTOs.Security
{
    public class UpsertUserPermissionsDto
    {
        [Required]
        public string ResourceKey { get; set; } = default!;
        public List<UserPermissionItem>? Items { get; set; }
    }

    public class UserPermissionItem
    {
        [Required]
        public string UserId { get; set; } = default!;
        public int AllowedMask { get; set; }
        public int DeniedMask { get; set; }
    }
}


