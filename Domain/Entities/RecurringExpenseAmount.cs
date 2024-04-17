using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class RecurringExpenseAmount : BaseEntity
    {
        public decimal Amount { get; set; }
        public DateTimeOffset TransactionExecutionDate { get; set; }

        [ForeignKey(nameof(RecurringExpense))]
        public Guid RecurringExpenseId { get; set; }
        public virtual RecurringExpense RecurringExpense { get; set; }
    }
}
