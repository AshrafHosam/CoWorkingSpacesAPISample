using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommand : IRequest<ApiResponse<DeleteReservationCommandResponse>>
    {
        [Required]
        public Guid Id { get; set; }
    }
}
