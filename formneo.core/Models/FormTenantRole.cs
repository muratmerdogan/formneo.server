using System;
using System.ComponentModel.DataAnnotations;

namespace formneo.core.Models
{
    public class FormTenantRole : BaseEntity
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}


