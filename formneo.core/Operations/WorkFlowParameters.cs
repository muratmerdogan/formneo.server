using NLayer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs;
using vesa.core.Models;
using vesa.core.Services;

namespace vesa.core.Operations
{
    public  class WorkFlowParameters
    {
        public  IWorkFlowService  workFlowService{ get; set; }
        public  IWorkFlowItemService workFlowItemService { get; set; }

        public ITicketServices _ticketService { get; set; }
        public IApproveItemsService _approverItemsService { get; set; }
        public  IServiceWithDto<WorkFlowDefination, WorkFlowDefinationDto>  _workFlowDefination { get; set; }

    }
}
