using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace vesa.core.Models
{
    [Index(nameof(MenuId), nameof(RoleId), IsUnique = true)]
    public class AspNetRolesMenu : GlobalBaseEntity
    {
        [ForeignKey("Menus")]
        public Guid MenuId { get; set; }
        public virtual Menu Menu { get; set; }

        [ForeignKey("AspNetRoles")]
        public string RoleId { get; set; }
        public virtual IdentityRole Role { get; set; }

        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

        public string Description { get; set; }

    }
}
