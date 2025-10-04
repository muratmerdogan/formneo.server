using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
using formneo.repository.Repositories;

namespace formneo.service.Services
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
 