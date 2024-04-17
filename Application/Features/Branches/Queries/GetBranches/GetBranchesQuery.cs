using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Branches.Queries.GetBranches
{
    public class GetBranchesQuery : IRequest<ApiResponse<List<GetBranchesQueryResponse>>>
    {
        [Required]
        public Guid BrandId { get; set; }
    }
}
