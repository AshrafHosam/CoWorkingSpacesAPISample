using Domain.Entities;

namespace Application.Contracts.Repos
{
    public interface IRecurringExpenseRepo : IBaseRepo<RecurringExpense>
    {
        Task<List<RecurringExpense>> GetCategoryRecurringExpenses(Guid brandId, Guid categoryId, int page, int pageSize);
        Task<long> GetCategoryRecurringExpensesCount(Guid brandId, Guid categoryId);
    }
}
