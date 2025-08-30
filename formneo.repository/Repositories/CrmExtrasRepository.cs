using vesa.core.Models.CRM;
using vesa.core.Repositories;

namespace vesa.repository.Repositories
{
	public class OpportunityRepository : GenericRepository<Opportunity>, IOpportunityRepository
	{
		public OpportunityRepository(AppDbContext context) : base(context) { }
	}

	public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
	{
		public ActivityRepository(AppDbContext context) : base(context) { }
	}

	public class ReminderRepository : GenericRepository<Reminder>, IReminderRepository
	{
		public ReminderRepository(AppDbContext context) : base(context) { }
	}

	public class MeetingRepository : GenericRepository<Meeting>, IMeetingRepository
	{
		public MeetingRepository(AppDbContext context) : base(context) { }
	}

	public class QuoteRepository : GenericRepository<Quote>, IQuoteRepository
	{
		public QuoteRepository(AppDbContext context) : base(context) { }
	}

	public class QuoteLineRepository : GenericRepository<QuoteLine>, IQuoteLineRepository
	{
		public QuoteLineRepository(AppDbContext context) : base(context) { }
	}

	public class SpecialDayRepository : GenericRepository<SpecialDay>, ISpecialDayRepository
	{
		public SpecialDayRepository(AppDbContext context) : base(context) { }
	}

	public class CrmChangeLogRepository : GenericRepository<CrmChangeLog>, ICrmChangeLogRepository
	{
		public CrmChangeLogRepository(AppDbContext context) : base(context) { }
	}
}


