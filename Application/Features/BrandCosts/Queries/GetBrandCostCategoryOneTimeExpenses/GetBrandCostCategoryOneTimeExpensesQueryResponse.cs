namespace Application.Features.BrandCosts.Queries.GetBrandCostCategoryOneTimeExpenses
{
    public class GetBrandCostCategoryOneTimeExpensesQueryResponse
    {
        public long TotalCount { get; set; }
        public List<OneTimeExpense> OneTimeExpenses { get; set; } = new List<OneTimeExpense>();
    }
    public class OneTimeExpense
    {
        public Guid ExpenseId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset ExecutionDate { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? BranchId { get; set; }
    }
}