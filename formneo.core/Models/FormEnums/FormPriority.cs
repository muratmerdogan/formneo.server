using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.Models.FormEnums
{
    public enum FormPriority
    {
        [Description("Düşük")]
        Low = 1,
        [Description("Orta")]
        Medium = 2,
        [Description("Yüksek")]
        High = 3,

    }
}
