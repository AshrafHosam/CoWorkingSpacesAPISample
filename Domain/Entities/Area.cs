using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Area : BaseEntity
    {
        public string Name { get; set; }
        public int Capacity { get; set; }

        [ForeignKey(nameof(BookableAreaPricingPlanModel))]
        public Guid? BookableAreaPricingPlanId { get; set; }
        public virtual BookableAreaPricingPlan? BookableAreaPricingPlanModel { get; set; }


        [ForeignKey(nameof(SharedAreaPricingPlanModel))]
        public Guid? SharedAreaPricingPlanId { get; set; }
        public virtual SharedAreaPricingPlan? SharedAreaPricingPlanModel { get; set; }


        [ForeignKey(nameof(AreaType))]
        public Guid AreaTypeId { get; set; }
        public virtual AreaType AreaType { get; set; }


        [ForeignKey(nameof(Branch))]
        public Guid BranchId { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
