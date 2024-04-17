using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Reservations.Commands.EditReservation
{
    public class EditReservationCommand : IRequest<ApiResponse<EditReservationCommandResponse>>
    {
        [Required]
        public Guid ReservationId { get; set; }
        [Required]
        public Guid AreaId { get; set; }
        [Required]
        public Guid ClientId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public string Notes { get; set; }
        public string Name { get; set; }

        public bool IsHourlyReservation { get; set; } = false;
        public bool IsDailyReservation { get; set; } = false;
        public bool IsMonthlyReservation { get; set; } = false;
    }
}
