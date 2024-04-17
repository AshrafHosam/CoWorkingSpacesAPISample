using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Branches.Commands.DeleteBranch
{
    public class DeleteBranchCommand : IRequest<ApiResponse<DeleteBranchCommandResponse>>
    {
        [Required]
        public Guid BrandId { get; set; }
        [Required]
        public Guid BranchId { get; set; }
    }
}
