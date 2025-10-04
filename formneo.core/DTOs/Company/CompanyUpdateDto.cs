using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.Company
{
    public class CompanyUpdateDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ClientId { get; set; }

       
    }
}
