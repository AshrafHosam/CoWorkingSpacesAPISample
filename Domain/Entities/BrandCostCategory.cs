using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BrandCostCategory : BaseEntity
    {
        public string Name { get; set; }

        [ForeignKey(nameof(Brand))]
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual List<OneTimeExpense> Expenses { get; set; }
    }
}
