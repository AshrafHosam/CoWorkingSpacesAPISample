using Application.Features.Areas.Common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Areas.Commands.EditArea
{
    public class EditAreaCommandResponse
    {
        public Guid AreaId { get; set; }
        public string Name { get; set; }
        public Guid AreaTypeId { get; set; }
        public Guid BranchId { get; set; }
        public int Capacity { get; set; }

        public SharedAreaPricingDto? SharedAreaPricingDTO { get; set; } = null;
        public BookableAreaPricingDto? BookableAreaPricingDTO { get; set; } = null;
    }
}