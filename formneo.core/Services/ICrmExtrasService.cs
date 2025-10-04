using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.DTOs.CRM;
using formneo.core.Models.CRM;

namespace formneo.core.Services
{
	public interface IOpportunityService 
	{ 
		Task<IEnumerable<OpportunityDto>> ListAsync(Guid customerId); 
		Task<OpportunityPagedResultDto> GetListPagedAsync(int page = 1, int pageSize = 50, string search = "", int? stage = null, Guid? customerId = null);
		Task<OpportunityDto> GetByIdAsync(Guid id); 
		Task<OpportunityDto> CreateAsync(OpportunityInsertDto dto); 
		Task<OpportunityDto> UpdateAsync(OpportunityUpdateDto dto); 
		Task DeleteAsync(Guid id);
		Task<int> GetTotalCountAsync(string search = "", int? stage = null, Guid? customerId = null);
		Task<OpportunityDashboardDto> GetDashboardAsync(int months = 12, Guid? customerId = null);
            Task<OpportunityDto> UpdateStageAsync(Guid id, OpportunityStage stage);
            Task<OpportunityDto> MarkWonAsync(Guid id);
            Task<OpportunityDto> MarkLostAsync(Guid id);
	}
	public interface IActivityService { Task<IEnumerable<ActivityDto>> ListAsync(Guid customerId); Task<ActivityDto> GetByIdAsync(Guid id); Task<ActivityDto> CreateAsync(ActivityDto dto); Task<ActivityDto> UpdateAsync(ActivityDto dto); Task DeleteAsync(Guid id); }
	public interface IReminderService { Task<IEnumerable<ReminderDto>> ListAsync(Guid customerId); Task<ReminderDto> GetByIdAsync(Guid id); Task<ReminderDto> CreateAsync(ReminderDto dto); Task<ReminderDto> UpdateAsync(ReminderDto dto); Task DeleteAsync(Guid id); }
	public interface IMeetingService { Task<IEnumerable<MeetingDto>> ListAsync(Guid customerId); Task<MeetingDto> GetByIdAsync(Guid id); Task<MeetingDto> CreateAsync(MeetingDto dto); Task<MeetingDto> UpdateAsync(MeetingDto dto); Task DeleteAsync(Guid id); }
	public interface IQuoteService { Task<IEnumerable<QuoteDto>> ListAsync(Guid customerId); Task<QuoteDto> GetByIdAsync(Guid id); Task<QuoteDto> CreateAsync(QuoteDto dto); Task<QuoteDto> UpdateAsync(QuoteDto dto); Task DeleteAsync(Guid id); }
	public interface ISpecialDayService { Task<IEnumerable<SpecialDayDto>> ListAsync(Guid customerId); Task<SpecialDayDto> GetByIdAsync(Guid id); Task<SpecialDayDto> CreateAsync(SpecialDayDto dto); Task<SpecialDayDto> UpdateAsync(SpecialDayDto dto); Task DeleteAsync(Guid id); }
	public interface ICrmChangeLogService { Task<IEnumerable<CrmChangeLogDto>> ListByCustomerAsync(Guid customerId); Task<IEnumerable<CrmChangeLogDto>> ListByEntityAsync(string entityName, Guid entityId); }
}


