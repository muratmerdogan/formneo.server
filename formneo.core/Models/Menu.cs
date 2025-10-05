using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.Models
{
    public class Menu : GlobalBaseEntity
    {

        public string MenuCode { get; set; }

        public Guid? ParentMenuId { get; set; }

        [ForeignKey("ParentMenuId")]
        public Menu? ParentMenu { get; set; }
        public ICollection<Menu>? SubMenus { get; set; }

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

        public string Description { get; set; }

        public bool ShowMenu { get; set; }

        // Global admin modunda gösterilmemesi gereken tenant-bazlı menüler için işaret
        public bool IsTenantOnly { get; set; }

        // Tenant modunda gösterilmemesi gereken, sadece global modda görünen menüler için işaret
        public bool IsGlobalOnly { get; set; }

    }

    public class MenuInsertDto
    {

        public string MenuCode { get; set; }

        public Guid? ParentMenuId { get; set; }


        public string Name { get; set; }

        public string? Route { get; set; }

        public string? Href { get; set; }

        public string? Icon { get; set; }

        public bool IsActive { get; set; } = true;

        public int Order { get; set; }

        public string? Description { get; set; }

        public bool ShowMenu { get; set; }

        public bool IsTenantOnly { get; set; }

        public bool IsGlobalOnly { get; set; }
    }

    public class MenuUpdateDto
    {


        public Guid Id { get; set; }

        public string MenuCode { get; set; }

        public Guid? ParentMenuId { get; set; }


        public string Name { get; set; }

        public string? Route { get; set; }

        public string? Href { get; set; }

        public string? Icon { get; set; }

        public bool IsActive { get; set; } = true;

        public int Order { get; set; }

        public string? Description { get; set; }

        public bool ShowMenu { get; set; }

        public bool IsTenantOnly { get; set; }

        public bool IsGlobalOnly { get; set; }
    }
}
