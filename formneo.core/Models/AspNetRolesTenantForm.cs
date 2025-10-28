using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace formneo.core.Models
{
    [Index(nameof(FormId), nameof(FormTenantRoleId), IsUnique = true)] // Tenant kapsamı BaseEntity.MainClientId ile sağlanır
    public class AspNetRolesTenantForm : BaseEntity
    {
        [ForeignKey("Forms")]
        public Guid FormId { get; set; }
        public virtual Form Form { get; set; }

        [ForeignKey("FormTenantRoles")]
        public Guid FormTenantRoleId { get; set; }
        public virtual FormTenantRole FormTenantRole { get; set; }

        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

        public string Description { get; set; }
    }
}


