namespace Application.Features.Areas.Common
{
    public class SharedAreaPricingDto
    {
        public decimal PricePerHour { get; set; } = 0;
        public bool IsFullDayApplicable { get; set; }
        public int? FullDayHours { get; set; }
    }

    public class BookableAreaPricingDto
    {
        public decimal? PricePerHour { get; set; }
        public decimal? PricePerDay { get; set; }
        public decimal? PricePerMonth { get; set; }
    }
}
