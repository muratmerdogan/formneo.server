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
using vesa.repository.UnitOfWorks;

namespace vesa.service.Services
{
    public class ApproveItemsService : Service<ApproveItems>, IApproveItemsService
    {
        private readonly IApproveItemsRepository _approveItemsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApproveItemsService(IGenericRepository<ApproveItems> repository, IUnitOfWork unitOfWork, IMapper mapper, IApproveItemsRepository approveItemsRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _approveItemsRepository = approveItemsRepository;

            _unitOfWork = unitOfWork;

        }

        public async Task<List<ApproveItemsDto>> GetAllRelationTable()
        {
            var list = _approveItemsRepository.GetAll(); ;

            var ss = _approveItemsRepository.GetAll().Include(e => e.WorkflowItem).ThenInclude(e => e.WorkflowHead).ToList();

            var dto = _mapper.Map<List<ApproveItemsDto>>(ss.OrderByDescending(e => e.CreatedDate));

            return dto;
        }

    }
}
