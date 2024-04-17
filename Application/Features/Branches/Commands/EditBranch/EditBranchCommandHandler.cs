using Application.Contracts.Repos;
using Application.Response;
using MediatR;

namespace Application.Features.Branches.Commands.EditBranch
{
    internal class EditBranchCommandHandler : IRequestHandler<EditBranchCommand, ApiResponse<EditBranchCommandResponse>>
    {
        private readonly IBranchRepo _branchRepo;

        public EditBranchCommandHandler(IBranchRepo branchRepo)
        {
            _branchRepo = branchRepo;
        }

        public async Task<ApiResponse<EditBranchCommandResponse>> Handle(EditBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepo.GetAsync(request.BranchId, request.BrandId);
            if (branch is null)
                return ApiResponse<EditBranchCommandResponse>.GetNotFoundApiResponse(error: "Branch not found");

            branch.Name = request.Name;
            branch.Address = request.Address;
            branch.PhoneNumber = request.PhoneNumber;
            branch.LocationUrl = request.LocationUrl;

            await _branchRepo.UpdateAsync(branch);

            return ApiResponse<EditBranchCommandResponse>.GetSuccessApiResponse(
                new EditBranchCommandResponse
                {
                    BranchId = branch.Id,
                    Name = branch.Name,
                    Address = branch.Address,
                    LocationUrl = branch.LocationUrl,
                    PhoneNumber = branch.PhoneNumber,
                    BrandId = branch.BrandId
                });
        }
    }
}
