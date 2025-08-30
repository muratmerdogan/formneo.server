using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;

namespace vesa.core.DTOs.Company
{
    public class CompanyInsertDto
    {
        public string Name { get; set; }

        public Guid ClientId { get; set; }



 
    }
}
