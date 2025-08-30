using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs
{
    public abstract class BaseDto
    {

  
        
        public DateTime CreatedDate { get; set; }
    }
}
