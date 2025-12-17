using NLayer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs;
using formneo.core.Models;
using formneo.core.Services;

namespace formneo.core.Operations
{
    public  class WorkFlowParameters
    {
        public  IWorkFlowService  workFlowService{ get; set; }
        public  IWorkFlowItemService workFlowItemService { get; set; }

        public ITicketServices _ticketService { get; set; }
        public IApproveItemsService _approverItemsService { get; set; }
        public IFormItemsService _formItemsService { get; set; }
        public IFormInstanceService _formInstanceService { get; set; }
        public IFormService _formService { get; set; }
        public  IServiceWithDto<WorkFlowDefination, WorkFlowDefinationDto>  _workFlowDefination { get; set; }

    }
}
