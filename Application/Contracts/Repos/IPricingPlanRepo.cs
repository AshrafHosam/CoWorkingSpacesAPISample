using Domain.Entities;

namespace Application.Contracts.Repos
{
    public interface IPricingPlanRepo : IBaseRepo<SharedAreaPricingPlan>
    {
        Task<List<SharedAreaPricingPlan>> GetPlansByIds(List<Guid> pricingPlanIds);

        Task DeletePricingPlans(List<SharedAreaPricingPlan> pricingPlans);
    }
}
