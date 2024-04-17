using Application.Features.Areas.Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Branches.Queries.GetBranchAreas
{
    public class GetBranchAreasQueryResponse
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