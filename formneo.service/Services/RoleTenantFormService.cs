using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using formneo.core.DTOs.RoleForm;
using formneo.core.Models;
using formneo.core.Models.FormEnums;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;
using NLayer.Service.Services;

namespace formneo.service.Services
{
    public class RoleTenantFormService : ServiceWithDto<AspNetRolesTenantForm, RoleTenantFormListDto>, IRoleTenantFormService
    {
        private readonly IGenericRepository<AspNetRolesTenantForm> _repo;
        private readonly IGenericRepository<FormTenantRole> _roleRepo;
        private readonly IGenericRepository<Form> _formRepo;

        public RoleTenantFormService(
            IGenericRepository<AspNetRolesTenantForm> repo,
            IGenericRepository<FormTenantRole> roleRepo,
            IGenericRepository<Form> formRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper
        ) : base(repo, unitOfWork, mapper)
        {
            _repo = repo;
            _roleRepo = roleRepo;
            _formRepo = formRepo;
        }

        public async Task<IEnumerable<RoleTenantFormListDto>> GetAllAsync()
        {
            var all = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleTenantFormListDto>>(all);
        }

        public async Task<RoleTenantFormDetailDto> GetDetailByFormTenantRoleAsync(Guid formTenantRoleId)
        {
            var role = await _roleRepo.GetByIdStringGuidAsync(formTenantRoleId);
            if (role == null) return null;

            var assignments = await _repo.Where(x => x.FormTenantRoleId == formTenantRoleId).ToListAsync();

            // Default mapping if there is no form assignment
            if (assignments == null || assignments.Count == 0)
            {
                return new RoleTenantFormDetailDto
                {
                    FormTenantRoleId = role.Id,
                    RoleName = role.Name,
                    RoleDescription = role.Description,
                    IsActive = role.IsActive,
                    Forms = new List<RoleTenantFormPermissionDto>()
                };
            }

            // Fetch assigned form entities and compute family roots
            var assignedFormIds = assignments.Select(a => a.FormId).Distinct().ToList();
            var assignedForms = await _formRepo.Where(f => assignedFormIds.Contains(f.Id)).ToListAsync();

            // If we couldn't load the form entities for some reason, fall back to original IDs
            if (assignedForms == null || assignedForms.Count == 0)
            {
                return new RoleTenantFormDetailDto
                {
                    FormTenantRoleId = role.Id,
                    RoleName = role.Name,
                    RoleDescription = role.Description,
                    IsActive = role.IsActive,
                    Forms = assignments.Select(x => new RoleTenantFormPermissionDto
                    {
                        FormId = x.FormId,
                        FormName = null,
                        CanView = x.CanView,
                        CanAdd = x.CanAdd,
                        CanEdit = x.CanEdit,
                        CanDelete = x.CanDelete
                    }).ToList()
                };
            }

            var familyRootIds = assignedForms
                .Select(f => f.ParentFormId ?? f.Id)
                .Distinct()
                .ToList();

            // Load all forms in those families to resolve latest revisions
            var familyForms = await _formRepo
                .Where(f => familyRootIds.Contains(f.Id) || (f.ParentFormId.HasValue && familyRootIds.Contains(f.ParentFormId.Value)))
                .ToListAsync();

            var latestByRoot = familyForms
                .GroupBy(f => f.ParentFormId ?? f.Id)
                .ToDictionary(
                    g => g.Key,
                    g => g.Where(f => f.PublicationStatus == FormPublicationStatus.Published)
                          .OrderByDescending(f => f.Revision)
                          .FirstOrDefault() ?? g.OrderByDescending(f => f.Revision).First()
                );

            var assignedFormsById = assignedForms.ToDictionary(f => f.Id, f => f);
            var formsWithLatest = assignments.Select(x =>
            {
                var assigned = assignedFormsById.TryGetValue(x.FormId, out var af) ? af : null;
                var resolvedId = x.FormId;
                string? resolvedName = assigned?.FormName;
                if (assigned != null)
                {
                    var rootId = assigned.ParentFormId ?? assigned.Id;
                    if (latestByRoot.TryGetValue(rootId, out var latest))
                    {
                        resolvedId = latest.Id;
                        resolvedName = latest.FormName;
                    }
                }

                return new RoleTenantFormPermissionDto
                {
                    FormId = resolvedId,
                    FormName = resolvedName,
                    CanView = x.CanView,
                    CanAdd = x.CanAdd,
                    CanEdit = x.CanEdit,
                    CanDelete = x.CanDelete
                };
            }).ToList();

            return new RoleTenantFormDetailDto
            {
                FormTenantRoleId = role.Id,
                RoleName = role.Name,
                RoleDescription = role.Description,
                IsActive = role.IsActive,
                Forms = formsWithLatest
            };
        }

        public async Task<Guid> InsertAsync(RoleTenantFormInsertDto dto)
        {
            var roleEntity = new FormTenantRole
            {
                Id = Guid.NewGuid(),
                Name = dto.RoleName,
                Description = dto.RoleDescription,
                IsActive = dto.RoleIsActive ?? true
            };
            await _roleRepo.AddAsync(roleEntity);

            // İlişkileri yeniden kaydet
            var existing = await _repo.Where(x => x.FormTenantRoleId == roleEntity.Id).ToListAsync();
            if (existing.Any())
            {
                _repo.RemoveRange(existing);
            }

            var toInsert = dto.FormPermissions.Select(m => new AspNetRolesTenantForm
            {
                Id = Guid.NewGuid(),
                FormTenantRoleId = roleEntity.Id,
                FormId = m.FormId,
                CanView = m.CanView,
                CanAdd = m.CanAdd,
                CanEdit = m.CanEdit,
                CanDelete = m.CanDelete,
                Description = string.Empty,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedBy = string.Empty
            }).ToList();
            if (toInsert.Any())
                await _repo.AddRangeAsync(toInsert);

            await _unitOfWork.CommitAsync();
            return roleEntity.Id;
        }

        public async Task<Guid> UpdateAsync(RoleTenantFormUpdateDto dto)
        {
            var roleEntity = await _roleRepo.GetByIdStringGuidAsync(dto.FormTenantRoleId);
            if (roleEntity == null) throw new Exception("Role not found");

            if (!string.IsNullOrWhiteSpace(dto.RoleName)) roleEntity.Name = dto.RoleName;
            if (dto.RoleDescription != null) roleEntity.Description = dto.RoleDescription;
            if (dto.RoleIsActive.HasValue) roleEntity.IsActive = dto.RoleIsActive.Value;
            _roleRepo.Update(roleEntity);

            var existing = await _repo.Where(x => x.FormTenantRoleId == roleEntity.Id).ToListAsync();
            if (existing.Any())
            {
                _repo.RemoveRange(existing);
            }
            var toInsert = dto.FormPermissions.Select(m => new AspNetRolesTenantForm
            {
                Id = Guid.NewGuid(),
                FormTenantRoleId = roleEntity.Id,
                FormId = m.FormId,
                CanView = m.CanView,
                CanAdd = m.CanAdd,
                CanEdit = m.CanEdit,
                CanDelete = m.CanDelete,
                Description = string.Empty,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedBy = string.Empty
            }).ToList();
            if (toInsert.Any())
                await _repo.AddRangeAsync(toInsert);

            await _unitOfWork.CommitAsync();
            return roleEntity.Id;
        }
    }
}


