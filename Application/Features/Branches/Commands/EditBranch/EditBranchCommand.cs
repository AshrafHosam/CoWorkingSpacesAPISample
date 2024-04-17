using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Branches.Commands.EditBranch
{
    public class EditBranchCommand : IRequest<ApiResponse<EditBranchCommandResponse>>
    {
        [Required]
        public Guid BranchId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid BrandId { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string LocationUrl { get; set; }
    }
}
