using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs;
using vesa.core.Models;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;
using vesa.repository;
using vesa.repository.Repositories;
using vesa.repository.UnitOfWorks;
using static System.Formats.Asn1.AsnWriter;

namespace vesa.service.Services
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
 