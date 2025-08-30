using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Configuration;

namespace vesa.core.Models
{

    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? MainClientId { get; set; }
        [ForeignKey("MainClientId")]
        [Key]
        public MainClient MainClient { get; set; }

        public Guid? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [Key]
        public Company Company { get; set; }

        public Guid? PlantId { get; set; }
        [ForeignKey("PlantId")]
        [Key]
        public Plant Plant { get; set; }


        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public BaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public bool IsDelete { get; set; }



        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Otomatik 
        public int UniqNumber { get; set; }
    }


    public abstract class GlobalBaseEntity
    {
        [Key]
        public Guid Id { get; set; }


        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public GlobalBaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public bool IsDelete { get; set; }



        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Otomatik 
        public int UniqNumber { get; set; }
    }
}
