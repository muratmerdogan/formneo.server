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
    public class WorkFlowDefinationService : Service<WorkFlowDefination>, IWorkFlowDefinationService
    {

        private readonly IWorkFlowDefinationRepository _workFlowDefinationRepository;


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkFlowDefinationService(IGenericRepository<WorkFlowDefination> repository, IUnitOfWork unitOfWork, IMapper mapper, IWorkFlowDefinationRepository workFlowRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;

            //var s=  workFlowItemRepository.GetAll();

            _workFlowDefinationRepository = workFlowRepository;


            _unitOfWork = unitOfWork;
        }


    }
}
 