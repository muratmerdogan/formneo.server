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
 