using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Branches.Queries.GetBranchAreas
{
    public class GetBranchAreasQuery : IRequest<ApiResponse<List<GetBranchAreasQueryResponse>>>
    {
        [Required]
        public Guid BranchId { get; set; }
    }
}
