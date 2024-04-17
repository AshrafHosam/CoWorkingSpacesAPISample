using Application.Contracts.Identity;
using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Features.Brands.Queries.GetUserBrand;
using Application.Features.Insights.Queries.ProfitAndLossSummary;
using Application.Response;
using MediatR;

namespace Application.Features.Branches.Commands.DeleteBranch
{
    internal class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand, ApiResponse<DeleteBranchCommandResponse>>
    {
        private readonly IBranchRepo _branchRepo;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public DeleteBranchCommandHandler(IBranchRepo branchRepo, IUserBrandInternalService userBrandInternalService)
        {
            _branchRepo = branchRepo;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<DeleteBranchCommandResponse>> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != request.BrandId))
                return ApiResponse<DeleteBranchCommandResponse>.GetNotFoundApiResponse(error: "User Brand Not Found");

            var branch = await _branchRepo.GetAsync(request.BranchId, request.BrandId);
            if (branch is null)
                return ApiResponse<DeleteBranchCommandResponse>.GetNotFoundApiResponse("Branch Not Found");

            await _branchRepo.DeleteAsync(branch);

            return ApiResponse<DeleteBranchCommandResponse>.GetNoContentApiResponse();
        }
    }
}
