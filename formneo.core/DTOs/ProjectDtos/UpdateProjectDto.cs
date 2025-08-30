using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.ProjectDtos
{
    public class UpdateProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Photo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProjectGain { get; set; }
        public string ProjectLearn { get; set; }
        public string ProjectTags { get; set; }
    }
}
