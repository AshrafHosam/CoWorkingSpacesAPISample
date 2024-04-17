using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Branches.Queries.GetBranches
{
    public class GetBranchesQueryHandler : IRequestHandler<GetBranchesQuery, ApiResponse<List<GetBranchesQueryResponse>>>
    {
        private readonly IBranchRepo _branchRepo;
        private readonly IMapper _mapper;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public GetBranchesQueryHandler(IBranchRepo branchRepo, IMapper mapper, IUserBrandInternalService userBrandInternalService)
        {
            _branchRepo = branchRepo;
            _mapper = mapper;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<List<GetBranchesQueryResponse>>> Handle(GetBranchesQuery request, CancellationToken cancellationToken)
        {
            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != request.BrandId))
                return ApiResponse<List<GetBranchesQueryResponse>>.GetNotFoundApiResponse(error: "User Brand Not Found");

            var branches = await _branchRepo.GetBranchesByBrandId(request.BrandId);

            return ApiResponse<List<GetBranchesQueryResponse>>.GetSuccessApiResponse(_mapper.Map<List<GetBranchesQueryResponse>>(branches));
        }
    }
}
