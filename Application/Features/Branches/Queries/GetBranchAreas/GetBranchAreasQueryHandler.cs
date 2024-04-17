using Application.Contracts.Repos;
using Application.Response;
using MediatR;

namespace Application.Features.Branches.Queries.GetBranchAreas
{
    public class GetBranchAreasQueryHandler : IRequestHandler<GetBranchAreasQuery, ApiResponse<List<GetBranchAreasQueryResponse>>>
    {
        private readonly IAreaRepo _areaRepo;
        private readonly IBranchRepo _branchRepo;
        public GetBranchAreasQueryHandler(IAreaRepo areaRepo, IBranchRepo branchRepo)
        {
            _areaRepo = areaRepo;
            _branchRepo = branchRepo;
        }

        public async Task<ApiResponse<List<GetBranchAreasQueryResponse>>> Handle(GetBranchAreasQuery request, CancellationToken cancellationToken)
        {
            var branchExist = await _branchRepo.AnyAsync(request.BranchId);
            if (!branchExist)
                return ApiResponse<List<GetBranchAreasQueryResponse>>.GetBadRequestApiResponse(error: "Branch Not Exist");

            var areas = await _areaRepo.GetAreasByBranch(request.BranchId);

            return ApiResponse<List<GetBranchAreasQueryResponse>>.GetSuccessApiResponse(areas.Select(area => new GetBranchAreasQueryResponse
            {
                AreaId = area.Id,
                AreaTypeId = area.AreaTypeId,
                BookableAreaPricingDTO = area.BookableAreaPricingPlanModel == null ? null : new Areas.Common.BookableAreaPricingDto
                {
                    PricePerDay = area.BookableAreaPricingPlanModel.PricePerDay,
                    PricePerHour = area.BookableAreaPricingPlanModel.PricePerHour,
                    PricePerMonth = area.BookableAreaPricingPlanModel.PricePerMonth
                },
                BranchId = area.BranchId,
                Capacity = area.Capacity,
                Name = area.Name,
                SharedAreaPricingDTO = area.SharedAreaPricingPlanModel == null ? null : new Areas.Common.SharedAreaPricingDto
                {
                    PricePerHour = area.SharedAreaPricingPlanModel.PricePerHour,
                    FullDayHours = area.SharedAreaPricingPlanModel.FullDayHours,
                    IsFullDayApplicable = area.SharedAreaPricingPlanModel.IsFullDayApplicable
                }
            }).ToList());
        }
    }
}
