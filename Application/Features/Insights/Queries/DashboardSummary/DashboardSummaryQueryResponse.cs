namespace Application.Features.Insights.Queries.DashboardSummary
{
    public class DashboardSummaryQueryResponse
    {
        public decimal TotalNumberOfClients { get; set; }
        public decimal AvgHours { get; set; }
        public decimal CheckInNo { get; set; }
        public decimal CurrentOccupacy { get; set; }
    }
}