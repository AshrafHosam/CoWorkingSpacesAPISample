using Application.Features.BrandCosts.Commands.AddRecurringExpense;
using Domain.Entities;

namespace Application.Contracts.Strategies
{
    public interface IRecurringExpenseStrategy
    {
        RecurringExpense Execute(RecurringExpenseStrategyModel model);
    }
}
