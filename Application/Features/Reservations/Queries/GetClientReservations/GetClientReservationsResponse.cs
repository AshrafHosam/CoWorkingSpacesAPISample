namespace Application.Features.Reservations.Queries.GetClientReservations
{
    public class GetClientReservationsQueryResponse
    {
        public Guid Id { get; set; }
        public Guid AreaId { get; set; }
        public Guid ClientId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Notes { get; set; }
        public string Name { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsAllDay { get; set; }
    }
}