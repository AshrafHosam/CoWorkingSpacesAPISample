using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Issentialz.Application.Features.Visits.Queries.GetCheckedInClients
{
    public class GetCheckedInClientsQuery : IRequest<ApiResponse<List<GetCheckedInClientsQueryResponse>>>
    {
        [Required]
        public Guid BranchId { get; set; }
    }
}
