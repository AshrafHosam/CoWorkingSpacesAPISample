using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Features.Clients.Queries.GetClient;
using Application.Response;
using MediatR;

namespace Application.Features.Clients.Queries.GetBrandClients
{
    internal class GetBrandClientsQueryHandler : IRequestHandler<GetBrandClientsQuery, ApiResponse<GetClientQueryResponse>>
    {
        private readonly IClientRepo _clientRepo;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public GetBrandClientsQueryHandler(IClientRepo clientRepo, IUserBrandInternalService userBrandInternalService)
        {
            _clientRepo = clientRepo;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<GetClientQueryResponse>> Handle(GetBrandClientsQuery request, CancellationToken cancellationToken)
        {
            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != request.BrandId))
                return ApiResponse<GetClientQueryResponse>.GetNotFoundApiResponse(error: "User Brand Not Found");

            var brandClients = await _clientRepo.GetBrandClientsPaginated(request.BrandId, request.Page, request.PageSize);

            var totalBrandClientsCount = await _clientRepo.GetClientsCount(request.BrandId);

            return ApiResponse<GetClientQueryResponse>
                .GetSuccessApiResponse(new GetClientQueryResponse
                {
                    Clients = brandClients,
                    TotalCount = totalBrandClientsCount,
                    HasNextPage = (request.Page * request.PageSize) < totalBrandClientsCount
                });
        }
    }
}
