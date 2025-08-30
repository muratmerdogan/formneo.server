using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs.PositionsDtos;
using vesa.core.Models;

namespace vesa.core.Services
{
    public interface IPositionService:IService<Positions>
    {
        Task<IEnumerable<PositionListDto>> GetPositionsListAsync();
        Task<PositionListDto> GetByIdPositionAsync(Guid id);
    }
}
