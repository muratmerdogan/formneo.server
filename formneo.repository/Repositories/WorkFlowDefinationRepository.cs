using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;
using vesa.core.Repositories;

namespace vesa.repository.Repositories
{
    public class WorkFlowDefinationRepository : GenericRepository<WorkFlowDefination>, IWorkFlowDefinationRepository
    {
        public WorkFlowDefinationRepository(AppDbContext context) : base(context)
        {
        }


    }
}
