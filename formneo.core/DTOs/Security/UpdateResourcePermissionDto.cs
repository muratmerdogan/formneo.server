using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using formneo.core.Models.Security;

namespace formneo.core.DTOs.Security
{
    public class UpdateResourcePermissionDto
    {
        [Required]
        public string ResourceKey { get; set; } = default!;
        public int? Mask { get; set; }
        public List<Actions>? Actions { get; set; }
    }
}


