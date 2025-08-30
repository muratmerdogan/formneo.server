using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;

namespace vesa.core.Repositories
{
    public  interface IWorkflowRepository : IGenericRepository<WorkflowHead>
    {
         Task<WorkflowHead> GetWorkFlowWitId(Guid persId);

    }
}
