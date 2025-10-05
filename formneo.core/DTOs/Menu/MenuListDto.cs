
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace formneo.core.DTOs.Menu
{
    public class MenuListDto
    {
        public Guid Id { get; set; }

        public string MenuCode { get; set; }

        public Guid? ParentMenuId { get; set; }

        public ICollection<MenuListDto>? SubMenus { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string? Route { get; set; }

        [StringLength(255)]
        public string? Href { get; set; }

        [StringLength(255)]
        public string? Icon { get; set; }

        public bool IsActive { get; set; } = true;

        public int Order { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string Description { get; set; }

        public bool ShowMenu { get; set; }

        public bool IsTenantOnly { get; set; }

        public bool IsGlobalOnly { get; set; }


    }
}