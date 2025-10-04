using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.Plants
{
    public class PlantListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
