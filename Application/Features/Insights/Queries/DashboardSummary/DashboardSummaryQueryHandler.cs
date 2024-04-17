using Application.Contracts.Services;
using Application.Response;
using MediatR;

namespace Application.Features.Insights.Queries.DashboardSummary
{
    internal class DashboardSummaryQueryHandler : IRequestHandler<DashboardSummaryQuery, ApiResponse<DashboardSummaryQueryResponse>>
    {
        private readonly IInsightsService _insightsService;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public DashboardSummaryQueryHandler(IInsightsService insightsService, IUserBrandInternalService userBrandInternalService)
        {
            _insightsService = insightsService;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<DashboardSummaryQueryResponse>> Handle(DashboardSummaryQuery request, CancellationToken cancellationToken)
        {
            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != request.BrandId))
                return ApiResponse<DashboardSummaryQueryResponse>.GetNotFoundApiResponse(error: "User Brand Not Found");

            var result = await _insightsService.GetBrandDashboardSummary(request.BrandId);

            return ApiResponse<DashboardSummaryQueryResponse>.GetSuccessApiResponse(result);
        }
    }
}
