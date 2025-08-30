using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.Models.FormEnums
{
    public enum FormCategory
    {
        [Description("İnsan Kaynakları")]
        HR = 1,
        [Description("Finans")]
        Finance = 2,
        [Description("Operasyon")]
        Operation = 3,
        [Description("Bilgi Teknolojileri")]
        IT = 4,

    }
}
