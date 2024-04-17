namespace Application.Features.Reservations.Commands.AddReservation
{
    public class CreateReservationCommandResponse
    {
        public Guid ReservationId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}