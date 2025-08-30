using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Services;
using vesa.core.DTOs;
using vesa.core.Models;
using vesa.core.Services;

namespace vesa.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MailController : CustomBaseController
    {

        public MailController(IMapper mapper, IWorkFlowService workFlowService, IWorkFlowItemService workFlowItemservice, IApproveItemsService approveItemsService, IServiceWithDto<WorkFlowDefination, WorkFlowDefinationDto> definationdto, IServiceWithDto<WorkflowHead, WorkFlowHeadDto> workFlowHeadService, ITicketServices ticketServices)
        {


        }

    }
}
