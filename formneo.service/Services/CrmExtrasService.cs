using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vesa.core.DTOs.CRM;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;
using vesa.service.Exceptions;
using vesa.core.Models.CRM;

namespace vesa.service.Services
{
	public class OpportunityService : IOpportunityService
	{
		private readonly IOpportunityRepository _repo;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _uow;
		public OpportunityService(IOpportunityRepository repo, IMapper mapper, IUnitOfWork uow){_repo=repo;_mapper=mapper;_uow=uow;}
		public async Task<OpportunityDto> CreateAsync(OpportunityDto dto){var e=_mapper.Map<vesa.core.Models.CRM.Opportunity>(dto);await _repo.AddAsync(e);await _uow.CommitAsync();return _mapper.Map<OpportunityDto>(e);}
		public async Task<IEnumerable<OpportunityDto>> ListAsync(Guid customerId){var q=_repo.Where(x=>x.CustomerId==customerId);var l=await q.AsNoTracking().ToListAsync();return _mapper.Map<List<OpportunityDto>>(l);}
		public async Task<OpportunityDto> GetByIdAsync(Guid id){var e=await _repo.GetByIdStringGuidAsync(id);return _mapper.Map<OpportunityDto>(e);}
		public async Task<OpportunityDto> UpdateAsync(OpportunityDto dto){var e=await _repo.GetByIdStringGuidAsync(dto.Id);if(e==null)return null; if(!e.RowVersion.SequenceEqual(dto.RowVersion ?? Array.Empty<byte>())) throw new ClientSideException("Kayıt başka biri tarafından güncellendi."); _mapper.Map(dto,e);_repo.Update(e);await _uow.CommitAsync();return _mapper.Map<OpportunityDto>(e);}
		public async Task DeleteAsync(Guid id){var e=await _repo.GetByIdStringGuidAsync(id);if(e==null)return; e.IsDelete=true; _repo.Update(e); await _uow.CommitAsync();}
	}

	public class ActivityService : IActivityService
	{
		private readonly IActivityRepository _repo; private readonly IMapper _mapper; private readonly IUnitOfWork _uow;
		public ActivityService(IActivityRepository repo,IMapper mapper,IUnitOfWork uow){_repo=repo;_mapper=mapper;_uow=uow;}
		public async Task<ActivityDto> CreateAsync(ActivityDto dto){var e=_mapper.Map<vesa.core.Models.CRM.Activity>(dto);await _repo.AddAsync(e);await _uow.CommitAsync();return _mapper.Map<ActivityDto>(e);}
		public async Task<IEnumerable<ActivityDto>> ListAsync(Guid customerId){var q=_repo.Where(x=>x.CustomerId==customerId);var l=await q.AsNoTracking().ToListAsync();return _mapper.Map<List<ActivityDto>>(l);}
		public async Task<ActivityDto> GetByIdAsync(Guid id){var e=await _repo.GetByIdStringGuidAsync(id);return _mapper.Map<ActivityDto>(e);}
		public async Task<ActivityDto> UpdateAsync(ActivityDto dto){var e=await _repo.GetByIdStringGuidAsync(dto.Id);if(e==null)return null; if(!(dto.RowVersion==null || (e.RowVersion!=null && e.RowVersion.SequenceEqual(dto.RowVersion)))) throw new ClientSideException("Kayıt başka biri tarafından güncellendi."); _mapper.Map(dto,e); _repo.Update(e); await _uow.CommitAsync(); return _mapper.Map<ActivityDto>(e);}
		public async Task DeleteAsync(Guid id){var e=await _repo.GetByIdStringGuidAsync(id);if(e==null)return; e.IsDelete=true; _repo.Update(e); await _uow.CommitAsync();}
	}

	public class ReminderService : IReminderService
	{
		private readonly IReminderRepository _repo; private readonly IMapper _mapper; private readonly IUnitOfWork _uow;
		public ReminderService(IReminderRepository repo,IMapper mapper,IUnitOfWork uow){_repo=repo;_mapper=mapper;_uow=uow;}
		public async Task<ReminderDto> CreateAsync(ReminderDto dto){var e=_mapper.Map<vesa.core.Models.CRM.Reminder>(dto);await _repo.AddAsync(e);await _uow.CommitAsync();return _mapper.Map<ReminderDto>(e);}
		public async Task<IEnumerable<ReminderDto>> ListAsync(Guid customerId){var q=_repo.Where(x=>x.CustomerId==customerId);var l=await q.AsNoTracking().ToListAsync();return _mapper.Map<List<ReminderDto>>(l);}
		public async Task<ReminderDto> GetByIdAsync(Guid id){var e=await _repo.GetByIdStringGuidAsync(id);return _mapper.Map<ReminderDto>(e);}
		public async Task<ReminderDto> UpdateAsync(ReminderDto dto){var e=await _repo.GetByIdStringGuidAsync(dto.Id);if(e==null)return null; if(!(dto.RowVersion==null || (e.RowVersion!=null && e.RowVersion.SequenceEqual(dto.RowVersion)))) throw new ClientSideException("Kayıt başka biri tarafından güncellendi."); _mapper.Map(dto,e); _repo.Update(e); await _uow.CommitAsync(); return _mapper.Map<ReminderDto>(e);}
		public async Task DeleteAsync(Guid id){var e=await _repo.GetByIdStringGuidAsync(id);if(e==null)return; e.IsDelete=true; _repo.Update(e); await _uow.CommitAsync();}
	}

	public class MeetingService : IMeetingService
	{
		private readonly IMeetingRepository _repo; private readonly IMapper _mapper; private readonly IUnitOfWork _uow;
		public MeetingService(IMeetingRepository repo,IMapper mapper,IUnitOfWork uow){_repo=repo;_mapper=mapper;_uow=uow;}
		public async Task<MeetingDto> CreateAsync(MeetingDto dto){var e=_mapper.Map<vesa.core.Models.CRM.Meeting>(dto);await _repo.AddAsync(e);await _uow.CommitAsync();return _mapper.Map<MeetingDto>(e);}
		public async Task<IEnumerable<MeetingDto>> ListAsync(Guid customerId){var q=_repo.Where(x=>x.CustomerId==customerId);var l=await q.AsNoTracking().ToListAsync();return _mapper.Map<List<MeetingDto>>(l);}
		public async Task<MeetingDto> GetByIdAsync(Guid id){var e=await _repo.GetByIdStringGuidAsync(id);return _mapper.Map<MeetingDto>(e);}
		public async Task<MeetingDto> UpdateAsync(MeetingDto dto){var e=await _repo.GetByIdStringGuidAsync(dto.Id);if(e==null)return null; if(!(dto.RowVersion==null || (e.RowVersion!=null && e.RowVersion.SequenceEqual(dto.RowVersion)))) throw new ClientSideException("Kayıt başka biri tarafından güncellendi."); _mapper.Map(dto,e); _repo.Update(e); await _uow.CommitAsync(); return _mapper.Map<MeetingDto>(e);}
		public async Task DeleteAsync(Guid id){var e=await _repo.GetByIdStringGuidAsync(id);if(e==null)return; e.IsDelete=true; _repo.Update(e); await _uow.CommitAsync();}
	}

	public class QuoteService : IQuoteService
	{
		private readonly IQuoteRepository _repo; private readonly IMapper _mapper; private readonly IUnitOfWork _uow;
		public QuoteService(IQuoteRepository repo,IMapper mapper,IUnitOfWork uow){_repo=repo;_mapper=mapper;_uow=uow;}
		public async Task<QuoteDto> CreateAsync(QuoteDto dto){var e=_mapper.Map<vesa.core.Models.CRM.Quote>(dto);await _repo.AddAsync(e);await _uow.CommitAsync();return _mapper.Map<QuoteDto>(e);}
		public async Task<IEnumerable<QuoteDto>> ListAsync(Guid customerId){var q=_repo.Where(x=>x.CustomerId==customerId);var l=await q.AsNoTracking().ToListAsync();return _mapper.Map<List<QuoteDto>>(l);}
		public async Task<QuoteDto> GetByIdAsync(Guid id){var e=await _repo.GetByIdStringGuidAsync(id);return _mapper.Map<QuoteDto>(e);}
		public async Task<QuoteDto> UpdateAsync(QuoteDto dto){var e=await _repo.GetByIdStringGuidAsync(dto.Id);if(e==null)return null; if(!e.RowVersion.SequenceEqual(dto.RowVersion ?? Array.Empty<byte>())) throw new ClientSideException("Kayıt başka biri tarafından güncellendi."); _mapper.Map(dto,e); _repo.Update(e); await _uow.CommitAsync(); return _mapper.Map<QuoteDto>(e);}
		public async Task DeleteAsync(Guid id){var e=await _repo.GetByIdStringGuidAsync(id);if(e==null)return; e.IsDelete=true; _repo.Update(e); await _uow.CommitAsync();}
	}

	public class SpecialDayService : ISpecialDayService
	{
		private readonly ISpecialDayRepository _repo; private readonly IMapper _mapper; private readonly IUnitOfWork _uow;
		public SpecialDayService(ISpecialDayRepository repo,IMapper mapper,IUnitOfWork uow){_repo=repo;_mapper=mapper;_uow=uow;}
		public async Task<SpecialDayDto> CreateAsync(SpecialDayDto dto){var e=_mapper.Map<SpecialDay>(dto);await _repo.AddAsync(e);await _uow.CommitAsync();return _mapper.Map<SpecialDayDto>(e);}
		public async Task DeleteAsync(Guid id){var e=await _repo.GetByIdStringGuidAsync(id);if(e==null)return; e.IsDelete=true; _repo.Update(e); await _uow.CommitAsync();}
		public async Task<SpecialDayDto> GetByIdAsync(Guid id){var e=await _repo.GetByIdStringGuidAsync(id);return _mapper.Map<SpecialDayDto>(e);}
		public async Task<IEnumerable<SpecialDayDto>> ListAsync(Guid customerId){var l=await _repo.Where(x=>x.CustomerId==customerId).AsNoTracking().ToListAsync();return _mapper.Map<List<SpecialDayDto>>(l);}
		public async Task<SpecialDayDto> UpdateAsync(SpecialDayDto dto){var e=await _repo.GetByIdStringGuidAsync(dto.Id);if(e==null)return null; if(!e.RowVersion.SequenceEqual(dto.RowVersion ?? Array.Empty<byte>())) throw new ClientSideException("Kayıt başka biri tarafından güncellendi."); _mapper.Map(dto,e); _repo.Update(e); await _uow.CommitAsync(); return _mapper.Map<SpecialDayDto>(e);}
	}

	public class CrmChangeLogService : ICrmChangeLogService
	{
		private readonly ICrmChangeLogRepository _repo; private readonly IMapper _mapper;
		public CrmChangeLogService(ICrmChangeLogRepository repo,IMapper mapper){_repo=repo;_mapper=mapper;}
		public async Task<IEnumerable<CrmChangeLogDto>> ListByCustomerAsync(Guid customerId){var l=await _repo.Where(x=>x.CustomerId==customerId).OrderByDescending(x=>x.ChangedDate).AsNoTracking().ToListAsync();return _mapper.Map<List<CrmChangeLogDto>>(l);}
		public async Task<IEnumerable<CrmChangeLogDto>> ListByEntityAsync(string entityName, Guid entityId){var l=await _repo.Where(x=>x.EntityName==entityName && x.EntityId==entityId).OrderByDescending(x=>x.ChangedDate).AsNoTracking().ToListAsync();return _mapper.Map<List<CrmChangeLogDto>>(l);}
	}
}


