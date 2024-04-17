using Application.Contracts.Factories;
using Application.Contracts.Strategies;
using Application.Features.BrandCosts.Commands.AddRecurringExpense;
using Persistence.Implementation.Strategies.RecurringExpenses;

namespace Persistence.Implementation.Factories
{
    internal class StrategyFactory : IStrategyFactory
    {
        public IRecurringExpenseStrategy GetRecurringExpenseStrategy(AddRecurringExpenseCommand request)
        {
            switch (request)
            {
                case { RecurringTimeSpanUnit: Domain.Enums.RecurringTimeSpanUnitEnum.Week }:
                    return new WeeklyStrategy();

                case { RecurringTimeSpanUnit: Domain.Enums.RecurringTimeSpanUnitEnum.CustomDates }:
                    return new SelectedDatesStrategy();

                case { RecurringTimeSpanUnit: Domain.Enums.RecurringTimeSpanUnitEnum.Month }:
                    return new MonthlyStrategy();

                case { RecurringTimeSpanUnit: Domain.Enums.RecurringTimeSpanUnitEnum.Year }:
                    return new AnnualStrategy();

                default:
                    return null;
            }
        }
    }
}
