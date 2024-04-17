using Application.Contracts.Strategies;
using Application.Features.BrandCosts.Commands.AddRecurringExpense;

namespace Application.Contracts.Factories
{
    public interface IStrategyFactory
    {
        IRecurringExpenseStrategy GetRecurringExpenseStrategy(AddRecurringExpenseCommand request);
    }
}
