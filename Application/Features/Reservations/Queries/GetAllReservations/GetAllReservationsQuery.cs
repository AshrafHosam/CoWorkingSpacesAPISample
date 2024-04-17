using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Reservations.Queries.GetAllReservations
{
    public class GetAllReservationsQuery : IRequest<ApiResponse<List<GetAllReservationsQueryResponse>>>
    {
        [Required]
        public Guid BranchId { get; set; }
        [Required]
        public DateTimeOffset FromDate { get; set; }
        [Required]
        public DateTimeOffset ToDate { get; set; }
    }
}
