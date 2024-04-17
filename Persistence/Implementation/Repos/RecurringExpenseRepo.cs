using Application.Contracts.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Implementation.Repos
{
    internal class RecurringExpenseRepo : BaseRepo<RecurringExpense>, IRecurringExpenseRepo
    {
        public RecurringExpenseRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<List<RecurringExpense>> GetCategoryRecurringExpenses(Guid brandId, Guid categoryId, int page, int pageSize)
        {
            return await _context.RecurringExpenses
                .AsNoTracking()
                .Include(a => a.RecurringExpenseAmounts)
                .Where(a => a.BrandCostCategoryId == categoryId && a.BrandCostCategory.BrandId == brandId)
                .OrderBy(a => a.CreatedDate)
                .Skip(page - 1)
                .Take(page * pageSize)
                .ToListAsync();
        }

        public async Task<long> GetCategoryRecurringExpensesCount(Guid brandId, Guid categoryId)
        {
            return await _context.RecurringExpenses
                .AsNoTracking()
                .CountAsync(a => a.BrandCostCategoryId == categoryId && a.BrandCostCategory.BrandId == brandId);
        }
    }
}
