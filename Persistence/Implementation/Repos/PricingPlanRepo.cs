using Application.Contracts.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Implementation.Repos
{
    public class PricingPlanRepo : BaseRepo<SharedAreaPricingPlan>, IPricingPlanRepo
    {
        public PricingPlanRepo(AppDbContext context) : base(context)
        {
        }

        public async Task DeletePricingPlans(List<SharedAreaPricingPlan> pricingPlans)
        {
            _context.SharedPricingPlans.RemoveRange(pricingPlans);

            await _context.SaveChangesAsync();
        }

        public async Task<List<SharedAreaPricingPlan>> GetPlansByIds(List<Guid> pricingPlanIds)
        {
            return await _context.SharedPricingPlans
                .Where(a => pricingPlanIds.Contains(a.Id))
                .ToListAsync();
        }
    }
}
