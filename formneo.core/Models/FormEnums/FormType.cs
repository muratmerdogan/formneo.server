using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.Models.FormEnums
{
    public enum FormType
    {
        [Description("Standart Form")]
        StandartForm = 1,
        [Description("İş Akış Formu")]
        WorkFlowForm = 2,
        [Description("Onay Formu")]
        ApprovalForm = 3,

    }
}
