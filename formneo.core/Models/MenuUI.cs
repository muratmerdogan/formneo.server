using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.Models
{
    public class MenuUI : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string Link { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("MenuId")]
        public virtual MenuUI Parent { get; set; }

        public virtual ICollection<MenuUI> Children { get; set; } = new List<MenuUI>();
    }
}
