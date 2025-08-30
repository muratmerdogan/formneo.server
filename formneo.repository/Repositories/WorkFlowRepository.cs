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
    public class WorkFlowRepository : GenericRepository<WorkflowHead>, IWorkflowRepository
    {
        public WorkFlowRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<WorkflowHead> GetWorkFlowWitId(Guid id)
        {
            var workflowHead = await _context.WorkflowHead.Include(e => e.workflowItems).FirstOrDefaultAsync(x => x.Id == id);
            return workflowHead;
        }
    }
}
