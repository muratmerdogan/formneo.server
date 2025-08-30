using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs;
using vesa.core.Models;

namespace vesa.core.Services
{
    public interface IWorkFlowService : IService<WorkflowHead>
    {

        Task<WorkflowHead> GetWorkFlowWitId(Guid guid);

        Task<bool> UpdateWorkFlowAndRelations(WorkflowHead head, List<WorkflowItem> workflowItems, ApproveItems approveItems = null);
    }
}
