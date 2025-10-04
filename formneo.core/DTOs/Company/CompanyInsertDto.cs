using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.DTOs.Company
{
    public class CompanyInsertDto
    {
        public string Name { get; set; }

        public Guid ClientId { get; set; }



 
    }
}
