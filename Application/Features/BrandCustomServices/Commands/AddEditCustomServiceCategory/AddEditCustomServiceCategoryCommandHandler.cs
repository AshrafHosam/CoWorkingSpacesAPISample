using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.BrandCustomServices.Commands.AddEditCustomServiceCategory
{
    public class AddEditCustomServiceCategoryCommandHandler : IRequestHandler<AddEditCustomServiceCategoryCommand, ApiResponse<AddEditCustomServiceCategoryCommandResponse>>
    {
        private readonly ICustomServiceCategoryRepo _categoryRepo;
        private readonly IMapper _mapper;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public AddEditCustomServiceCategoryCommandHandler(ICustomServiceCategoryRepo categoryRepo, IMapper mapper, IUserBrandInternalService userBrandInternalService)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<AddEditCustomServiceCategoryCommandResponse>> Handle(AddEditCustomServiceCategoryCommand request, CancellationToken cancellationToken)
        {
            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != request.BrandId))
                return ApiResponse<AddEditCustomServiceCategoryCommandResponse>.GetNotFoundApiResponse(error: "User Brand Not Found");

            if (request.CategoryId.HasValue)
            {
                var category = await _categoryRepo.GetAsync(request.CategoryId.Value);
                if (category == null)
                    return ApiResponse<AddEditCustomServiceCategoryCommandResponse>.GetNotFoundApiResponse(error: "Category Not Found");

                _mapper.Map(request, category);

                await _categoryRepo.UpdateAsync(category);

                return ApiResponse<AddEditCustomServiceCategoryCommandResponse>.GetSuccessApiResponse(_mapper.Map<AddEditCustomServiceCategoryCommandResponse>(category));
            }
            else
            {
                var category = await _categoryRepo.AddAsync(_mapper.Map<CustomServiceCategory>(request));
                if (category == null)
                    return ApiResponse<AddEditCustomServiceCategoryCommandResponse>.GetBadRequestApiResponse(error: "Category Not Created");

                return ApiResponse<AddEditCustomServiceCategoryCommandResponse>.GetSuccessApiResponse(_mapper.Map<AddEditCustomServiceCategoryCommandResponse>(category));
            }
        }
    }
}
