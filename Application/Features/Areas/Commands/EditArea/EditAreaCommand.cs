using Application.Features.Areas.Common;
using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Areas.Commands.EditArea
{
    public class EditAreaCommand : IRequest<ApiResponse<EditAreaCommandResponse>>
    {
        [Required]
        public Guid AreaId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid AreaTypeId { get; set; }
        [Required]
        public Guid BranchId { get; set; }
        public int Capacity { get; set; }

        public SharedAreaPricingDto? SharedAreaPricingDTO { get; set; } = null;
        public BookableAreaPricingDto? BookableAreaPricingDTO { get; set; } = null;
    }
}
