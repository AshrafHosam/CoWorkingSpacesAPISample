using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Clients.Queries.GetClient
{
    public class GetClientQueryHandler : IRequestHandler<GetClientQuery, ApiResponse<GetClientQueryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IClientRepo _clientRepo;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public GetClientQueryHandler(IMapper mapper, IClientRepo clientRepo, IUserBrandInternalService userBrandInternalService)
        {
            _mapper = mapper;
            _clientRepo = clientRepo;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<GetClientQueryResponse>> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode)
                return ApiResponse<GetClientQueryResponse>.GetNotFoundApiResponse(error: "Brand Not Found");

            if (request.Id.HasValue)
            {
                var clientEntity = await _clientRepo.GetAsync(request.Id.Value);
                if (clientEntity == null)
                    return ApiResponse<GetClientQueryResponse>.GetNotFoundApiResponse(error: "Client Not Found");

                return new ApiResponse<GetClientQueryResponse>
                {
                    Data = new GetClientQueryResponse
                    {
                        Clients = new List<ClientDto>() { _mapper.Map<ClientDto>(clientEntity) },
                        HasNextPage = false,
                        TotalCount = 1
                    }
                };
            }

            var clients = await _clientRepo.SearchForClient(request.SearchText, userBrand.Data.Id, request.Page, request.PageSize);

            var totalBrandClientsCount = await _clientRepo.GetClientsCount(userBrand.Data.Id, request.SearchText);

            var hasNextPage = (request.Page * request.PageSize) < totalBrandClientsCount;

            return new ApiResponse<GetClientQueryResponse>
            {
                Data = new GetClientQueryResponse
                {
                    Clients = clients,
                    HasNextPage = hasNextPage,
                    TotalCount = totalBrandClientsCount
                }
            };
        }
    }
}
