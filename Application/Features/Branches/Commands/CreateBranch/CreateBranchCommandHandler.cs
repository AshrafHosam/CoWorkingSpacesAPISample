using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Branches.Commands.CreateBranch
{
    public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, ApiResponse<CreateBranchCommandResponse>>
    {
        private readonly IBranchRepo _branchRepo;
        private readonly IMapper _mapper;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public CreateBranchCommandHandler(IBranchRepo branchRepo, IMapper mapper, IUserBrandInternalService userBrandInternalService)
        {
            _branchRepo = branchRepo;
            _mapper = mapper;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<CreateBranchCommandResponse>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != request.BrandId))
                return ApiResponse<CreateBranchCommandResponse>.GetNotFoundApiResponse(error: "User Brand Not Found");

            var createdBranch = await _branchRepo.AddAsync(_mapper.Map<Branch>(request));
            if (createdBranch is null)
                return ApiResponse<CreateBranchCommandResponse>.GetBadRequestApiResponse(error: "Branch Not Created");

            return ApiResponse<CreateBranchCommandResponse>
                .GetSuccessApiResponse(new CreateBranchCommandResponse
                {
                    Id = createdBranch.Id
                });
        }
    }
}
