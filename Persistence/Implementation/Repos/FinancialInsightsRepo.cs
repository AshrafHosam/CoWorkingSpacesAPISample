using Application.Contracts.Repos;
using Application.Features.Insights.Queries.ProfitAndLossSummary;
using Domain.CustomEntities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Implementation.Queries.PostgreSQL;

namespace Persistence.Implementation.Repos
{
    internal class FinancialInsightsRepo : IFinancialInsightsRepo
    {
        private readonly AppDbContext _context;

        public FinancialInsightsRepo(AppDbContext context)
        {
            _context = context;
        }

        #region Insights Per Brand
        public async Task<List<FinancialChartDto>> GetFinancialChartDataByBrand(Guid brandId)
        {
            var oneTimeExpensesResult = (await _context.Set<AmountsByDateCustomEntity>()
                .FromSqlRaw(FinancialInsightsByBrandPostgreSqlQueries.OneTimeExpenseQueryByBrand.Replace("@brandId", $"'{brandId}'"))
                .ToListAsync()) ?? new List<AmountsByDateCustomEntity>();

            if (oneTimeExpensesResult.Any())
                oneTimeExpensesResult.ForEach(a => a.TotalAmount = a.TotalAmount * -1);


            var recurringExpensesResult = (await _context.Set<AmountsByDateCustomEntity>()
                .FromSqlRaw(FinancialInsightsByBrandPostgreSqlQueries.RecurringExpenseQueryByBrand.Replace("@brandId", $"'{brandId}'"))
                .ToListAsync()) ?? new List<AmountsByDateCustomEntity>();

            if (recurringExpensesResult.Any())
                recurringExpensesResult.ForEach(a => a.TotalAmount = a.TotalAmount * -1);

            var visitsIncomeResult = (await _context.Set<AmountsByDateCustomEntity>()
                .FromSqlRaw(FinancialInsightsByBrandPostgreSqlQueries.VisitsIncomeQueryByBrand.Replace("@brandId", $"'{brandId}'"))
                .ToListAsync()) ?? new List<AmountsByDateCustomEntity>();

            List<AmountsByDateCustomEntity> reservationsMonthlyIncomeResult = await GetMonthlyReservationsIncome(brandId);

            var reservationsIncomeResult = (await _context.Set<AmountsByDateCustomEntity>()
                .FromSqlRaw(FinancialInsightsByBrandPostgreSqlQueries.Reservations_Horly_Daily_IncomeQueryByBrand.Replace("@brandId", $"'{brandId}'"))
                .ToListAsync()) ?? new List<AmountsByDateCustomEntity>();

            var chartData = oneTimeExpensesResult
                .Concat(recurringExpensesResult)
                .Concat(visitsIncomeResult)
                .Concat(reservationsIncomeResult)
                .Concat(reservationsMonthlyIncomeResult)
                .GroupBy(a => new { a.Year, a.Month })
                .Select(a => new FinancialChartDto
                {
                    Month = a.Key.Month,
                    Year = a.Key.Year,
                    Amount = a.Sum(b => b.TotalAmount)
                })
                .OrderBy(dto => dto.Year)
                .ThenBy(dto => dto.Month)
                .ToList();

            return chartData;
        }

        public async Task<TopClientDto> GetMostPayingClientByBrand(Guid brandId)
        {
            var topClient = await _context.SharedAreaVisits
                .AsNoTracking()
                .Where(a => a.Branch.BrandId == brandId
                && (a.CheckOutStamp.HasValue && a.CheckOutStamp.Value.Date <= DateTimeOffset.UtcNow.Date))
                .GroupBy(a => a.ClientId)
                .Select(a => new
                {
                    ClientId = a.Key,
                    TotalAmount = a.Sum(a => a.TotalAmount)
                })
                .OrderByDescending(a => a.TotalAmount)
                .FirstOrDefaultAsync();

            var mostPayingClient = topClient is not null ? await _context.Clients
                .AsNoTracking()
                .FilterIf(topClient is not null, a => a.Id == topClient.ClientId)
                .Select(a => new TopClientDto
                {
                    Email = a.Email,
                    Name = a.Name,
                    PhoneNumber = a.MobileNumber,
                    TotalAmount = topClient.TotalAmount
                })
                .FirstOrDefaultAsync()
                : null;

            return mostPayingClient ?? new TopClientDto();
        }

        public async Task<MostProfitableAreaDto> GetMostProfitableAreaByBrand(Guid brandId)
        {
            var topSharedArea = await _context.SharedAreaVisits
                .AsNoTracking()
                .Where(a => a.Branch.BrandId == brandId
                && (a.CheckOutStamp.HasValue && a.CheckOutStamp.Value.Date <= DateTimeOffset.UtcNow.Date))
                .GroupBy(a => a.AreaId)
                .Select(group => new
                {
                    AreaId = group.Key,
                    TotalAmount = group.Sum(sv => sv.TotalAmount)
                })
                .OrderByDescending(result => result.TotalAmount)
                .FirstOrDefaultAsync();

            var mostProfitableAreaDto = topSharedArea is not null ? await _context.Areas
                    .AsNoTracking()
                    .FilterIf(topSharedArea is not null, a => a.Id == topSharedArea.AreaId)
                    .Select(a => new MostProfitableAreaDto
                    {
                        AreaName = a.Name,
                        BranchName = a.Branch.Name,
                        TotalAmount = topSharedArea.TotalAmount
                    })
                    .FirstOrDefaultAsync()
                    : null;

            return mostProfitableAreaDto ?? new MostProfitableAreaDto();
        }

        public async Task<decimal> GetOneTimeExpensesTotalByBrand(Guid brandId)
        {
            return await _context.OneTimeExpenses
                .AsNoTracking()
                .Where(a => (a.Branch.BrandId == brandId || a.BrandCostCategory.BrandId == brandId))
                .SumAsync(a => a.Amount);
        }

        public async Task<decimal> GetRecurringExpensesTotalByBrand(Guid brandId)
        {
            return await _context.RecurringExpenseAmounts
                .AsNoTracking()
                .Where(a => (a.RecurringExpense.Branch.BrandId == brandId || a.RecurringExpense.BrandCostCategory.BrandId == brandId))
                .SumAsync(a => a.Amount);
        }

        public async Task<decimal> GetSharedAreaIncomeByBrand(Guid brandId)
        {
            return await _context.SharedAreaVisits
                .AsNoTracking()
                .Where(a => a.Branch.BrandId == brandId
                && a.CheckOutStamp.HasValue)
                .SumAsync(a => a.TotalAmount);
        }

        public async Task<decimal> GetReservationsIncomeByBrand(Guid brandId)
        {
            return await _context.Reservations
                .AsNoTracking()
                .Where(a => a.Area.Branch.BrandId == brandId)
                .SumAsync(a => a.TotalAmount);
        }

        private async Task<List<AmountsByDateCustomEntity>> GetMonthlyReservationsIncome(Guid brandId)
        {
            var reservationsDetailsResult = await _context.Reservations
                .Where(a => a.Area.Branch.Brand.Id == brandId && a.IsMonthlyReservation)
                .Select(a => new Reservation
                {
                    TotalAmount = a.TotalAmount,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    IsDailyReservation = a.IsDailyReservation,
                    IsMonthlyReservation = a.IsMonthlyReservation
                })
                .ToListAsync();

            var reservationsMonthkyIncomeResult = new List<AmountsByDateCustomEntity>();


            foreach (var reservation in reservationsDetailsResult.Where(a => a.IsMonthlyReservation))
                reservationsMonthkyIncomeResult.AddRange(GetReservationsMonthsIncomeSummarized(reservation));

            reservationsMonthkyIncomeResult = reservationsMonthkyIncomeResult
                .GroupBy(a => new { a.Year, a.Month })
                .Select(a => new AmountsByDateCustomEntity
                {
                    Year = a.Key.Year,
                    Month = a.Key.Month,
                    TotalAmount = a.Sum(b => b.TotalAmount)
                })
                .ToList();

            return reservationsMonthkyIncomeResult ?? new List<AmountsByDateCustomEntity>();
        }
        #endregion

        #region Insights Per Brand Branch
        public async Task<List<FinancialChartDto>> GetFinancialChartDataByBrandBranch(Guid brandId, Guid branchId)
        {
            var oneTimeExpensesResult = (await _context.Set<AmountsByDateCustomEntity>()
                .FromSqlRaw(FinancialInsightsByBrandBranchPostgreSqlQueries.OneTimeExpenseQueryByBrandBranch.Replace("@brandId", $"'{brandId}'").Replace("@branchId", $"'{branchId}'"))
                .ToListAsync()) ?? new List<AmountsByDateCustomEntity>();

            if (oneTimeExpensesResult.Any())
                oneTimeExpensesResult.ForEach(a => a.TotalAmount = a.TotalAmount * -1);


            var recurringExpensesResult = (await _context.Set<AmountsByDateCustomEntity>()
                .FromSqlRaw(FinancialInsightsByBrandBranchPostgreSqlQueries.RecurringExpenseQueryByBrandBranch.Replace("@brandId", $"'{brandId}'").Replace("@branchId", $"'{branchId}'"))
                .ToListAsync()) ?? new List<AmountsByDateCustomEntity>();

            if (recurringExpensesResult.Any())
                recurringExpensesResult.ForEach(a => a.TotalAmount = a.TotalAmount * -1);

            var visitsIncomeResult = (await _context.Set<AmountsByDateCustomEntity>()
                .FromSqlRaw(FinancialInsightsByBrandBranchPostgreSqlQueries.VisitsIncomeQueryByBrandBranch.Replace("@brandId", $"'{brandId}'").Replace("@branchId", $"'{branchId}'"))
                .ToListAsync()) ?? new List<AmountsByDateCustomEntity>();

            List<AmountsByDateCustomEntity> reservationsMonthlyIncomeResult = await GetMonthlyReservationsIncome(brandId, branchId);

            var reservationsIncomeResult = (await _context.Set<AmountsByDateCustomEntity>()
                .FromSqlRaw(FinancialInsightsByBrandBranchPostgreSqlQueries.Reservations_Horly_Daily_IncomeQueryByBrandBranch.Replace("@brandId", $"'{brandId}'").Replace("@branchId", $"'{branchId}'"))
                .ToListAsync()) ?? new List<AmountsByDateCustomEntity>();

            var chartData = oneTimeExpensesResult
                .Concat(recurringExpensesResult)
                .Concat(visitsIncomeResult)
                .Concat(reservationsIncomeResult)
                .Concat(reservationsMonthlyIncomeResult)
                .GroupBy(a => new { a.Year, a.Month })
                .Select(a => new FinancialChartDto
                {
                    Month = a.Key.Month,
                    Year = a.Key.Year,
                    Amount = a.Sum(b => b.TotalAmount)
                })
                .OrderBy(dto => dto.Year)
                .ThenBy(dto => dto.Month)
                .ToList();

            return chartData;
        }

        private async Task<List<AmountsByDateCustomEntity>> GetMonthlyReservationsIncome(Guid brandId, Guid branchId)
        {
            var reservationsDetailsResult = await _context.Reservations
                .Where(a => a.Area.Branch.Id == branchId && a.Area.Branch.Brand.Id == brandId && a.IsMonthlyReservation)
                .Select(a => new Reservation
                {
                    TotalAmount = a.TotalAmount,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    IsDailyReservation = a.IsDailyReservation,
                    IsMonthlyReservation = a.IsMonthlyReservation
                })
                .ToListAsync();

            var reservationsMonthkyIncomeResult = new List<AmountsByDateCustomEntity>();


            foreach (var reservation in reservationsDetailsResult.Where(a => a.IsMonthlyReservation))
                reservationsMonthkyIncomeResult.AddRange(GetReservationsMonthsIncomeSummarized(reservation));

            reservationsMonthkyIncomeResult = reservationsMonthkyIncomeResult
                .GroupBy(a => new { a.Year, a.Month })
                .Select(a => new AmountsByDateCustomEntity
                {
                    Year = a.Key.Year,
                    Month = a.Key.Month,
                    TotalAmount = a.Sum(b => b.TotalAmount)
                })
                .ToList();

            return reservationsMonthkyIncomeResult ?? new List<AmountsByDateCustomEntity>();
        }

        private List<AmountsByDateCustomEntity> GetReservationsMonthsIncomeSummarized(Reservation reservation)
        {
            int numMonths = (reservation.EndDate.Year - reservation.StartDate.Year) * 12 + (reservation.EndDate.Month - reservation.StartDate.Month) + 1;

            decimal monthlyAmount = reservation.TotalAmount / numMonths;

            List<AmountsByDateCustomEntity> amountsByDate = new List<AmountsByDateCustomEntity>();

            var currentDate = reservation.StartDate;
            for (int i = 0; i < numMonths; i++)
            {
                amountsByDate.Add(new AmountsByDateCustomEntity
                {
                    Year = currentDate.Year,
                    Month = currentDate.Month,
                    TotalAmount = monthlyAmount
                });

                currentDate = currentDate.AddMonths(1);
            }

            var summarizedAmounts = amountsByDate
                .GroupBy(a => new { a.Year, a.Month })
                .Select(a => new AmountsByDateCustomEntity
                {
                    Year = a.Key.Year,
                    Month = a.Key.Month,
                    TotalAmount = a.Sum(b => b.TotalAmount)
                })
                .ToList();

            return summarizedAmounts;
        }

        public async Task<TopClientDto> GetMostPayingClientByBrandBranch(Guid brandId, Guid branchId)
        {
            var topClient = await _context.SharedAreaVisits
                .AsNoTracking()
                .Where(a => a.Branch.BrandId == brandId && a.Branch.Id == branchId
                && (a.CheckOutStamp.HasValue && a.CheckOutStamp.Value.Date <= DateTimeOffset.UtcNow.Date))
                .GroupBy(a => a.ClientId)
                .Select(a => new
                {
                    ClientId = a.Key,
                    TotalAmount = a.Sum(a => a.TotalAmount)
                })
                .OrderByDescending(a => a.TotalAmount)
                .FirstOrDefaultAsync();

            var mostPayingClient = topClient is not null ? await _context.Clients
                .AsNoTracking()
                .FilterIf(topClient is not null, a => a.Id == topClient.ClientId)
                .Select(a => new TopClientDto
                {
                    Email = a.Email,
                    Name = a.Name,
                    PhoneNumber = a.MobileNumber,
                    TotalAmount = topClient.TotalAmount
                })
                .FirstOrDefaultAsync()
                : null;

            return mostPayingClient ?? new TopClientDto();
        }

        public async Task<MostProfitableAreaDto> GetMostProfitableAreaByBrandBranch(Guid brandId, Guid branchId)
        {
            var topSharedArea = await _context.SharedAreaVisits
                .AsNoTracking()
                .Where(a => a.Branch.BrandId == brandId && a.Branch.Id == branchId
                && (a.CheckOutStamp.HasValue && a.CheckOutStamp.Value.Date <= DateTimeOffset.UtcNow.Date))
                .GroupBy(a => a.AreaId)
                .Select(group => new
                {
                    AreaId = group.Key,
                    TotalAmount = group.Sum(sv => sv.TotalAmount)
                })
                .OrderByDescending(result => result.TotalAmount)
                .FirstOrDefaultAsync();

            var mostProfitableAreaDto = topSharedArea is not null ? await _context.Areas
                    .AsNoTracking()
                    .FilterIf(topSharedArea is not null, a => a.Id == topSharedArea.AreaId)
                    .Select(a => new MostProfitableAreaDto
                    {
                        AreaName = a.Name,
                        BranchName = a.Branch.Name,
                        TotalAmount = topSharedArea.TotalAmount
                    })
                    .FirstOrDefaultAsync()
                    : null;

            return mostProfitableAreaDto ?? new MostProfitableAreaDto();
        }

        public async Task<decimal> GetOneTimeExpensesTotalByBrandBranch(Guid brandId, Guid branchId)
        {
            return await _context.OneTimeExpenses
                .AsNoTracking()
                .Where(a => (a.Branch.BrandId == brandId || a.BrandCostCategory.BrandId == brandId)
                && (a.BranchId.HasValue && a.Branch.Id == branchId))
                .SumAsync(a => a.Amount);
        }

        public async Task<decimal> GetRecurringExpensesTotalByBrandBranch(Guid brandId, Guid branchId)
        {
            return await _context.RecurringExpenseAmounts
                .AsNoTracking()
                .Where(a => (a.RecurringExpense.Branch.BrandId == brandId || a.RecurringExpense.BrandCostCategory.BrandId == brandId)
                && (a.RecurringExpense.BranchId.HasValue && a.RecurringExpense.Branch.Id == branchId))
                .SumAsync(a => a.Amount);
        }

        public async Task<decimal> GetSharedAreaIncomeByBrandBranch(Guid brandId, Guid branchId)
        {
            return await _context.SharedAreaVisits
                .AsNoTracking()
                .Where(a => a.Branch.BrandId == brandId
                && a.Branch.Id == branchId
                && a.CheckOutStamp.HasValue)
                .SumAsync(a => a.TotalAmount);
        }

        public async Task<decimal> GetReservationsIncomeByBrandBranch(Guid brandId, Guid branchId)
        {
            return await _context.Reservations
                .AsNoTracking()
                .Where(a => a.Area.Branch.BrandId == brandId && a.Area.Branch.Id == branchId)
                .SumAsync(a => a.TotalAmount);
        }
        #endregion
    }
}