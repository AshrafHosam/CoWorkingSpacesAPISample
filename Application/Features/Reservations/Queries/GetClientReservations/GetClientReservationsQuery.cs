using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Reservations.Queries.GetClientReservations
{
    public class GetClientReservationsQuery : IRequest<ApiResponse<List<GetClientReservationsQueryResponse>>>
    {
        [Required]
        public Guid ClientId { get; set; }
        [Required]
        public Guid BranchId { get; set; }
    }
}
