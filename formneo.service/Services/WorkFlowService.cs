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
        private readonly IFormItemsRepository _formItemsRepository;


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkFlowService(IGenericRepository<WorkflowHead> repository, IUnitOfWork unitOfWork, IMapper mapper, IWorkflowRepository workFlowRepository, IWorkFlowItemRepository workFlowItemRepository, IApproveItemsRepository approveItemsRepository, IFormItemsRepository formItemsRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;

           //var s=  workFlowItemRepository.GetAll();
 
            _workFlowRepository = workFlowRepository;
            _workFlowItemRepository = workFlowItemRepository;
            _approveItemsRepository = approveItemsRepository;
            _formItemsRepository = formItemsRepository;

            _unitOfWork = unitOfWork;
        }

        public async Task<WorkflowHead> GetWorkFlowWitId(Guid guid)
        {

            var result = await _workFlowRepository.GetWorkFlowWitId(guid);
            return result;
        }

        public async Task<bool> UpdateWorkFlowAndRelations(WorkflowHead head, List<WorkflowItem> workflowItems, ApproveItems approveItem = null, FormItems formItem = null)
        {



            _unitOfWork.BeginTransaction();

            _workFlowRepository.Update(head);

            //_workFlowItemRepository.AddRangeAsync(workflowItems);

            if (approveItem != null)
            {
                _approveItemsRepository.Update(approveItem);
            }

            if (formItem != null)
            {
                if (formItem.Id == Guid.Empty)
                {
                    await _formItemsRepository.AddAsync(formItem);
                }
                else
                {
                    _formItemsRepository.Update(formItem);
                }
            }

            // WorkflowItems içindeki FormItems'ları kaydet
            foreach (var item in workflowItems)
            {
                if (item.formItems != null && item.formItems.Count > 0)
                {
                    foreach (var fi in item.formItems)
                    {
                        if (fi.Id == Guid.Empty)
                        {
                            await _formItemsRepository.AddAsync(fi);
                        }
                        else
                        {
                            _formItemsRepository.Update(fi);
                        }
                    }
                }
            }

            await _unitOfWork.CommitAsync();


            return true;
        }


    }
}
 