using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.Models.FormEnums
{
    public enum FormStatus
    {
        [Description("Bekleniyor")]
        waiting = 1,
        [Description("Onay Sürecinde")]
        inApprove = 2,
        [Description("Tamamlandı")]
        completed = 3,
        [Description("Süresi Geçti")]
        expired = 4,

    }
}
