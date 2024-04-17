using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.BrandCosts.Commands.AddEditBrandCostCategory
{
    public class AddEditBrandCostCategoryCommandHandler : IRequestHandler<AddEditBrandCostCategoryCommand, ApiResponse<AddEditBrandCostCategoryCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IBrandCostCategoryRepo _brandCostCategoryRepo;

        public AddEditBrandCostCategoryCommandHandler(IMapper mapper, IBrandCostCategoryRepo brandCostCategoryRepo)
        {
            _mapper = mapper;
            _brandCostCategoryRepo = brandCostCategoryRepo;
        }

        public async Task<ApiResponse<AddEditBrandCostCategoryCommandResponse>> Handle(AddEditBrandCostCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.Id.HasValue)
            {
                var costCategory = await _brandCostCategoryRepo.GetAsync(request.Id.Value);
                if (costCategory == null)
                    return ApiResponse<AddEditBrandCostCategoryCommandResponse>.GetNotFoundApiResponse(error: "Cost Category Not Found");

                _mapper.Map(request, costCategory);

                await _brandCostCategoryRepo.UpdateAsync(costCategory);

                return ApiResponse<AddEditBrandCostCategoryCommandResponse>.GetSuccessApiResponse(_mapper.Map<AddEditBrandCostCategoryCommandResponse>(costCategory));
            }
            else
            {
                var costCategory = await _brandCostCategoryRepo.AddAsync(_mapper.Map<BrandCostCategory>(request));
                if (costCategory == null)
                    return ApiResponse<AddEditBrandCostCategoryCommandResponse>.GetBadRequestApiResponse("Cost Category Not Created");

                return ApiResponse<AddEditBrandCostCategoryCommandResponse>.GetSuccessApiResponse(_mapper.Map<AddEditBrandCostCategoryCommandResponse>(costCategory));
            }
        }
    }
}
