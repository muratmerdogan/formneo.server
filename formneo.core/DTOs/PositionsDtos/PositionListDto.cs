using vesa.core.DTOs.SapDtos;
using vesa.core.Models;

namespace vesa.core.DTOs.PositionsDtos
{
    public class PositionListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } = null;
        public Guid? CustomerRefId { get; set; } = null;
        public string? CustomerName { get; set; } = null;
    }
}
