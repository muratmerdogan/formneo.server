using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.Repositories
{
    public  interface IWorkflowRepository : IGenericRepository<WorkflowHead>
    {
         Task<WorkflowHead> GetWorkFlowWitId(Guid persId);

    }
}
