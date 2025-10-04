using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.Company
{
    public class CompanyListDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ClientName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
