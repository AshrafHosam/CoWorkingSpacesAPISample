namespace Application.Features.Reservations.Commands.EditReservation
{
    public class EditReservationCommandResponse
    {
        public bool IsSuccess { get; set; } = false;
        public Guid ReservationId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}