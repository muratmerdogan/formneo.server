using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using formneo.core.DTOs.CRM;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;
using formneo.service.Exceptions;
using formneo.core.Models.CRM;

namespace formneo.service.Services
{
	public class OpportunityService : IOpportunityService
	{
		private readonly IOpportunityRepository _repo;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _uow;

		public OpportunityService(IOpportunityRepository repo, IMapper mapper, IUnitOfWork uow)
		{
			_repo = repo;
			_mapper = mapper;
			_uow = uow;
		}

		public async Task<OpportunityDto> CreateAsync(OpportunityInsertDto dto)
		{
			var entity = _mapper.Map<Opportunity>(dto);
			await _repo.AddAsync(entity);
			await _uow.CommitAsync();
			return _mapper.Map<OpportunityDto>(entity);
		}

		public async Task<IEnumerable<OpportunityDto>> ListAsync(Guid customerId)
		{
			var query = _repo.Where(x => x.CustomerId == customerId);
			var list = await query.AsNoTracking().ToListAsync();
			return _mapper.Map<List<OpportunityDto>>(list);
		}

		public async Task<OpportunityPagedResultDto> GetListPagedAsync(int page = 1, int pageSize = 50, string search = "", int? stage = null, Guid? customerId = null)
		{
			var skip = (page - 1) * pageSize;
			var totalCount = await _repo.GetTotalCountAsync(search, stage, customerId);
			var opportunities = await _repo.GetListPagedAsync(skip, pageSize, search, stage, customerId);
			var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

			return new OpportunityPagedResultDto
			{
				Items = _mapper.Map<List<OpportunityListDto>>(opportunities),
				TotalCount = totalCount,
				Page = page,
				PageSize = pageSize,
				TotalPages = totalPages,
				HasNextPage = page < totalPages,
				HasPreviousPage = page > 1
			};
		}

		public async Task<OpportunityDto> GetByIdAsync(Guid id)
		{
			var entity = await _repo.GetByIdStringGuidAsync(id);
			return _mapper.Map<OpportunityDto>(entity);
		}

		public async Task<OpportunityDto> UpdateAsync(OpportunityUpdateDto dto)
		{
			var entity = await _repo.GetByIdStringGuidAsync(dto.Id);
			if (entity == null) return null;

			_mapper.Map(dto, entity);
			_repo.Update(entity);
			
			try
			{
				await _uow.CommitAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw new ClientSideException("Kayıt başka biri tarafından güncellendi.");
			}
			
			return _mapper.Map<OpportunityDto>(entity);
		}

		public async Task<OpportunityDto> UpdateStageAsync(Guid id, OpportunityStage stage)
		{
			var entity = await _repo.GetByIdStringGuidAsync(id);
			if (entity == null) return null;
			entity.Stage = stage;
			_repo.Update(entity);
			try
			{
				await _uow.CommitAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw new ClientSideException("Kayıt başka biri tarafından güncellendi.");
			}
			return _mapper.Map<OpportunityDto>(entity);
		}

		public Task<OpportunityDto> MarkWonAsync(Guid id) => UpdateStageAsync(id, OpportunityStage.Won);
		public Task<OpportunityDto> MarkLostAsync(Guid id) => UpdateStageAsync(id, OpportunityStage.Lost);

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _repo.GetByIdStringGuidAsync(id);
			if (entity == null) return;
			entity.IsDelete = true;
			_repo.Update(entity);
			await _uow.CommitAsync();
		}

		public async Task<int> GetTotalCountAsync(string search = "", int? stage = null, Guid? customerId = null)
		{
			return await _repo.GetTotalCountAsync(search, stage, customerId);
		}

		public async Task<OpportunityDashboardDto> GetDashboardAsync(int months = 12, Guid? customerId = null)
		{
			var metrics = await _repo.GetMetricsAsync(customerId);
			var stageStats = await _repo.GetStageStatsAsync(customerId);
			var trends = await _repo.GetMonthlyTrendsAsync(months, customerId);
			var topCustomers = customerId.HasValue 
				? new List<OpportunityTopCustomerDto>() // Belirli müşteri için top customer listesi anlamsız
				: await _repo.GetTopCustomersAsync(10);

			return new OpportunityDashboardDto
			{
				Metrics = metrics,
				StageStats = stageStats,
				MonthlyTrends = trends,
				TopCustomers = topCustomers
			};
		}
	}

	public class ActivityService : IActivityService
	{
		private readonly IActivityRepository _repo;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _uow;

		public ActivityService(IActivityRepository repo, IMapper mapper, IUnitOfWork uow)
		{
			_repo = repo;
			_mapper = mapper;
			_uow = uow;
		}

		public async Task<ActivityDto> CreateAsync(ActivityDto dto)
		{
			var entity = _mapper.Map<formneo.core.Models.CRM.Activity>(dto);
			await _repo.AddAsync(entity);
			await _uow.CommitAsync();
			return _mapper.Map<ActivityDto>(entity);
		}

		public async Task<IEnumerable<ActivityDto>> ListAsync(Guid customerId)
		{
			var query = _repo.Where(x => x.CustomerId == customerId);
			var list = await query.AsNoTracking().ToListAsync();
			return _mapper.Map<List<ActivityDto>>(list);
		}

		public async Task<ActivityDto> GetByIdAsync(Guid id)
		{
			var entity = await _repo.GetByIdStringGuidAsync(id);
			return _mapper.Map<ActivityDto>(entity);
		}

		public async Task<ActivityDto> UpdateAsync(ActivityDto dto)
		{
			var entity = await _repo.GetByIdStringGuidAsync(dto.Id);
			if (entity == null) return null;

			_mapper.Map(dto, entity);
			_repo.Update(entity);
			
			try
			{
				await _uow.CommitAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw new ClientSideException("Kayıt başka biri tarafından güncellendi.");
			}
			
			return _mapper.Map<ActivityDto>(entity);
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _repo.GetByIdStringGuidAsync(id);
			if (entity == null) return;
			entity.IsDelete = true;
			_repo.Update(entity);
			await _uow.CommitAsync();
		}
	}

	public class ReminderService : IReminderService
	{
		private readonly IReminderRepository _repo;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _uow;

		public ReminderService(IReminderRepository repo, IMapper mapper, IUnitOfWork uow)
		{
			_repo = repo;
			_mapper = mapper;
			_uow = uow;
		}

		public async Task<ReminderDto> CreateAsync(ReminderDto dto)
		{
			var entity = _mapper.Map<formneo.core.Models.CRM.Reminder>(dto);
			await _repo.AddAsync(entity);
			await _uow.CommitAsync();
			return _mapper.Map<ReminderDto>(entity);
		}

		public async Task<IEnumerable<ReminderDto>> ListAsync(Guid customerId)
		{
			var query = _repo.Where(x => x.CustomerId == customerId);
			var list = await query.AsNoTracking().ToListAsync();
			return _mapper.Map<List<ReminderDto>>(list);
		}

		public async Task<ReminderDto> GetByIdAsync(Guid id)
		{
			var entity = await _repo.GetByIdStringGuidAsync(id);
			return _mapper.Map<ReminderDto>(entity);
		}

		public async Task<ReminderDto> UpdateAsync(ReminderDto dto)
		{
			var entity = await _repo.GetByIdStringGuidAsync(dto.Id);
			if (entity == null) return null;

			_mapper.Map(dto, entity);
			_repo.Update(entity);
			
			try
			{
				await _uow.CommitAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw new ClientSideException("Kayıt başka biri tarafından güncellendi.");
			}
			
			return _mapper.Map<ReminderDto>(entity);
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _repo.GetByIdStringGuidAsync(id);
			if (entity == null) return;
			entity.IsDelete = true;
			_repo.Update(entity);
			await _uow.CommitAsync();
		}
	}

	public class MeetingService : IMeetingService
	{
		private readonly IMeetingRepository _repo;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _uow;

		public MeetingService(IMeetingRepository repo, IMapper mapper, IUnitOfWork uow)
		{
			_repo = repo;
			_mapper = mapper;
			_uow = uow;
		}

		public async Task<MeetingDto> CreateAsync(MeetingDto dto)
		{
			var entity = _mapper.Map<formneo.core.Models.CRM.Meeting>(dto);
			await _repo.AddAsync(entity);
			await _uow.CommitAsync();
			return _mapper.Map<MeetingDto>(entity);
		}

		public async Task<IEnumerable<MeetingDto>> ListAsync(Guid customerId)
		{
			var query = _repo.Where(x => x.CustomerId == customerId);
			var list = await query.AsNoTracking().ToListAsync();
			return _mapper.Map<List<MeetingDto>>(list);
		}

		public async Task<MeetingDto> GetByIdAsync(Guid id)
		{
			var entity = await _repo.GetByIdStringGuidAsync(id);
			return _mapper.Map<MeetingDto>(entity);
		}

		public async Task<MeetingDto> UpdateAsync(MeetingDto dto)
		{
			var entity = await _repo.GetByIdStringGuidAsync(dto.Id);
			if (entity == null) return null;

			_mapper.Map(dto, entity);
			_repo.Update(entity);
			
			try
			{
				await _uow.CommitAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw new ClientSideException("Kayıt başka biri tarafından güncellendi.");
			}
			
			return _mapper.Map<MeetingDto>(entity);
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _repo.GetByIdStringGuidAsync(id);
			if (entity == null) return;
			entity.IsDelete = true;
			_repo.Update(entity);
			await _uow.CommitAsync();
		}
	}

	public class QuoteService : IQuoteService
	{
		private readonly IQuoteRepository _repo;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _uow;

		public QuoteService(IQuoteRepository repo, IMapper mapper, IUnitOfWork uow)
		{
			_repo = repo;
			_mapper = mapper;
			_uow = uow;
		}

		public async Task<QuoteDto> CreateAsync(QuoteDto dto)
		{
			var entity = _mapper.Map<formneo.core.Models.CRM.Quote>(dto);
			await _repo.AddAsync(entity);
			await _uow.CommitAsync();
			return _mapper.Map<QuoteDto>(entity);
		}

		public async Task<IEnumerable<QuoteDto>> ListAsync(Guid customerId)
		{
			var query = _repo.Where(x => x.CustomerId == customerId);
			var list = await query.AsNoTracking().ToListAsync();
			return _mapper.Map<List<QuoteDto>>(list);
		}

		public async Task<QuoteDto> GetByIdAsync(Guid id)
		{
			var entity = await _repo.GetByIdStringGuidAsync(id);
			return _mapper.Map<QuoteDto>(entity);
		}

		public async Task<QuoteDto> UpdateAsync(QuoteDto dto)
		{
			var entity = await _repo.GetByIdStringGuidAsync(dto.Id);
			if (entity == null) return null;

			_mapper.Map(dto, entity);
			_repo.Update(entity);
			
			try
			{
				await _uow.CommitAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw new ClientSideException("Kayıt başka biri tarafından güncellendi.");
			}
			
			return _mapper.Map<QuoteDto>(entity);
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _repo.GetByIdStringGuidAsync(id);
			if (entity == null) return;
			entity.IsDelete = true;
			_repo.Update(entity);
			await _uow.CommitAsync();
		}
	}

	public class SpecialDayService : ISpecialDayService
	{
		private readonly ISpecialDayRepository _repo;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _uow;

		public SpecialDayService(ISpecialDayRepository repo, IMapper mapper, IUnitOfWork uow)
		{
			_repo = repo;
			_mapper = mapper;
			_uow = uow;
		}

		public async Task<SpecialDayDto> CreateAsync(SpecialDayDto dto)
		{
			var entity = _mapper.Map<SpecialDay>(dto);
			await _repo.AddAsync(entity);
			await _uow.CommitAsync();
			return _mapper.Map<SpecialDayDto>(entity);
		}

		public async Task<IEnumerable<SpecialDayDto>> ListAsync(Guid customerId)
		{
			var list = await _repo.Where(x => x.CustomerId == customerId).AsNoTracking().ToListAsync();
			return _mapper.Map<List<SpecialDayDto>>(list);
		}

		public async Task<SpecialDayDto> GetByIdAsync(Guid id)
		{
			var entity = await _repo.GetByIdStringGuidAsync(id);
			return _mapper.Map<SpecialDayDto>(entity);
		}

		public async Task<SpecialDayDto> UpdateAsync(SpecialDayDto dto)
		{
			var entity = await _repo.GetByIdStringGuidAsync(dto.Id);
			if (entity == null) return null;

			_mapper.Map(dto, entity);
			_repo.Update(entity);
			
			try
			{
				await _uow.CommitAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw new ClientSideException("Kayıt başka biri tarafından güncellendi.");
			}
			
			return _mapper.Map<SpecialDayDto>(entity);
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _repo.GetByIdStringGuidAsync(id);
			if (entity == null) return;
			entity.IsDelete = true;
			_repo.Update(entity);
			await _uow.CommitAsync();
		}
	}

	public class CrmChangeLogService : ICrmChangeLogService
	{
		private readonly ICrmChangeLogRepository _repo;
		private readonly IMapper _mapper;

		public CrmChangeLogService(ICrmChangeLogRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<IEnumerable<CrmChangeLogDto>> ListByCustomerAsync(Guid customerId)
		{
			var list = await _repo.Where(x => x.CustomerId == customerId)
				.OrderByDescending(x => x.ChangedDate)
				.AsNoTracking()
				.ToListAsync();
			return _mapper.Map<List<CrmChangeLogDto>>(list);
		}

		public async Task<IEnumerable<CrmChangeLogDto>> ListByEntityAsync(string entityName, Guid entityId)
		{
			var list = await _repo.Where(x => x.EntityName == entityName && x.EntityId == entityId)
				.OrderByDescending(x => x.ChangedDate)
				.AsNoTracking()
				.ToListAsync();
			return _mapper.Map<List<CrmChangeLogDto>>(list);
		}
	}
}