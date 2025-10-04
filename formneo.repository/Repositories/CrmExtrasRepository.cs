using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using formneo.core.DTOs.CRM;
using formneo.core.Models.CRM;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
	public class OpportunityRepository : GenericRepository<Opportunity>, IOpportunityRepository
	{
		public OpportunityRepository(AppDbContext context) : base(context) { }

		public async Task<List<Opportunity>> GetListPagedAsync(int skip, int take, string search = "", int? stage = null, Guid? customerId = null)
		{
			var query = _context.Opportunities
				.Include(x => x.Customer)
				.AsQueryable();

			// Search filter - Title ve Description'da arama
			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(x => x.Title.ToLower().Contains(search.ToLower()) ||
									   (x.Description != null && x.Description.ToLower().Contains(search.ToLower())));
			}

			// Stage filter
			if (stage.HasValue)
			{
				query = query.Where(x => (int)x.Stage == stage.Value);
			}

			// Customer filter
			if (customerId.HasValue)
			{
				query = query.Where(x => x.CustomerId == customerId.Value);
			}

			return await query
				.AsNoTracking()
				.OrderBy(x => x.CreatedDate)
				.Skip(skip)
				.Take(take)
				.ToListAsync();
		}

		public async Task<int> GetTotalCountAsync(string search = "", int? stage = null, Guid? customerId = null)
		{
			var query = _context.Opportunities.AsQueryable();

			// Search filter
			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(x => x.Title.ToLower().Contains(search.ToLower()) ||
									   (x.Description != null && x.Description.ToLower().Contains(search.ToLower())));
			}

			// Stage filter
			if (stage.HasValue)
			{
				query = query.Where(x => (int)x.Stage == stage.Value);
			}

			// Customer filter
			if (customerId.HasValue)
			{
				query = query.Where(x => x.CustomerId == customerId.Value);
			}

			return await query.CountAsync();
		}

		public async Task<OpportunityMetricsDto> GetMetricsAsync(Guid? customerId = null)
		{
			var query = _context.Opportunities.AsQueryable();
			
			if (customerId.HasValue)
				query = query.Where(x => x.CustomerId == customerId.Value);

			var opportunities = await query.ToListAsync();
			var totalOpps = opportunities.Count;
			var wonOpps = opportunities.Where(x => x.Stage == OpportunityStage.Won).ToList();
			var lostOpps = opportunities.Where(x => x.Stage == OpportunityStage.Lost).ToList();
			var closedOpps = wonOpps.Concat(lostOpps).ToList();

			return new OpportunityMetricsDto
			{
				TotalOpportunities = totalOpps,
				TotalAmount = opportunities.Where(x => x.Amount.HasValue).Sum(x => x.Amount.Value),
				AverageAmount = opportunities.Where(x => x.Amount.HasValue).Any() 
					? opportunities.Where(x => x.Amount.HasValue).Average(x => x.Amount.Value) : 0,
				WeightedAmount = opportunities.Where(x => x.Amount.HasValue && x.Probability.HasValue)
					.Sum(x => x.Amount.Value * (x.Probability.Value / 100m)),
				WonCount = wonOpps.Count,
				LostCount = lostOpps.Count,
				WinRate = closedOpps.Any() ? (decimal)wonOpps.Count / closedOpps.Count * 100 : 0,
				AverageDaysToClose = closedOpps.Any() 
					? (decimal)closedOpps.Where(x => x.UpdatedDate.HasValue)
						.Average(x => (x.UpdatedDate.Value - x.CreatedDate).TotalDays) : 0
			};
		}

		public async Task<List<OpportunityStageStatsDto>> GetStageStatsAsync(Guid? customerId = null)
		{
			var query = _context.Opportunities.AsQueryable();
			
			if (customerId.HasValue)
				query = query.Where(x => x.CustomerId == customerId.Value);

			var opportunities = await query.ToListAsync();
			var totalCount = opportunities.Count;

			var stageStats = opportunities
				.GroupBy(x => x.Stage)
				.Select(g => new OpportunityStageStatsDto
				{
					Stage = (int)g.Key,
					StageName = g.Key.ToString(),
					Count = g.Count(),
					TotalAmount = g.Where(x => x.Amount.HasValue).Sum(x => x.Amount.Value),
					AverageAmount = g.Where(x => x.Amount.HasValue).Any() 
						? g.Where(x => x.Amount.HasValue).Average(x => x.Amount.Value) : 0,
					Percentage = totalCount > 0 ? (decimal)g.Count() / totalCount * 100 : 0
				})
				.OrderBy(x => x.Stage)
				.ToList();

			return stageStats;
		}

		public async Task<List<OpportunityTrendDto>> GetMonthlyTrendsAsync(int months = 12, Guid? customerId = null)
		{
			var startDate = DateTime.UtcNow.AddMonths(-months);
			var query = _context.Opportunities
				.Where(x => x.CreatedDate >= startDate)
				.AsQueryable();
			
			if (customerId.HasValue)
				query = query.Where(x => x.CustomerId == customerId.Value);

			var opportunities = await query.ToListAsync();

			var trends = new List<OpportunityTrendDto>();
			for (int i = months - 1; i >= 0; i--)
			{
				var targetDate = DateTime.UtcNow.AddMonths(-i);
				var monthOpps = opportunities.Where(x => 
					x.CreatedDate.Year == targetDate.Year && x.CreatedDate.Month == targetDate.Month).ToList();
				var wonOpps = monthOpps.Where(x => x.Stage == OpportunityStage.Won).ToList();
				var lostOpps = monthOpps.Where(x => x.Stage == OpportunityStage.Lost).ToList();

				trends.Add(new OpportunityTrendDto
				{
					Year = targetDate.Year,
					Month = targetDate.Month,
					MonthName = targetDate.ToString("MMMM"),
					CreatedCount = monthOpps.Count,
					WonCount = wonOpps.Count,
					LostCount = lostOpps.Count,
					CreatedAmount = monthOpps.Where(x => x.Amount.HasValue).Sum(x => x.Amount.Value),
					WonAmount = wonOpps.Where(x => x.Amount.HasValue).Sum(x => x.Amount.Value),
					LostAmount = lostOpps.Where(x => x.Amount.HasValue).Sum(x => x.Amount.Value)
				});
			}

			return trends;
		}

		public async Task<List<OpportunityTopCustomerDto>> GetTopCustomersAsync(int count = 10)
		{
			var opportunities = await _context.Opportunities
				.Include(x => x.Customer)
				.ToListAsync();

			var customerStats = opportunities
				.GroupBy(x => new { x.CustomerId, CustomerName = x.Customer?.Name ?? "Unknown" })
				.Select(g =>
				{
					var wonOpps = g.Where(x => x.Stage == OpportunityStage.Won).ToList();
					var allOpps = g.ToList();
					var closedOpps = allOpps.Where(x => x.Stage == OpportunityStage.Won || x.Stage == OpportunityStage.Lost).ToList();

					return new OpportunityTopCustomerDto
					{
						CustomerId = g.Key.CustomerId,
						CustomerName = g.Key.CustomerName,
						OpportunityCount = allOpps.Count,
						TotalAmount = allOpps.Where(x => x.Amount.HasValue).Sum(x => x.Amount.Value),
						WonAmount = wonOpps.Where(x => x.Amount.HasValue).Sum(x => x.Amount.Value),
						WonCount = wonOpps.Count,
						WinRate = closedOpps.Any() ? (decimal)wonOpps.Count / closedOpps.Count * 100 : 0
					};
				})
				.OrderByDescending(x => x.TotalAmount)
				.Take(count)
				.ToList();

			return customerStats;
		}
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


