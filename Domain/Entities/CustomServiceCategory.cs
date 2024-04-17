using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class CustomServiceCategory : BaseEntity
    {
        [ForeignKey(nameof(Brand))]
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }

        public virtual List<CustomService> CustomServices { get; set; }
        public string Name { get; set; }
    }
}
