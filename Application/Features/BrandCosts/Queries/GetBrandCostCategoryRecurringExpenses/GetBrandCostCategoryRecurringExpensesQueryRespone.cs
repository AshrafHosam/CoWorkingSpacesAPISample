namespace Application.Features.BrandCosts.Queries.GetBrandCostCategoryRecurringExpenses
{
    public class GetBrandCostCategoryRecurringExpensesQueryResponse
    {
        public long TotalCount { get; set; }
        public List<RecurringExpenseDTO> RecurringExpenses { get; set; } = new List<RecurringExpenseDTO>();
    }

    public class RecurringExpenseDTO
    {
        public Guid ExpenseId { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? BranchId { get; set; }
        public string RecurringTimeSpanUnit { get; set; }
        public int NumberOfOccurs { get; set; }

        public List<RecurringExpenseAmountDTO> RecurringExpenseAmounts { get; set; } = new List<RecurringExpenseAmountDTO>();
    }

    public class RecurringExpenseAmountDTO
    {
        public Guid ExpenseAmountId { get; set; }
        public DateTimeOffset ExecutionDate { get; set; }
        public decimal Amount { get; set; }
    }
}