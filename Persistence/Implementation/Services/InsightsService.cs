using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Features.Insights.Queries.DashboardSummary;
using Application.Features.Insights.Queries.ProfitAndLossSummary;

namespace Persistence.Implementation.Services
{
    internal class InsightsService : IInsightsService
    {
        private readonly IFinancialInsightsRepo _financialInsightsRepo;
        private readonly IDashboardInsightsRepo _dashboardInsightsRepo;
        public InsightsService(IFinancialInsightsRepo financialInsightsRepo, IDashboardInsightsRepo dashboardInsightsRepo)
        {
            _financialInsightsRepo = financialInsightsRepo;
            _dashboardInsightsRepo = dashboardInsightsRepo;
        }

        public async Task<ProfitAndLossSummaryQueryResponse> GetBrandBranchProfitAndLossSummary(Guid brandId, Guid branchId)
        {
            decimal totalIncomeFromShared = (await _financialInsightsRepo.GetSharedAreaIncomeByBrandBranch(brandId, branchId)) + (await _financialInsightsRepo.GetReservationsIncomeByBrandBranch(brandId, branchId));

            decimal totalOneTimeExpenses = await _financialInsightsRepo.GetOneTimeExpensesTotalByBrandBranch(brandId, branchId);

            decimal totalRecurringExpenses = await _financialInsightsRepo.GetRecurringExpensesTotalByBrandBranch(brandId, branchId);

            MostProfitableAreaDto mostProfitableAreaDto = await _financialInsightsRepo.GetMostProfitableAreaByBrandBranch(brandId, branchId);

            TopClientDto mostPayingClient = await _financialInsightsRepo.GetMostPayingClientByBrandBranch(brandId, branchId);

            List<FinancialChartDto> chartData = await _financialInsightsRepo.GetFinancialChartDataByBrandBranch(brandId, branchId);

            return new ProfitAndLossSummaryQueryResponse
            {
                MostProfitableArea = mostProfitableAreaDto,
                TopClient = mostPayingClient,
                TotalExpenses = totalOneTimeExpenses + totalRecurringExpenses,
                TotalIncome = totalIncomeFromShared,
                FinancialChartData = chartData
            };
        }

        public async Task<ProfitAndLossSummaryQueryResponse> GetBrandProfitAndLossSummary(Guid brandId)
        {
            decimal totalIncomeFromShared = (await _financialInsightsRepo.GetSharedAreaIncomeByBrand(brandId)) + (await _financialInsightsRepo.GetReservationsIncomeByBrand(brandId));

            decimal totalOneTimeExpenses = await _financialInsightsRepo.GetOneTimeExpensesTotalByBrand(brandId);

            decimal totalRecurringExpenses = await _financialInsightsRepo.GetRecurringExpensesTotalByBrand(brandId);

            MostProfitableAreaDto mostProfitableAreaDto = await _financialInsightsRepo.GetMostProfitableAreaByBrand(brandId);

            TopClientDto mostPayingClient = await _financialInsightsRepo.GetMostPayingClientByBrand(brandId);

            List<FinancialChartDto> chartData = await _financialInsightsRepo.GetFinancialChartDataByBrand(brandId);

            return new ProfitAndLossSummaryQueryResponse
            {
                MostProfitableArea = mostProfitableAreaDto,
                TopClient = mostPayingClient,
                TotalExpenses = totalOneTimeExpenses + totalRecurringExpenses,
                TotalIncome = totalIncomeFromShared,
                FinancialChartData = chartData
            };
        }

        public async Task<DashboardSummaryQueryResponse> GetBrandDashboardSummary(Guid brandId)
        {
            decimal totalNumberOfClients = await _dashboardInsightsRepo.GetClientsCount(brandId);
            decimal avgHours = await _dashboardInsightsRepo.GetAverageHoursCount(brandId);
            decimal checkInNo = await _dashboardInsightsRepo.GetCheckInCount(brandId);
            decimal currentOccupacy = await _dashboardInsightsRepo.GetCurrentOccupacy(brandId);
            return new DashboardSummaryQueryResponse
            {
                TotalNumberOfClients = totalNumberOfClients,
                AvgHours = avgHours,
                CheckInNo = checkInNo,
                CurrentOccupacy = currentOccupacy
            };
        }
    }
}
