using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Clients.Commands.AddBulkClients
{
    public class AddBulkClientsCommandHandler : IRequestHandler<AddBulkClientsCommand, ApiResponse<AddBulkClientsCommandResponse>>
    {
        private readonly IClientRepo _clientRepo;
        private readonly IMapper _mapper;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public AddBulkClientsCommandHandler(IClientRepo clientRepo, IMapper mapper, IUserBrandInternalService userBrandInternalService)
        {
            _clientRepo = clientRepo;
            _mapper = mapper;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<AddBulkClientsCommandResponse>> Handle(AddBulkClientsCommand request, CancellationToken cancellationToken)
        {
            var brandId = request.Clients.Select(a => a.BrandId).Distinct().ToList();
            if (brandId.Count != 1)
                return ApiResponse<AddBulkClientsCommandResponse>.GetBadRequestApiResponse(error: "Can't have more than one brand");

            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != brandId.First()))
                return ApiResponse<AddBulkClientsCommandResponse>.GetNotFoundApiResponse(error: "User Brand Not Found");

            var addedClients = await _clientRepo.AddRangeAsync(_mapper.Map<List<Domain.Entities.Client>>(request.Clients));
            if (addedClients.Count() == request.Clients.Count)
                return ApiResponse<AddBulkClientsCommandResponse>.GetNoContentApiResponse();

            return ApiResponse<AddBulkClientsCommandResponse>.GetBadRequestApiResponse(error: $"Only {addedClients.Count()} Added From {request.Clients.Count} Clients");
        }
    }
}
