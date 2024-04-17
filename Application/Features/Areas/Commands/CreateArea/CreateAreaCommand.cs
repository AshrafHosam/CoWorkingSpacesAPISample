using Application.Features.Areas.Common;
using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Areas.Commands.CreateArea
{
    public class CreateAreaCommand : IRequest<ApiResponse<CreateAreaCommandResponse>>
    {
        [Required]
        public string Name { get; set; }
        public int Capacity { get; set; }
        [Required]
        public Guid AreaTypeId { get; set; }
        [Required]
        public Guid BranchId { get; set; }
        public SharedAreaPricingDto? SharedAreaPricingDTO { get; set; } = null;
        public BookableAreaPricingDto? BookableAreaPricingDTO { get; set; } = null;
    }
}
