using Domain.Common;

namespace Domain.Entities
{
    public class BookableAreaPricingPlan : BaseEntity
    {
        public decimal? PricePerHour { get; set; }
        public decimal? PricePerDay { get; set; }
        public decimal? PricePerMonth { get; set; }
    }
}
