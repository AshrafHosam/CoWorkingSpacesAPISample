using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Features.Brands.Queries.GetUserBrand;
using Application.Features.Insights.Queries.ProfitAndLossSummary;
using Application.Response;
using MediatR;

namespace Application.Features.Areas.Commands.DeleteArea
{
    public class DeleteAreaCommandHandler : IRequestHandler<DeleteAreaCommand, ApiResponse<DeleteAreaCommandResponse>>
    {
        private readonly IAreaRepo _areaRepo;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public DeleteAreaCommandHandler(IAreaRepo areaRepo, IUserBrandInternalService userBrandInternalService)
        {
            _areaRepo = areaRepo;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<DeleteAreaCommandResponse>> Handle(DeleteAreaCommand request, CancellationToken cancellationToken)
        {
            var area = await _areaRepo.GetArea_PricingPlans_Brand_Included(request.AreaId);
            if (area == null)
                return ApiResponse<DeleteAreaCommandResponse>.GetNotFoundApiResponse(error: "Area Not Found");

            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != area.Branch.BrandId))
                return ApiResponse<DeleteAreaCommandResponse>.GetNotFoundApiResponse(error: "User Brand Not Found");

            await _areaRepo.DeleteAsync(area);

            return ApiResponse<DeleteAreaCommandResponse>.GetNoContentApiResponse();
        }
    }
}
