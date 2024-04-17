using Domain.Entities;

namespace Application.Contracts.Repos
{
    public interface IOneTimeExpenseRepo : IBaseRepo<OneTimeExpense>
    {
        Task<List<OneTimeExpense>> GetCategoryOneTimeExpenses(Guid brandId, Guid? branchId, Guid? categoryId, int page, int pageSize);
        Task<List<OneTimeExpense>> GetCategoryOneTimeExpenses(Guid brandId, Guid? branchId, Guid? categoryId, int page, int pageSize, DateTimeOffset fromDate, DateTimeOffset toDate);
        Task<long> GetCategoryOneTimeExpensesCount(Guid brandId, Guid? branchId, Guid? categoryId);
        Task<long> GetCategoryOneTimeExpensesCount(Guid brandId, Guid? branchId, Guid? categoryId, DateTimeOffset fromDate, DateTimeOffset toDate);
    }
}
