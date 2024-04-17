using Application.Contracts.Strategies;
using Application.Features.BrandCosts.Commands.AddRecurringExpense;
using Domain.Entities;

namespace Persistence.Implementation.Strategies.RecurringExpenses
{
    public class WeeklyStrategy : IRecurringExpenseStrategy
    {
        public RecurringExpense Execute(RecurringExpenseStrategyModel model)
        {
            var recurringExpense = new RecurringExpense
            {
                Name = model.Name,
                BrandCostCategoryId = model.CategoryId,
                BranchId = model.BranchId,
                RecurringTimeSpanUnit = model.RecurringTimeSpanUnit,
                RecurringExpenseAmounts = new List<RecurringExpenseAmount>()
            };
            for (int i = 0; i < model.NumberOfOccurs; i++)
            {
                recurringExpense.RecurringExpenseAmounts.Add(new RecurringExpenseAmount
                {
                    Amount = model.Amount,
                    TransactionExecutionDate = model.StartDate.AddDays(i * 7)
                });
            }

            return recurringExpense;
        }
    }
}
