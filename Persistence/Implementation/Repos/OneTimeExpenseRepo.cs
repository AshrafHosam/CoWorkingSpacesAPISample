using Application.Contracts.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Implementation.Repos
{
    internal class OneTimeExpenseRepo : BaseRepo<OneTimeExpense>, IOneTimeExpenseRepo
    {
        public OneTimeExpenseRepo(AppDbContext context) : base(context)
        {
        }
        public async Task<List<OneTimeExpense>> GetCategoryOneTimeExpenses(Guid brandId, Guid? branchId, Guid? categoryId, int page, int pageSize)
        {
            return await _context.OneTimeExpenses
                .AsNoTracking()
                .FilterIf(categoryId.HasValue, a => a.BrandCostCategoryId == categoryId.Value)
                .FilterIf(branchId.HasValue, a => a.BranchId == branchId.Value)
                .Where(a => a.BrandCostCategory.BrandId == brandId)
                .OrderBy(a => a.TransactionExecutionDate)
                .Skip(page - 1)
                .Take(page * pageSize)
                .ToListAsync();
        }

        public async Task<List<OneTimeExpense>> GetCategoryOneTimeExpenses(Guid brandId, Guid? branchId, Guid? categoryId, int page, int pageSize, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            return await _context.OneTimeExpenses
                .AsNoTracking()
                .FilterIf(categoryId.HasValue, a => a.BrandCostCategoryId == categoryId.Value)
                .FilterIf(branchId.HasValue, a => a.BranchId == branchId.Value)
                .Where(a => a.BrandCostCategory.BrandId == brandId
                && a.TransactionExecutionDate.DateTime >= fromDate.DateTime
                && a.TransactionExecutionDate.DateTime <= toDate.DateTime)
                .OrderBy(a => a.TransactionExecutionDate)
                .Skip(page - 1)
                .Take(page * pageSize)
                .ToListAsync();
        }

        public async Task<long> GetCategoryOneTimeExpensesCount(Guid brandId, Guid? branchId, Guid? categoryId)
        {
            return await _context.OneTimeExpenses
                .AsNoTracking()
                .FilterIf(categoryId.HasValue, a => a.BrandCostCategoryId == categoryId.Value)
                .FilterIf(branchId.HasValue, a => a.BranchId == branchId.Value)
                .CountAsync(a => a.BrandCostCategory.BrandId == brandId);
        }

        public async Task<long> GetCategoryOneTimeExpensesCount(Guid brandId, Guid? branchId, Guid? categoryId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            return await _context.OneTimeExpenses
                .AsNoTracking()
                .FilterIf(categoryId.HasValue, a => a.BrandCostCategoryId == categoryId.Value)
                .FilterIf(branchId.HasValue, a => a.BranchId == branchId.Value)
                .CountAsync(a => a.BrandCostCategory.BrandId == brandId
                && a.TransactionExecutionDate.DateTime >= fromDate.DateTime
                && a.TransactionExecutionDate.DateTime <= toDate.DateTime);
        }
    }
}
