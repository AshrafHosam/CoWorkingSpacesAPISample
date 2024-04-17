using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Reservations.Queries.GetReservation
{
    public class GetReservationQuery : IRequest<ApiResponse<GetReservationQueryResponse>>
    {
        [Required]
        public Guid Id { get; set; }
    }
}
