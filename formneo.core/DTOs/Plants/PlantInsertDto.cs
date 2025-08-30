using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.Plants
{
    public class PlantInsertDto
    {
        public string Name { get; set; }
        public Guid CompanyId { get; set; }

    }
}
