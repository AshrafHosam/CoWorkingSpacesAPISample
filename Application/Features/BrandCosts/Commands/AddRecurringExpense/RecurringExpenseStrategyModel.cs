using Domain.Enums;

namespace Application.Features.BrandCosts.Commands.AddRecurringExpense
{
    public class RecurringExpenseStrategyModel
    {
        public Guid CategoryId { get; set; }
        public Guid? BranchId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int NumberOfOccurs { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public RecurringTimeSpanUnitEnum RecurringTimeSpanUnit { get; set; }
        public List<DateTimeOffset> SelectedDates { get; set; }
    }
}
