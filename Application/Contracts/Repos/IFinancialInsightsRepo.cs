using Application.Features.Insights.Queries.ProfitAndLossSummary;

namespace Application.Contracts.Repos
{
    public interface IFinancialInsightsRepo
    {
        Task<List<FinancialChartDto>> GetFinancialChartDataByBrand(Guid brandId);
        Task<List<FinancialChartDto>> GetFinancialChartDataByBrandBranch(Guid brandId, Guid branchId);
        Task<TopClientDto> GetMostPayingClientByBrand(Guid brandId);
        Task<TopClientDto> GetMostPayingClientByBrandBranch(Guid brandId, Guid branchId);
        Task<MostProfitableAreaDto> GetMostProfitableAreaByBrand(Guid brandId);
        Task<MostProfitableAreaDto> GetMostProfitableAreaByBrandBranch(Guid brandId, Guid branchId);
        Task<decimal> GetOneTimeExpensesTotalByBrand(Guid brandId);
        Task<decimal> GetOneTimeExpensesTotalByBrandBranch(Guid brandId, Guid branchId);
        Task<decimal> GetRecurringExpensesTotalByBrand(Guid brandId);
        Task<decimal> GetRecurringExpensesTotalByBrandBranch(Guid brandId, Guid branchId);
        Task<decimal> GetReservationsIncomeByBrand(Guid brandId);
        Task<decimal> GetReservationsIncomeByBrandBranch(Guid brandId, Guid branchId);
        Task<decimal> GetSharedAreaIncomeByBrand(Guid brandId);
        Task<decimal> GetSharedAreaIncomeByBrandBranch(Guid brandId, Guid branchId);
    }
}
