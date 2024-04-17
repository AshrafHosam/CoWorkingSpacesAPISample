using Domain.Entities;

namespace Application.Contracts.Repos
{
    public interface IAreaRepo : IBaseRepo<Area>
    {
        Task<List<AreaType>> GetAreaTypesAsync();
        Task<Area> GetAreaPricingPlansIncluded(Guid AreaId);
        Task<List<Area>> GetAreasByBranch(Guid branchId);
        Task<bool> IsCreateReservationAreaCriteriaValid(Guid areaId, bool isHourlyReservation, bool isDailyReservation, bool isMonthlyReservation);
        Task<Area> GetArea_PricingPlans_Brand_Included(Guid AreaId);
    }
}
