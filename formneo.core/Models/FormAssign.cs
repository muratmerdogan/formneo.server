using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models.FormEnums;
using formneo.core.Models.Ticket;

namespace formneo.core.Models
{
    public class FormAssign : BaseEntity
    {
        [ForeignKey("Form")]
        public Guid FormId { get; set; }

        public virtual Form Form { get; set; }

        [ForeignKey("UserApp")]
        public string UserAppId { get; set; }
        public virtual UserApp UserApp { get; set; }

        [EnumDataType(typeof(FormStatus))]
        public FormStatus Status { get; set; }

        public Guid? FormRunTimeId { get; set; }

    }
}
