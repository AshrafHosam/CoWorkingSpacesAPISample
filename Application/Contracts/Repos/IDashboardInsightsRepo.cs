
namespace Application.Contracts.Repos
{
    public interface IDashboardInsightsRepo
    {
        Task<decimal> GetAverageHoursCount(Guid brandId);
        Task<decimal> GetCheckInCount(Guid brandId);
        Task<decimal> GetClientsCount(Guid brandId);
        Task<decimal> GetCurrentOccupacy(Guid brandId);
    }
}
