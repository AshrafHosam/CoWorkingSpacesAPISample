using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class SharedAreaVisit : BaseEntity
    {
        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }
        [ForeignKey(nameof(Branch))]
        public Guid BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        [ForeignKey(nameof(Area))]
        public Guid AreaId { get; set; }
        public virtual Area Area { get; set; }
        public DateTimeOffset CheckInStamp { get; set; }
        public DateTimeOffset? CheckOutStamp { get; set; }
        public virtual List<CustomService> CustomServices { get; set; }
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; }
    }
}
