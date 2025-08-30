using System;
using System.Threading.Tasks;
using vesa.core.Models.CRM;

namespace vesa.core.Repositories
{
	public interface IOpportunityRepository : IGenericRepository<Opportunity> { }
	public interface IActivityRepository : IGenericRepository<Activity> { }
	public interface IReminderRepository : IGenericRepository<Reminder> { }
	public interface IMeetingRepository : IGenericRepository<Meeting> { }
	public interface IQuoteRepository : IGenericRepository<Quote> { }
	public interface IQuoteLineRepository : IGenericRepository<QuoteLine> { }
	public interface ISpecialDayRepository : IGenericRepository<SpecialDay> { }
	public interface ICrmChangeLogRepository : IGenericRepository<CrmChangeLog> { }
}


