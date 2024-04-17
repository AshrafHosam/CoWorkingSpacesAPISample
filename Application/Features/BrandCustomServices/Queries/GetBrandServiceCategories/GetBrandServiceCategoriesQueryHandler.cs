using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.BrandCustomServices.Queries.GetBrandServiceCategories
{
    public class GetBrandServiceCategoriesQueryHandler : IRequestHandler<GetBrandServiceCategoriesQuery, ApiResponse<List<GetBrandServiceCategoriesQueryResponse>>>
    {
        private readonly ICustomServiceCategoryRepo _customServiceCategoryRepo;
        private readonly IMapper _mapper;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public GetBrandServiceCategoriesQueryHandler(ICustomServiceCategoryRepo customServiceCategoryRepo, IMapper mapper, IUserBrandInternalService userBrandInternalService)
        {
            _customServiceCategoryRepo = customServiceCategoryRepo;
            _mapper = mapper;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<List<GetBrandServiceCategoriesQueryResponse>>> Handle(GetBrandServiceCategoriesQuery request, CancellationToken cancellationToken)
        {
            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != request.BrandId))
                return ApiResponse<List<GetBrandServiceCategoriesQueryResponse>>.GetNotFoundApiResponse(error: "User Brand Not Found");

            var categories = await _customServiceCategoryRepo.GetBrandServiceCategories(request.BrandId);

            return ApiResponse<List<GetBrandServiceCategoriesQueryResponse>>.GetSuccessApiResponse(_mapper.Map<List<GetBrandServiceCategoriesQueryResponse>>(categories));
        }
    }
}
