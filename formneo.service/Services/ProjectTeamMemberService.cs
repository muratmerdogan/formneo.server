using Microsoft.EntityFrameworkCore;
using formneo.core.DTOs.ProjectTeamMember;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;

namespace formneo.service.Services
{
	public class ProjectTeamMemberService : Service<ProjectTeamMember>, IProjectTeamMemberService
	{
		private readonly IProjectTeamMemberRepository _repository;
		private readonly IUnitOfWork _unitOfWork;

		public ProjectTeamMemberService(IGenericRepository<ProjectTeamMember> repository, IUnitOfWork unitOfWork, IProjectTeamMemberRepository projectTeamMemberRepository)
			: base(repository, unitOfWork)
		{
			_repository = projectTeamMemberRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<ProjectTeamMemberListDto>> GetByProjectAsync(Guid projectId)
		{
			var list = await _repository.Where(x => x.ProjectId == projectId)
				.Include(x => x.User)
				.ToListAsync();
			return list.Select(MapToDto).ToList();
		}

		public async Task<ProjectTeamMemberListDto> CreateAsync(ProjectTeamMemberInsertDto dto)
		{
			var entity = new ProjectTeamMember
			{
				ProjectId = dto.ProjectId,
				UserId = dto.UserId,
				Role = dto.Role,
				IsActive = true
			};
			await _repository.AddAsync(entity);
			await _unitOfWork.CommitAsync();
			var created = await _repository.Where(x => x.Id == entity.Id)
				.Include(x => x.User)
				.FirstAsync();
			return MapToDto(created);
		}

		public async Task<ProjectTeamMemberListDto?> UpdateAsync(ProjectTeamMemberUpdateDto dto)
		{
			var entity = await _repository.GetByIdStringGuidAsync(dto.Id);
			if (entity == null) return null;
			entity.Role = dto.Role;
			entity.IsActive = dto.IsActive;
			await base.UpdateAsync(entity);
			var updated = await _repository.Where(x => x.Id == entity.Id)
				.Include(x => x.User)
				.FirstAsync();
			return MapToDto(updated);
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var entity = await _repository.GetByIdStringGuidAsync(id);
			if (entity == null) return false;
			await base.RemoveAsync(entity);
			return true;
		}

		private static ProjectTeamMemberListDto MapToDto(ProjectTeamMember m)
		{
			return new ProjectTeamMemberListDto
			{
				Id = m.Id,
				ProjectId = m.ProjectId,
				UserId = m.UserId,
				UserName = m.User?.UserName,
				FullName = m.User != null ? ($"{m.User.FirstName} {m.User.LastName}").Trim() : null,
				Role = m.Role,
				IsActive = m.IsActive
			};
		}
	}
}


