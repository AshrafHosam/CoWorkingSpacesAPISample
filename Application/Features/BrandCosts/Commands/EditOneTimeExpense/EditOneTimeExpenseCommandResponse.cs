namespace Application.Features.BrandCosts.Commands.EditOneTimeExpense
{
    public class EditOneTimeExpenseCommandResponse
    {
        public Guid ExpenseId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset ExecutionDate { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? BranchId { get; set; }
    }
}