using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [ForeignKey("ClientId")]
        public MainClient Client { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public Company()
        {
            CreatedDate = DateTime.UtcNow;
        }

        // Navigation property for related plants
        public ICollection<Plant> Plant { get; set; }
    }
}
