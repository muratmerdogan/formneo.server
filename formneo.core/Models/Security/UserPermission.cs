using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace formneo.core.Models.Security
{
    // Tenant-izole kullanıcı bazlı override izinler
    public class UserPermission : BaseEntity
    {
        [Required]
        [MaxLength(128)]
        public string ResourceKey { get; set; }

        [Required]
        public string UserId { get; set; }

        public int AllowedMask { get; set; }
        public int DeniedMask { get; set; }
    }
}


