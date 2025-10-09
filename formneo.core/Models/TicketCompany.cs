using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.Models
{
    public class Customer222 : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string MailExtension { get; set; } // Mail uzantısı örnek: @formneo.com


        public virtual ICollection<UserApp> Users { get; set; }
    }
}
