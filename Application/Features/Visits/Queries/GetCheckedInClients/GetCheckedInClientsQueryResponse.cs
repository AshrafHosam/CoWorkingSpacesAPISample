namespace Issentialz.Application.Features.Visits.Queries.GetCheckedInClients
{
    public class GetCheckedInClientsQueryResponse
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientMobileNumber { get; set; }
        public string ClientEmail { get; set; }
        public DateTimeOffset CheckInStamp { get; set; }
        public Guid VisitId { get; set; }
        public Guid AreaId { get; set; }
        public string AreaName { get; set; }
        public List<ConsumedService> ConsumedServices { get; set; } = new List<ConsumedService>();
    }

    public class ConsumedService
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
    }
}
