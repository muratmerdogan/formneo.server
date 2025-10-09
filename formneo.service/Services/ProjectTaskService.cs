using AutoMapper;
using Microsoft.EntityFrameworkCore;
using formneo.core.DTOs.ProjectTask;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;
using formneo.core.Services;

namespace formneo.service.Services
{
	public class ProjectTaskService : Service<ProjectTask>, IProjectTaskService
	{
		private readonly IProjectTaskRepository _repository;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ITenantProjectRepository _tenantProjectRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly ITenantContext _tenantContext;

		public ProjectTaskService(IGenericRepository<ProjectTask> repository, IUnitOfWork unitOfWork, IMapper mapper, IProjectTaskRepository projectTaskRepository, ITenantProjectRepository tenantProjectRepository, ICustomerRepository customerRepository, ITenantContext tenantContext)
			: base(repository, unitOfWork)
		{
			_repository = projectTaskRepository;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_tenantProjectRepository = tenantProjectRepository;
			_customerRepository = customerRepository;
			_tenantContext = tenantContext;
		}

		public async Task<IEnumerable<ProjectTaskListDto>> GetByProjectAsync(Guid projectId)
		{
			var query = _repository.Where(t => t.ProjectId == projectId)
				.Include(t => t.Assignee)
				.Include(t => t.Customer);
			var list = await query.ToListAsync();
			return list.Select(MapToListDto).ToList();
		}

		public async Task<ProjectTaskListDto?> GetDetailAsync(Guid id)
		{
			var item = await _repository.Where(t => t.Id == id)
				.Include(t => t.Assignee)
				.Include(t => t.Customer)
				.FirstOrDefaultAsync();
			return item == null ? null : MapToListDto(item);
		}

		public async Task<ProjectTaskListDto> CreateAsync(ProjectTaskInsertDto dto)
		{
			var entity = new ProjectTask
			{
				ProjectId = dto.ProjectId,
				CustomerId = dto.CustomerId,
				Name = dto.Name,
				Description = dto.Description,
				StartDate = dto.StartDate,
				EndDate = dto.EndDate,
				Status = (ProjectTaskStatus)dto.Status,
				AssigneeId = dto.AssigneeId
			};
			// Auto-assign project customer if not provided
			if (entity.CustomerId == null || entity.CustomerId == Guid.Empty)
			{
				var proj = await _tenantProjectRepository.GetByIdStringGuidAsync(entity.ProjectId);
				if (proj != null && proj.CustomerId.HasValue)
				{
					entity.CustomerId = proj.CustomerId.Value;
				}
			}
			// Validate customer FK within current tenant scope
			if (entity.CustomerId != null && entity.CustomerId != Guid.Empty)
			{
				var cust = await _customerRepository.GetByIdStringGuidAsync(entity.CustomerId.Value);
				if (cust == null)
				{
					entity.CustomerId = null; // invalid -> clear
				}
			}
			await _repository.AddAsync(entity);
			await _unitOfWork.CommitAsync();
			// reload with relations
			var created = await _repository.Where(t => t.Id == entity.Id)
				.Include(t => t.Assignee)
				.Include(t => t.Customer)
				.FirstAsync();
			return MapToListDto(created);
		}

		public async Task<ProjectTaskListDto?> UpdateAsync(ProjectTaskUpdateDto dto)
		{
			ProjectTask? entity;
			entity = await _repository.GetByIdStringGuidAsync(dto.Id);
			if (entity == null) return null;

			entity.ProjectId = dto.ProjectId;
			entity.CustomerId = dto.CustomerId;
			entity.Name = dto.Name;
			entity.Description = dto.Description;
			entity.StartDate = dto.StartDate;
			entity.EndDate = dto.EndDate;
			entity.Status = (ProjectTaskStatus)dto.Status;
			entity.AssigneeId = dto.AssigneeId;

			// Auto-assign project customer if still empty
			if ((entity.CustomerId == null || entity.CustomerId == Guid.Empty))
			{
				var proj = await _tenantProjectRepository.GetByIdStringGuidAsync(entity.ProjectId);
				if (proj != null && proj.CustomerId.HasValue)
				{
					entity.CustomerId = proj.CustomerId.Value;
				}
			}
			// Validate customer FK within current tenant scope
			if (entity.CustomerId != null && entity.CustomerId != Guid.Empty)
			{
				var cust = await _customerRepository.GetByIdStringGuidAsync(entity.CustomerId.Value);
				if (cust == null)
				{
					entity.CustomerId = null;
				}
			}

			await base.UpdateAsync(entity);

			var updated = await _repository.Where(t => t.Id == entity.Id)
				.Include(t => t.Assignee)
				.Include(t => t.Customer)
				.FirstAsync();
			return MapToListDto(updated);
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var entity = await _repository.GetByIdStringGuidAsync(id);
			if (entity == null) return false;
			await base.RemoveAsync(entity);
			return true;
		}

		private static ProjectTaskListDto MapToListDto(ProjectTask t)
		{
			return new ProjectTaskListDto
			{
				Id = t.Id,
				ProjectId = t.ProjectId,
				CustomerId = t.CustomerId,
				CustomerName = t.Customer?.Name,
				Name = t.Name,
				Description = t.Description,
				StartDate = t.StartDate,
				EndDate = t.EndDate,
				Status = (int)t.Status,
				StatusText = t.Status.ToString(),
				AssigneeId = t.AssigneeId,
				AssigneeName = t.Assignee != null ? ($"{t.Assignee.FirstName} {t.Assignee.LastName}").Trim() : null
			};
		}
	}
}


