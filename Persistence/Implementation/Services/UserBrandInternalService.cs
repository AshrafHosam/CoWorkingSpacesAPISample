using Application.Contracts.Identity;
using Application.Contracts.Services;
using Application.Features.Brands.Queries.GetUserBrand;
using Application.Response;
using MediatR;

namespace Persistence.Implementation.Services
{
    internal class UserBrandInternalService : IUserBrandInternalService
    {
        private readonly IClaimService _claimService;
        private readonly IMediator _mediator;

        public UserBrandInternalService(IClaimService claimService, IMediator mediator)
        {
            _claimService = claimService;
            _mediator = mediator;
        }

        public async Task<ApiResponse<GetUserBrandQueryResponse>> GetLoggedInUserBrand()
        {
            return await _mediator.Send(new GetUserBrandQuery(Guid.Parse(_claimService.GetUserId())));
        }
    }
}
