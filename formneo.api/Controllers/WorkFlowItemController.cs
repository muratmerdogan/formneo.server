using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NLayer.Core.Services;
using NLayer.Service.Services;
using System;
using formneo.core;
using formneo.core.DTOs;
using formneo.core.DTOs.Budget.JobCodeRequest;
using formneo.core.DTOs.Clients;
using formneo.core.Models;
using formneo.core.Operations;
using formneo.core.Services;
using formneo.service.Services;
using formneo.workflow;
using WorkflowCore.Models;
using WorkflowCore.Services;
namespace formneo.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkFlowItemController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IWorkFlowService _service;
        private readonly IWorkFlowItemService _workFlowItemservice;
        private readonly IApproveItemsService _approveItemsService;
        private readonly IServiceWithDto<WorkFlowDefination, WorkFlowDefinationDto> _workFlowDefinationDto;
        private readonly IServiceWithDto<WorkflowHead, WorkFlowHeadDto> _workFlowHeadService;
        private readonly IServiceWithDto<WorkflowItem, WorkFlowItemDto> _workFlowItemService;

        public WorkFlowItemController(IMapper mapper, IWorkFlowService workFlowService, IWorkFlowItemService workFlowItemservice, IApproveItemsService approveItemsService, IServiceWithDto<WorkFlowDefination, WorkFlowDefinationDto> definationdto, IServiceWithDto<WorkflowHead, WorkFlowHeadDto> workFlowHeadService, IServiceWithDto<WorkflowItem, WorkFlowItemDto> workflowItemService)
        {

            _mapper = mapper;
            _service = workFlowService;
            _workFlowDefinationDto = definationdto;
            _workFlowItemservice = workFlowItemservice;
            _approveItemsService = approveItemsService;
            _workFlowHeadService = workFlowHeadService;
            _workFlowItemService = workflowItemService;

        }


        [HttpGet("{workFlowHeadId}")]
        public async Task<List<WorkFlowItemDtoWithApproveItems>> GetApproveItems(Guid workFlowHeadId)
        {

           var heads = await _workFlowItemService.Include();

            var approveItems = heads.Where(e => e.WorkflowHeadId == workFlowHeadId).Include(e => e.approveItems);

            var items = _mapper.Map<List<WorkFlowItemDtoWithApproveItems>>(approveItems);
            return items.Where(e => e.NodeType == "approverNode").ToList();

        }


        [HttpGet("{workFlowHeadId}")]
        public async Task<int> getStatus(Guid workFlowHeadId)
        {
            var head =  await _workFlowHeadService.Find(e => e.Id == workFlowHeadId);

            return (int)head.Data.workFlowStatus;
        }

       
    }
}
