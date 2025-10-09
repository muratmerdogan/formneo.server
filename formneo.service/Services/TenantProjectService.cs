using Microsoft.EntityFrameworkCore;
using formneo.core.DTOs.TenantProject;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;

namespace formneo.service.Services
{
	public class TenantProjectService : Service<TenantProject>, ITenantProjectService
	{
		private readonly ITenantProjectRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		public TenantProjectService(IGenericRepository<TenantProject> repository, IUnitOfWork unitOfWork, ITenantProjectRepository tenantProjectRepository)
			: base(repository, unitOfWork)
		{
			_repository = tenantProjectRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<TenantProjectListDto>> GetAllAsyncList()
		{
			var list = await _repository.GetAll().Include(p => p.Customer).ToListAsync();
			return list.Select(MapToListDto).ToList();
		}

		public async Task<TenantProjectListDto?> GetDetailAsync(Guid id)
		{
			var item = await _repository.Where(p => p.Id == id).Include(p => p.Customer).FirstOrDefaultAsync();
			return item == null ? null : MapToListDto(item);
		}

		public async Task<TenantProjectListDto> CreateAsync(TenantProjectInsertDto dto)
		{
			var entity = new TenantProject
			{
				Name = dto.Name,
				Description = dto.Description,
				CustomerId = dto.CustomerId,
				IsPrivate = dto.IsPrivate
			};
			await _repository.AddAsync(entity);
			await _unitOfWork.CommitAsync();
			var created = await _repository.Where(p => p.Id == entity.Id).Include(p => p.Customer).FirstAsync();
			return MapToListDto(created);
		}

		public async Task<TenantProjectListDto?> UpdateAsync(TenantProjectUpdateDto dto)
		{
			var entity = await _repository.GetByIdStringGuidAsync(dto.Id);
			if (entity == null) return null;
			entity.Name = dto.Name;
			entity.Description = dto.Description;
			entity.CustomerId = dto.CustomerId;
			entity.IsPrivate = dto.IsPrivate;
			await base.UpdateAsync(entity);
			var updated = await _repository.Where(p => p.Id == entity.Id).Include(p => p.Customer).FirstAsync();
			return MapToListDto(updated);
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var entity = await _repository.GetByIdStringGuidAsync(id);
			if (entity == null) return false;
			await base.RemoveAsync(entity);
			return true;
		}

		private static TenantProjectListDto MapToListDto(TenantProject p)
		{
			return new TenantProjectListDto
			{
				Id = p.Id,
				Name = p.Name,
				Description = p.Description,
				CustomerId = p.CustomerId,
				CustomerName = p.Customer?.Name,
				IsPrivate = p.IsPrivate
			};
		}
	}
}


