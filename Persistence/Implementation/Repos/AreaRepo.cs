using Application.Contracts.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Implementation.Repos
{
    public class AreaRepo : BaseRepo<Area>, IAreaRepo
    {
        public AreaRepo(AppDbContext context) : base(context)
        {
        }

        public async new Task DeleteAsync(Area area)
        {
            if (area.BookableAreaPricingPlanModel is not null)
                _context.Remove(area.BookableAreaPricingPlanModel);

            if (area.SharedAreaPricingPlanModel is not null)
                _context.Remove(area.SharedAreaPricingPlanModel);

            _context.Remove(area);

            await _context.SaveChangesAsync();
        }

        public async new Task<Area> UpdateAsync(Area area)
        {
            if (area.BookableAreaPricingPlanModel is not null)
                _context.Update(area.BookableAreaPricingPlanModel);

            if (area.SharedAreaPricingPlanModel is not null)
                _context.Update(area.SharedAreaPricingPlanModel);

            _context.Update(area);

            await _context.SaveChangesAsync();

            return area;
        }
        public async Task<Area> GetAreaPricingPlansIncluded(Guid AreaId)
        {
            return await _context.Areas
                .AsNoTracking()
                .Include(a => a.SharedAreaPricingPlanModel)
                .Include(a => a.BookableAreaPricingPlanModel)
                .Include(a => a.AreaType)
                .FirstOrDefaultAsync(a => a.Id == AreaId);
        }

        public async Task<Area> GetArea_PricingPlans_Brand_Included(Guid AreaId)
        {
            return await _context.Areas
                .AsNoTracking()
                .Include(a => a.SharedAreaPricingPlanModel)
                .Include(a => a.BookableAreaPricingPlanModel)
                .Include(a => a.Branch)
                .FirstOrDefaultAsync(a => a.Id == AreaId);
        }

        public async Task<List<Area>> GetAreasByBranch(Guid branchId)
        {
            return await _context.Areas
                .AsNoTracking()
                .Include(a => a.AreaType)
                .Include(a => a.SharedAreaPricingPlanModel)
                .Include(a => a.BookableAreaPricingPlanModel)
                .Where(a => a.BranchId == branchId)
                .ToListAsync();
        }

        public async Task<List<AreaType>> GetAreaTypesAsync()
        {
            return await _context.AreaTypes
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<bool> IsCreateReservationAreaCriteriaValid(Guid areaId, bool isHourlyReservation, bool isDailyReservation, bool isMonthlyReservation)
        {
            return await _context.Areas
            .Where(x => x.Id == areaId)
            .FilterIf(isMonthlyReservation, x => x.BookableAreaPricingPlanModel.PricePerMonth.HasValue)
            .FilterIf(isDailyReservation, x => x.BookableAreaPricingPlanModel.PricePerDay.HasValue)
            .FilterIf(isHourlyReservation, x => x.BookableAreaPricingPlanModel.PricePerHour.HasValue)
            .AnyAsync();
        }
    }
}
