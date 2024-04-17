using Application.Features.Insights.Queries.DashboardSummary;
using Application.Features.Insights.Queries.ProfitAndLossSummary;

namespace Application.Contracts.Services
{
    public interface IInsightsService
    {
        Task<DashboardSummaryQueryResponse> GetBrandDashboardSummary(Guid brandId);
        Task<ProfitAndLossSummaryQueryResponse> GetBrandBranchProfitAndLossSummary(Guid brandId,Guid branchId);
        Task<ProfitAndLossSummaryQueryResponse> GetBrandProfitAndLossSummary(Guid brandId);
    }
}
