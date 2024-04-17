using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Areas.Queries.GetArea
{
    public class GetAreaQueryHandler : IRequestHandler<GetAreaQuery, ApiResponse<GetAreaQueryResponse>>
    {
        private readonly IAreaRepo _areaRepo;
        private readonly IMapper _mapper;

        public GetAreaQueryHandler(IAreaRepo areaRepo, IMapper mapper)
        {
            _areaRepo = areaRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetAreaQueryResponse>> Handle(GetAreaQuery request, CancellationToken cancellationToken)
        {
            var area = await _areaRepo.GetAreaPricingPlansIncluded(request.AreaId);
            if (area == null)
                return ApiResponse<GetAreaQueryResponse>.GetNotFoundApiResponse(error: "Area Not Found");

            return ApiResponse<GetAreaQueryResponse>.GetSuccessApiResponse(new GetAreaQueryResponse
            {
                AreaId = area.Id,
                AreaTypeId = area.AreaTypeId,
                BookableAreaPricingDTO = area.BookableAreaPricingPlanModel == null ? null : new Common.BookableAreaPricingDto
                {
                    PricePerDay = area.BookableAreaPricingPlanModel.PricePerDay,
                    PricePerHour = area.BookableAreaPricingPlanModel.PricePerHour,
                    PricePerMonth = area.BookableAreaPricingPlanModel.PricePerMonth
                },
                BranchId = area.BranchId,
                Capacity = area.Capacity,
                Name = area.Name,
                SharedAreaPricingDTO = area.SharedAreaPricingPlanModel == null ? null : new Common.SharedAreaPricingDto
                {
                    PricePerHour = area.SharedAreaPricingPlanModel.PricePerHour,
                    FullDayHours = area.SharedAreaPricingPlanModel.FullDayHours,
                    IsFullDayApplicable = area.SharedAreaPricingPlanModel.IsFullDayApplicable
                }
            });
        }
    }
}