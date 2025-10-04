using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.Configuration
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GmtPlus3Attribute : Attribute
    {
        public DateTime GetGmtPlus3Time()
        {
            return DateTime.UtcNow.AddHours(3);
        }
    }
        
}
