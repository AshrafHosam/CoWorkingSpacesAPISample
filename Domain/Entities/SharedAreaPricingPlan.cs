using Domain.Common;

namespace Domain.Entities
{
    public class SharedAreaPricingPlan : BaseEntity
    {
        public decimal PricePerHour { get; set; }
        public bool IsFullDayApplicable { get; set; }
        public int? FullDayHours { get; set; }
    }
}
