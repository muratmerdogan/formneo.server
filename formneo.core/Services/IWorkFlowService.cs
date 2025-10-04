using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs;
using formneo.core.Models;

namespace formneo.core.Services
{
    public interface IWorkFlowService : IService<WorkflowHead>
    {

        Task<WorkflowHead> GetWorkFlowWitId(Guid guid);

        Task<bool> UpdateWorkFlowAndRelations(WorkflowHead head, List<WorkflowItem> workflowItems, ApproveItems approveItems = null);
    }
}
