using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.Models
{
    public class FormRuntime  : BaseEntity
    {

        [ForeignKey("Form")]
        public Guid FormId { get; set; }

        public virtual Form Form { get; set; }

        public string ValuesJson { get; set; }
        public string ValuesJsonData { get; set; }


        public bool İsActive { get; set; }

    }
}
