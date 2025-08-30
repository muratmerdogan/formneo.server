using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.Models
{
    public enum Category
    {
        [Description("SAP FIORI")]
        SapFiori,

        [Description("SAP BTP")]
        SapBtp,

        [Description("SAP SUCCESSFACTORS")]
        SapSuccessFactors,

        [Description("SAP CORE HR")]
        SapCoreHr

    }
    public class Project :BaseEntity
    {
        [ForeignKey("UserApp")]
        public string UserId { get; set; }
        public UserApp UserApp { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Photo { get; set; }
        public DateTime StartDate  { get; set; }
        public DateTime EndDate { get; set; }
        public string ProjectGain { get; set; }
        public string ProjectLearn { get; set; }
        public string ProjectTags { get; set; }
    }
}
