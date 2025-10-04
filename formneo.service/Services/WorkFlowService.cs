using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;
using formneo.repository;
using formneo.repository.Repositories;
using formneo.repository.UnitOfWorks;
using static System.Formats.Asn1.AsnWriter;

namespace formneo.service.Services
{
    public class WorkFlowService : Service<WorkflowHead>, IWorkFlowService
    {

        private readonly IWorkflowRepository _workFlowRepository;
        private readonly IWorkFlowItemRepository _workFlowItemRepository;
        private readonly IApproveItemsRepository _approveItemsRepository;


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkFlowService(IGenericRepository<WorkflowHead> repository, IUnitOfWork unitOfWork, IMapper mapper, IWorkflowRepository workFlowRepository, IWorkFlowItemRepository workFlowItemRepository, IApproveItemsRepository approveItemsRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;

           //var s=  workFlowItemRepository.GetAll();
 
            _workFlowRepository = workFlowRepository;
            _workFlowItemRepository = workFlowItemRepository;
            _approveItemsRepository = approveItemsRepository;

            _unitOfWork = unitOfWork;
        }

        public async Task<WorkflowHead> GetWorkFlowWitId(Guid guid)
        {

            var result = await _workFlowRepository.GetWorkFlowWitId(guid);
            return result;
        }

        public async Task<bool> UpdateWorkFlowAndRelations(WorkflowHead head, List<WorkflowItem> workflowItems, ApproveItems approveItem)
        {



            _unitOfWork.BeginTransaction();

            _workFlowRepository.Update(head);

            //_workFlowItemRepository.AddRangeAsync(workflowItems);

            _approveItemsRepository.Update(approveItem);



            await _unitOfWork.CommitAsync();


            return true;
        }


    }
}
 