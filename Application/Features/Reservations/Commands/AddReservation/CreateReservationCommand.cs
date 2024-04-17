using Application.Response;
using MediatR;

namespace Application.Features.Reservations.Commands.AddReservation
{
    public class CreateReservationCommand : IRequest<ApiResponse<CreateReservationCommandResponse>>
    {
        public Guid AreaId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Notes { get; set; }
        public string Name { get; set; }

        public bool IsHourlyReservation { get; set; } = false;
        public bool IsDailyReservation { get; set; } = false;
        public bool IsMonthlyReservation { get; set; } = false;
    }
}
