using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;

namespace formneo.service.Services
{
	public class UserTenantRoleService : IUserTenantRoleService
	{
		private readonly IUserTenantRoleRepository _repository;
		private readonly IGenericRepository<UserTenantRole> _genericRepository;
		private readonly IUnitOfWork _unitOfWork;

		public UserTenantRoleService(IUserTenantRoleRepository repository, IGenericRepository<UserTenantRole> genericRepository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_genericRepository = genericRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<UserTenantRole> AddAsync(UserTenantRole entity)
		{
			await _genericRepository.AddAsync(entity);
			await _unitOfWork.CommitAsync();
			return entity;
		}

		public async Task<IEnumerable<UserTenantRole>> AddRangeAsync(IEnumerable<UserTenantRole> entities)
		{
			await _genericRepository.AddRangeAsync(entities);
			await _unitOfWork.CommitAsync();
			return entities;
		}

		public Task<bool> AnyAsync(Expression<Func<UserTenantRole, bool>> expression)
		{
			return _genericRepository.AnyAsync(expression);
		}

		public Task<IEnumerable<UserTenantRole>> GetAllAsync()
		{
			return Task.FromResult(_genericRepository.GetAll().AsEnumerable());
		}

		public Task<UserTenantRole> GetByIdAsync(int id)
		{
			return _genericRepository.GetByIdAsync(id);
		}

		public Task<UserTenantRole> GetByIdStringAsync(string id)
		{
			return _genericRepository.GetByIdStringAsync(id);
		}

		public Task<UserTenantRole> GetByIdStringGuidAsync(Guid id)
		{
			return _genericRepository.GetByIdStringGuidAsync(id);
		}

		public IQueryable<UserTenantRole> Where(Expression<Func<UserTenantRole, bool>> expression)
		{
			return _genericRepository.Where(expression);
		}

		public async Task UpdateAsync(UserTenantRole entity)
		{
			_genericRepository.Update(entity);
			await _unitOfWork.CommitAsync();
		}

		public async Task RemoveAsync(UserTenantRole entity)
		{
			_genericRepository.Remove(entity);
			await _unitOfWork.CommitAsync();
		}

		public async Task RemoveRangeAsync(IEnumerable<UserTenantRole> entities)
		{
			_genericRepository.RemoveRange(entities);
			await _unitOfWork.CommitAsync();
		}

		public async Task<List<UserTenantRole>> GetByUserAndTenantAsync(string userId, Guid tenantId)
		{
			return await _repository.GetByUserAndTenantAsync(userId, tenantId);
		}
	}
}


