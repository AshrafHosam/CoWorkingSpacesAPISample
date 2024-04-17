using Application.Features.Clients.Queries.GetClient;

namespace Application.Features.Reservations.Queries.GetAllReservations
{
    public class GetAllReservationsQueryResponse
    {
        public Guid Id { get; set; }
        public Guid AreaId { get; set; }
        public Guid ClientId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public string Notes { get; set; }

        public decimal TotalAmount { get; set; }
        public string Name { get; set; }
        public bool IsAllDay { get; set; }
        public bool IsHourlyReservation { get; set; } = false;
        public bool IsDailyReservation { get; set; } = false;
        public bool IsMonthlyReservation { get; set; } = false;

        public ClientDto Client { get; set; } = new ClientDto();
    }
}