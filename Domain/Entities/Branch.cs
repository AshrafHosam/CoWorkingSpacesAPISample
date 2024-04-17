using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string LocationUrl { get; set; }
        [ForeignKey(nameof(Brand))]
        public Guid BrandId { get; set; }
        public virtual IEnumerable<Area> Areas { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
