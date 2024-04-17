using Application.Contracts.Services;
using Application.Response;
using MediatR;

namespace Application.Features.Insights.Queries.ProfitAndLossSummary
{
    internal class ProfitAndLossSummaryQueryHandler : IRequestHandler<ProfitAndLossSummaryQuery, ApiResponse<ProfitAndLossSummaryQueryResponse>>
    {
        private readonly IInsightsService _insightsService;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public ProfitAndLossSummaryQueryHandler(IInsightsService insightsService, IUserBrandInternalService userBrandInternalService)
        {
            _insightsService = insightsService;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<ProfitAndLossSummaryQueryResponse>> Handle(ProfitAndLossSummaryQuery request, CancellationToken cancellationToken)
        {
            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != request.BrandId))
                return ApiResponse<ProfitAndLossSummaryQueryResponse>.GetNotFoundApiResponse(error: "User Brand Not Found");

            ProfitAndLossSummaryQueryResponse result;

            if (request.BranchId.HasValue && request.BranchId != Guid.Empty)
                result = await _insightsService.GetBrandBranchProfitAndLossSummary(request.BrandId, request.BranchId.Value);
            else
                result = await _insightsService.GetBrandProfitAndLossSummary(request.BrandId);

            return ApiResponse<ProfitAndLossSummaryQueryResponse>.GetSuccessApiResponse(result);
        }
    }
}
