using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.DTOs.CRM;
using formneo.core.Models.CRM;

namespace formneo.core.Repositories
{
	public interface IOpportunityRepository : IGenericRepository<Opportunity> 
	{ 
		Task<List<Opportunity>> GetListPagedAsync(int skip, int take, string search = "", int? stage = null, Guid? customerId = null);
		Task<int> GetTotalCountAsync(string search = "", int? stage = null, Guid? customerId = null);
		Task<OpportunityMetricsDto> GetMetricsAsync(Guid? customerId = null);
		Task<List<OpportunityStageStatsDto>> GetStageStatsAsync(Guid? customerId = null);
		Task<List<OpportunityTrendDto>> GetMonthlyTrendsAsync(int months = 12, Guid? customerId = null);
		Task<List<OpportunityTopCustomerDto>> GetTopCustomersAsync(int count = 10);
	}
	public interface IActivityRepository : IGenericRepository<Activity> { }
	public interface IReminderRepository : IGenericRepository<Reminder> { }
	public interface IMeetingRepository : IGenericRepository<Meeting> { }
	public interface IQuoteRepository : IGenericRepository<Quote> { }
	public interface IQuoteLineRepository : IGenericRepository<QuoteLine> { }
	public interface ISpecialDayRepository : IGenericRepository<SpecialDay> { }
	public interface ICrmChangeLogRepository : IGenericRepository<CrmChangeLog> { }
}


