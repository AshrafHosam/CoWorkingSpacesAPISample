using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.BrandCosts.Queries.GetBrandCostCategories
{
    public class GetBrandCostCategoriesQueryHandler : IRequestHandler<GetBrandCostCategoriesQuery, ApiResponse<List<GetBrandCostCategoriesQueryResponse>>>
    {
        private readonly IBrandCostCategoryRepo _brandCostCategoryRepo;
        private readonly IMapper _mapper;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public GetBrandCostCategoriesQueryHandler(IBrandCostCategoryRepo brandCostCategoryRepo, IMapper mapper, IUserBrandInternalService userBrandInternalService)
        {
            _brandCostCategoryRepo = brandCostCategoryRepo;
            _mapper = mapper;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<List<GetBrandCostCategoriesQueryResponse>>> Handle(GetBrandCostCategoriesQuery request, CancellationToken cancellationToken)
        {
            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != request.BrandId))
                return ApiResponse<List<GetBrandCostCategoriesQueryResponse>>.GetNotFoundApiResponse(error: "User Brand Not Found");

            var brandCostCategories = await _brandCostCategoryRepo.GetBrandCostCategories(request.BrandId);

            return ApiResponse<List<GetBrandCostCategoriesQueryResponse>>.GetSuccessApiResponse(_mapper.Map<List<GetBrandCostCategoriesQueryResponse>>(brandCostCategories));
        }
    }
}
