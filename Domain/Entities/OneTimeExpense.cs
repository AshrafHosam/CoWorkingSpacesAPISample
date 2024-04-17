using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class OneTimeExpense : BaseEntity
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset TransactionExecutionDate { get; set; }

        [ForeignKey(nameof(BrandCostCategory))]
        public Guid BrandCostCategoryId { get; set; }
        public virtual BrandCostCategory BrandCostCategory { get; set; }

        [ForeignKey(nameof(Branch))]
        public Guid? BranchId { get; set; }
        public virtual Branch Branch { get; set; }
    }
}