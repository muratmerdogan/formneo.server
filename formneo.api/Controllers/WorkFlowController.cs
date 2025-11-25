using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using NLayer.Service.Services;
using System.Linq;
using formneo.core;
using formneo.core.DTOs;
using formneo.core.DTOs.Budget.JobCodeRequest;
using formneo.core.Models;
using formneo.core.Operations;
using formneo.core.Services;
using formneo.service.Services;
using formneo.workflow;
using formneo.workflow.Services;
using WorkflowCore.Models;
using WorkflowCore.Services;
namespace formneo.api.Controllers
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
        private readonly IServiceWithDto<WorkflowItem, WorkFlowItemDto> _workFlowItemService;
        private readonly ITicketServices _ticketService;
        private readonly WorkflowResponseBuilder _responseBuilder;
        
        public WorkFlowController(
            IMapper mapper, 
            IWorkFlowService workFlowService, 
            IWorkFlowItemService workFlowItemservice, 
            IApproveItemsService approveItemsService, 
            IServiceWithDto<WorkFlowDefination, WorkFlowDefinationDto> definationdto, 
            IServiceWithDto<WorkflowHead, WorkFlowHeadDto> workFlowHeadService, 
            IServiceWithDto<WorkflowItem, WorkFlowItemDto> workFlowItemService, 
            ITicketServices ticketServices)
        {
            _mapper = mapper;
            _service = workFlowService;
            _workFlowDefinationDto = definationdto;
            _workFlowItemservice = workFlowItemservice;
            _approveItemsService = approveItemsService;
            _workFlowHeadService = workFlowHeadService;
            _workFlowItemService = workFlowItemService;
            _ticketService = ticketServices;
            _responseBuilder = new WorkflowResponseBuilder(mapper);
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
            
            // Generic response builder kullan
            var mapResult = _responseBuilder.BuildResponse(result);

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
            workFlowDto.UserName = workFlowApiDto.UserName ?? User.Identity.Name;
            workFlowDto.WorkFlowInfo = workFlowApiDto.WorkFlowInfo;
            workFlowDto.Action = workFlowApiDto.Action;

            // FormData'yı payloadJson olarak gönder
            var payloadJson = workFlowApiDto.FormData;

            var result = await execute.StartAsync(workFlowDto, parameters, payloadJson);
            
            // Generic response builder kullan
            var mapResult = _responseBuilder.BuildResponse(result);

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
            
            // Generic response builder kullan
            var mapResult = _responseBuilder.BuildResponse(result);

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

        /// <summary>
        /// Workflow detail bilgilerini getirir (nodes, edges, approve items dahil)
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkFlowHeadDetailDto>> GetDetail(Guid id)
        {
            var workFlowHead = await _workFlowHeadService.GetByIdGuidAsync(id);
            if (workFlowHead.Data == null)
            {
                return NotFound();
            }

            // Workflow items ve approve items'ları dahil et (WorkFlowItemController'daki gibi)
            var workflowItemsQuery = await _workFlowItemService.Include();
            var itemsWithApproves = workflowItemsQuery
                .Where(e => e.WorkflowHeadId == id)
                .Include(e => e.approveItems)
                .ToList();
            
            // Workflow definition'dan nodes ve edges bilgilerini al
            var workFlowDefination = await _workFlowDefinationDto.GetByIdGuidAsync(workFlowHead.Data.WorkFlowDefinationId);
            
            var detailDto = new WorkFlowHeadDetailDto
            {
                Id = id.ToString(), // Id'yi parametreden al
                WorkflowName = workFlowHead.Data.WorkflowName,
                WorkFlowInfo = workFlowHead.Data.WorkFlowInfo,
                WorkFlowStatus = workFlowHead.Data.workFlowStatus,
                CreateUser = workFlowHead.Data.CreateUser,
                UniqNumber = workFlowHead.Data.UniqNumber,
                WorkFlowDefinationId = workFlowHead.Data.WorkFlowDefinationId,
                WorkflowItems = _mapper.Map<List<WorkFlowItemDtoWithApproveItems>>(itemsWithApproves),
                WorkFlowDefinationJson = workFlowDefination?.Data?.Defination
            };

            return detailDto;
        }

        private void Validations()
        {


        }

    }
}
