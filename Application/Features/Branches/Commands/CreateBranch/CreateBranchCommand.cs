using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Branches.Commands.CreateBranch
{
    public class CreateBranchCommand : IRequest<ApiResponse<CreateBranchCommandResponse>>
    {
        [Required]
        public string Name { get; set; }
        public Guid BrandId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string LocationUrl { get; set; }
    }
}
