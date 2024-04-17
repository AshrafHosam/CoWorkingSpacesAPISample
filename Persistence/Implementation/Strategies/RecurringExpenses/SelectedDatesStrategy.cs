using Application.Contracts.Strategies;
using Application.Features.BrandCosts.Commands.AddRecurringExpense;
using Domain.Entities;

namespace Persistence.Implementation.Strategies.RecurringExpenses
{
    public class SelectedDatesStrategy : IRecurringExpenseStrategy
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
            foreach (var transactionDate in model.SelectedDates)
            {
                recurringExpense.RecurringExpenseAmounts.Add(new RecurringExpenseAmount
                {
                    Amount = model.Amount,
                    TransactionExecutionDate = transactionDate
                });
            }

            return recurringExpense;
        }
    }
}
