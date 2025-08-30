using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
using vesa.repository.Repositories;

namespace vesa.service.Services
{
    public class WorkFlowItemService : Service<WorkflowItem>, IWorkFlowItemService
    {

        private readonly IWorkFlowItemRepository _workFlowItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkFlowItemService(IGenericRepository<WorkflowItem> repository, IUnitOfWork unitOfWork, IMapper mapper, IWorkFlowItemRepository workFlowItemRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;

            _workFlowItemRepository = workFlowItemRepository;

            _unitOfWork = unitOfWork;
        }

    }
}
 