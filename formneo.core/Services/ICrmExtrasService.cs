using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.DTOs.CRM;

namespace vesa.core.Services
{
	public interface IOpportunityService { Task<IEnumerable<OpportunityDto>> ListAsync(Guid customerId); Task<OpportunityDto> GetByIdAsync(Guid id); Task<OpportunityDto> CreateAsync(OpportunityDto dto); Task<OpportunityDto> UpdateAsync(OpportunityDto dto); Task DeleteAsync(Guid id); }
	public interface IActivityService { Task<IEnumerable<ActivityDto>> ListAsync(Guid customerId); Task<ActivityDto> GetByIdAsync(Guid id); Task<ActivityDto> CreateAsync(ActivityDto dto); Task<ActivityDto> UpdateAsync(ActivityDto dto); Task DeleteAsync(Guid id); }
	public interface IReminderService { Task<IEnumerable<ReminderDto>> ListAsync(Guid customerId); Task<ReminderDto> GetByIdAsync(Guid id); Task<ReminderDto> CreateAsync(ReminderDto dto); Task<ReminderDto> UpdateAsync(ReminderDto dto); Task DeleteAsync(Guid id); }
	public interface IMeetingService { Task<IEnumerable<MeetingDto>> ListAsync(Guid customerId); Task<MeetingDto> GetByIdAsync(Guid id); Task<MeetingDto> CreateAsync(MeetingDto dto); Task<MeetingDto> UpdateAsync(MeetingDto dto); Task DeleteAsync(Guid id); }
	public interface IQuoteService { Task<IEnumerable<QuoteDto>> ListAsync(Guid customerId); Task<QuoteDto> GetByIdAsync(Guid id); Task<QuoteDto> CreateAsync(QuoteDto dto); Task<QuoteDto> UpdateAsync(QuoteDto dto); Task DeleteAsync(Guid id); }
	public interface ISpecialDayService { Task<IEnumerable<SpecialDayDto>> ListAsync(Guid customerId); Task<SpecialDayDto> GetByIdAsync(Guid id); Task<SpecialDayDto> CreateAsync(SpecialDayDto dto); Task<SpecialDayDto> UpdateAsync(SpecialDayDto dto); Task DeleteAsync(Guid id); }
	public interface ICrmChangeLogService { Task<IEnumerable<CrmChangeLogDto>> ListByCustomerAsync(Guid customerId); Task<IEnumerable<CrmChangeLogDto>> ListByEntityAsync(string entityName, Guid entityId); }
}


