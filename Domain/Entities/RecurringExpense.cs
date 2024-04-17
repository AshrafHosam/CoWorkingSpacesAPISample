using Domain.Common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class RecurringExpense : BaseEntity
    {
        public string Name { get; set; }
        public RecurringTimeSpanUnitEnum RecurringTimeSpanUnit { get; set; }

        [ForeignKey(nameof(BrandCostCategory))]
        public Guid BrandCostCategoryId { get; set; }
        public virtual BrandCostCategory BrandCostCategory { get; set; }

        [ForeignKey(nameof(Branch))]
        public Guid? BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        public virtual List<RecurringExpenseAmount> RecurringExpenseAmounts { get; set; }
    }
}
