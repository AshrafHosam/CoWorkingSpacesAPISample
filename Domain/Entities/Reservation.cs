using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Reservation : BaseEntity
    {
        [ForeignKey(nameof(Area))]
        public Guid AreaId { get; set; }
        public virtual Area Area { get; set; }

        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }

        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public string Notes { get; set; }

        public string Name { get; set; }
        public decimal TotalAmount { get; set; }

        public bool IsHourlyReservation { get; set; } = false;
        public bool IsDailyReservation { get; set; } = false;
        public bool IsMonthlyReservation { get; set; } = false;
    }
}
