using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using formneo.core.DTOs.RoleForm;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;

namespace formneo.service.Services
{
    public class UserTenantFormRoleService : IUserTenantFormRoleService
    {
        private readonly IUserTenantFormRoleRepository _repository;
        private readonly IGenericRepository<FormTenantRole> _formTenantRoleRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserTenantFormRoleService(IUserTenantFormRoleRepository repository, IGenericRepository<FormTenantRole> formTenantRoleRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _formTenantRoleRepo = formTenantRoleRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserTenantFormRoleListDto>> GetByUserAsync(string userId)
        {
            var list = await _repository.GetByUserAsync(userId);
            return list.Select(x => new UserTenantFormRoleListDto
            {
                Id = x.Id,
                UserId = x.UserId,
                FormTenantRoleId = x.FormTenantRoleId,
                FormTenantRoleName = x.FormTenantRole?.Name,
                IsActive = x.IsActive
            }).ToList();
        }

        public async Task BulkSaveAsync(UserTenantFormRoleBulkSaveDto dto)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var existing = await _repository.Where(x => x.UserId == dto.UserId).ToListAsync();
                if (existing.Any())
                {
                    _repository.RemoveRange(existing);
                }

                var formRoleIds = dto.FormTenantRoleIds?.Distinct().ToList() ?? new List<Guid>();
                if (formRoleIds.Any())
                {
                    var validFormRoles = await _formTenantRoleRepo.Where(fr => formRoleIds.Contains(fr.Id) && fr.IsActive).Select(fr => fr.Id).ToListAsync();
                    var toAdd = validFormRoles.Select(id => new UserTenantFormRole
                    {
                        Id = Guid.NewGuid(),
                        UserId = dto.UserId,
                        FormTenantRoleId = id,
                        IsActive = true,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = "system",
                        UpdatedBy = string.Empty
                    }).ToList();
                    if (toAdd.Any())
                    {
                        await _repository.AddRangeAsync(toAdd);
                    }
                }

                await _unitOfWork.CommitAsync();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}


