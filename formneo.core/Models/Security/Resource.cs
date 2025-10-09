using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace formneo.core.Models.Security
{
    // Global kaynak sözlüğü: ekran/resource anahtarları ve varsayılan mask
    public class Resource : GlobalBaseEntity
    {
        [Required]
        [MaxLength(128)]
        public string ResourceKey { get; set; }

        // Varsayılan aksiyon maskesi (örn. Full)
        [Required]
        public int DefaultMask { get; set; }

        public bool IsActive { get; set; } = true;
    }
}


