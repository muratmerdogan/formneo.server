using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace formneo.core.Models
{
    public class UserTenantFormRole : BaseEntity
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual UserApp User { get; set; }

        [ForeignKey(nameof(FormTenantRole))]
        public Guid FormTenantRoleId { get; set; }
        public virtual FormTenantRole FormTenantRole { get; set; }

        public bool IsActive { get; set; } = true;
    }
}


