using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs;
using vesa.core.Models;

namespace vesa.core.Services
{

    public interface IApproveItemsService : IService<ApproveItems>
    {

        Task<List<ApproveItemsDto>> GetAllRelationTable();

    }

}
