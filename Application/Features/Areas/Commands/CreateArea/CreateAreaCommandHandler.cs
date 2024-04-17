using Application.Contracts.Repos;
using Application.Response;
using Domain.Entities;
using MediatR;

namespace Application.Features.Areas.Commands.CreateArea
{
    public class CreateAreaCommandHandler : IRequestHandler<CreateAreaCommand, ApiResponse<CreateAreaCommandResponse>>
    {
        private readonly IAreaRepo _areaRepo;
        private readonly IBranchRepo _branchRepo;
        private readonly IAreaTypeRepo _areaTypeRepo;
        public CreateAreaCommandHandler(IAreaRepo areaRepo, IBranchRepo branchRepo, IAreaTypeRepo areaTypeRepo)
        {
            _areaRepo = areaRepo;
            _branchRepo = branchRepo;
            _areaTypeRepo = areaTypeRepo;
        }

        public async Task<ApiResponse<CreateAreaCommandResponse>> Handle(CreateAreaCommand request, CancellationToken cancellationToken)
        {
            var isAreaTypeExist = await _areaTypeRepo.AnyAsync(request.AreaTypeId);
            if (!isAreaTypeExist)
                return ApiResponse<CreateAreaCommandResponse>.GetNotFoundApiResponse(error: "Area Type Not Found");

            var isBranchExist = await _branchRepo.AnyAsync(request.BranchId);
            if (!isBranchExist)
                return ApiResponse<CreateAreaCommandResponse>.GetNotFoundApiResponse(error: "Branch Not Found");

            var createdArea = await _areaRepo.AddAsync(new Area
            {
                AreaTypeId = request.AreaTypeId,
                BranchId = request.BranchId,
                Name = request.Name,
                Capacity = request.Capacity,
                BookableAreaPricingPlanModel = request.BookableAreaPricingDTO is null ? null : new BookableAreaPricingPlan
                {
                    PricePerDay = request.BookableAreaPricingDTO?.PricePerDay,
                    PricePerHour = request.BookableAreaPricingDTO?.PricePerHour,
                    PricePerMonth = request.BookableAreaPricingDTO?.PricePerMonth
                },
                SharedAreaPricingPlanModel = request.SharedAreaPricingDTO is null ? null : new SharedAreaPricingPlan
                {
                    PricePerHour = request.SharedAreaPricingDTO.PricePerHour,
                    IsFullDayApplicable = request.SharedAreaPricingDTO.IsFullDayApplicable,
                    FullDayHours = request.SharedAreaPricingDTO.FullDayHours
                }
            });

            if (createdArea is null)
                return ApiResponse<CreateAreaCommandResponse>.GetBadRequestApiResponse(error: "Area Couldn't be Created");

            return ApiResponse<CreateAreaCommandResponse>.GetSuccessApiResponse(new CreateAreaCommandResponse
            {
                AreaId = createdArea.Id
            });
        }
    }
}
