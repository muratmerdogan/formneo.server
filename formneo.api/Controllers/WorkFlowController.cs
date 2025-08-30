using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Services;
using NLayer.Service.Services;
using vesa.core.DTOs;
using vesa.core.DTOs.Budget.JobCodeRequest;
using vesa.core.Models;
using vesa.core.Operations;
using vesa.core.Services;
using vesa.service.Services;
using vesa.workflow;
using WorkflowCore.Models;
using WorkflowCore.Services;
namespace vesa.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkFlowController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IWorkFlowService _service;
        private readonly IWorkFlowItemService _workFlowItemservice;
        private readonly IApproveItemsService _approveItemsService;
        private readonly IServiceWithDto<WorkFlowDefination, WorkFlowDefinationDto> _workFlowDefinationDto;
        private readonly IServiceWithDto<WorkflowHead, WorkFlowHeadDto> _workFlowHeadService;
        private readonly ITicketServices _ticketService;
        public WorkFlowController(IMapper mapper, IWorkFlowService workFlowService, IWorkFlowItemService workFlowItemservice, IApproveItemsService approveItemsService, IServiceWithDto<WorkFlowDefination, WorkFlowDefinationDto> definationdto, IServiceWithDto<WorkflowHead, WorkFlowHeadDto> workFlowHeadService, ITicketServices ticketServices)
        {

            _mapper = mapper;
            _service = workFlowService;
            _workFlowDefinationDto = definationdto;
            _workFlowItemservice = workFlowItemservice;
            _approveItemsService = approveItemsService;
            _workFlowHeadService = workFlowHeadService;
            _ticketService = ticketServices;

        }
        [HttpPost]
        public async Task<ActionResult<WorkFlowHeadDtoResultStartOrContinue>> Contiune(WorkFlowContiuneApiDto workFlowApiDto)
        {

            WorkFlowExecute execute = new WorkFlowExecute();
            WorkFlowDto workFlowDto = new WorkFlowDto();
            WorkFlowParameters parameters = new WorkFlowParameters();
            parameters.workFlowService = _service;
            parameters.workFlowItemService = _workFlowItemservice;
            parameters._workFlowDefination = _workFlowDefinationDto;
            parameters._approverItemsService = _approveItemsService;
            parameters._ticketService = _ticketService;


            workFlowApiDto.UserName = User.Identity.Name;

            var workFlowItem = await _workFlowItemservice.GetByIdStringGuidAsync(new Guid(workFlowApiDto.workFlowItemId));
            var workFlowHead = await _workFlowHeadService.GetByIdGuidAsync(new Guid(workFlowItem.WorkflowHeadId.ToString()));


            workFlowDto.WorkFlowDefinationId = workFlowHead.Data.WorkFlowDefinationId;
            workFlowDto.NodeId = workFlowItem.Id.ToString();
            workFlowDto.WorkFlowId = workFlowItem.WorkflowHeadId.ToString(); ;
            workFlowDto.ApproverItemId = workFlowApiDto.ApproveItem;
            workFlowDto.Input = workFlowApiDto.Input;
            workFlowDto.UserName = workFlowApiDto.UserName;
            workFlowDto.Note = workFlowApiDto.Note;
            workFlowDto.NumberManDay = workFlowApiDto.NumberManDay;

            var result = await execute.StartAsync(workFlowDto, parameters, null);
            var mapResult = _mapper.Map<WorkFlowHeadDtoResultStartOrContinue>(result);

            return mapResult;
        }

        [HttpPost]
        public async Task<ActionResult<WorkFlowHeadDtoResultStartOrContinue>> Start(WorkFlowStartApiDto workFlowApiDto)
        {
            WorkFlowExecute execute = new WorkFlowExecute();
            WorkFlowDto workFlowDto = new WorkFlowDto();

            WorkFlowParameters parameters = new WorkFlowParameters();
            parameters.workFlowService = _service;
            parameters.workFlowItemService = _workFlowItemservice;
            parameters._workFlowDefination = _workFlowDefinationDto;


            workFlowDto.WorkFlowDefinationId = new Guid(workFlowApiDto.DefinationId);
            workFlowDto.UserName = workFlowApiDto.UserName;

            workFlowDto.WorkFlowInfo = workFlowApiDto.WorkFlowInfo;


            var result = await execute.StartAsync(workFlowDto, parameters, null);
            var mapResult = _mapper.Map<WorkFlowHeadDtoResultStartOrContinue>(result);

            return mapResult;
        }

        [HttpPost]
        public async Task<ActionResult<WorkFlowHeadDtoResultStartOrContinue>> StartAndTicket(WorkFlowStartApiDto workFlowApiDto)
        {
            WorkFlowExecute execute = new WorkFlowExecute();
            WorkFlowDto workFlowDto = new WorkFlowDto();

            WorkFlowParameters parameters = new WorkFlowParameters();
            parameters.workFlowService = _service;
            parameters.workFlowItemService = _workFlowItemservice;
            parameters._workFlowDefination = _workFlowDefinationDto;
            workFlowApiDto.UserName = User.Identity.Name;


            workFlowDto.WorkFlowDefinationId = new Guid(workFlowApiDto.DefinationId);
            workFlowDto.UserName = workFlowApiDto.UserName;

            workFlowDto.WorkFlowInfo = workFlowApiDto.WorkFlowInfo;


            var result = await execute.StartAsync(workFlowDto, parameters, null);
            var mapResult = _mapper.Map<WorkFlowHeadDtoResultStartOrContinue>(result);

            return mapResult;



            //    var workFlowApi = new WorkFlowApi(configuration);
            //    let startDto: WorkFlowStartApiDto = { };
            //    startDto.definationId = "b08b506c-1d98-4feb-a680-64ddc67b3c39";
            //    startDto.userName = username;
            //    startDto.workFlowInfo = "Terfi Edecek Kişi:" + `${ usernameTerfi} (${ IcKaynakname})`;
            //    var result = await workFlowApi.apiWorkFlowStartPost(startDto)
            //        .then(response => {
            //        onSubmit(response.data.id!, true);
            //        dispatchAlert({ message: "Kayıt Güncelleme Başarılı", type: MessageBoxTypes.Success })
            //           dispatchBusy({ isBusy: false });

            //})



        }

        private void Validations()
        {


        }

    }
}
