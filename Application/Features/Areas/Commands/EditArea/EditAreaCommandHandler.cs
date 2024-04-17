using Application.Contracts.Repos;
using Application.Features.Areas.Common;
using Application.Response;
using MediatR;

namespace Application.Features.Areas.Commands.EditArea
{
    public class EditAreaCommandHandler : IRequestHandler<EditAreaCommand, ApiResponse<EditAreaCommandResponse>>
    {
        private readonly IAreaRepo _areaRepo;
        public EditAreaCommandHandler(IAreaRepo areaRepo)
        {
            _areaRepo = areaRepo;
        }

        public async Task<ApiResponse<EditAreaCommandResponse>> Handle(EditAreaCommand request, CancellationToken cancellationToken)
        {
            var area = await _areaRepo.GetAreaPricingPlansIncluded(request.AreaId);
            if (area == null)
                return ApiResponse<EditAreaCommandResponse>.GetNotFoundApiResponse(error: "Area Not Found");

            if (area.AreaTypeId != request.AreaTypeId)
                return ApiResponse<EditAreaCommandResponse>.GetBadRequestApiResponse(error: "Area Type Mismatch");

            area.Name = request.Name;
            area.Capacity = request.Capacity;
            area.BranchId = request.BranchId;

            if (area.BookableAreaPricingPlanModel is not null)
            {
                area.BookableAreaPricingPlanModel.PricePerMonth = request.BookableAreaPricingDTO.PricePerMonth;
                area.BookableAreaPricingPlanModel.PricePerDay = request.BookableAreaPricingDTO.PricePerDay;
                area.BookableAreaPricingPlanModel.PricePerHour = request.BookableAreaPricingDTO.PricePerHour;
            }

            if (area.SharedAreaPricingPlanModel is not null)
            {
                area.SharedAreaPricingPlanModel.PricePerHour = request.SharedAreaPricingDTO.PricePerHour;
                area.SharedAreaPricingPlanModel.IsFullDayApplicable = request.SharedAreaPricingDTO.IsFullDayApplicable;
                area.SharedAreaPricingPlanModel.FullDayHours = request.SharedAreaPricingDTO.FullDayHours;
            }

            await _areaRepo.UpdateAsync(area);

            return ApiResponse<EditAreaCommandResponse>.GetSuccessApiResponse(new EditAreaCommandResponse
            {
                AreaTypeId = request.AreaTypeId,
                BranchId = request.BranchId,
                Name = request.Name,
                Capacity = request.Capacity,
                BookableAreaPricingDTO = request.BookableAreaPricingDTO is null ? null : new BookableAreaPricingDto
                {
                    PricePerDay = request.BookableAreaPricingDTO?.PricePerDay,
                    PricePerHour = request.BookableAreaPricingDTO?.PricePerHour,
                    PricePerMonth = request.BookableAreaPricingDTO?.PricePerMonth
                },
                SharedAreaPricingDTO = request.SharedAreaPricingDTO is null ? null : new SharedAreaPricingDto
                {
                    PricePerHour = request.SharedAreaPricingDTO.PricePerHour,
                    IsFullDayApplicable = request.SharedAreaPricingDTO.IsFullDayApplicable,
                    FullDayHours = request.SharedAreaPricingDTO.FullDayHours
                }
            });
        }
    }
}
