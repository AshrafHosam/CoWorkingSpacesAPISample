using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Response;
using MediatR;

namespace Application.Features.Clients.Commands.DeleteAllClients
{
    internal class DeleteAllClientsCommandHandler : IRequestHandler<DeleteAllClientsCommand, ApiResponse<DeleteAllClientsCommandResponse>>
    {
        private readonly IClientRepo _clientRepo;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public DeleteAllClientsCommandHandler(IClientRepo clientRepo, IUserBrandInternalService userBrandInternalService)
        {
            _clientRepo = clientRepo;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<DeleteAllClientsCommandResponse>> Handle(DeleteAllClientsCommand request, CancellationToken cancellationToken)
        {
            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != request.BrandId))
                return ApiResponse<DeleteAllClientsCommandResponse>.GetNotFoundApiResponse(error: "Brand Not Found");

            await _clientRepo.DeleteAllClients(request.BrandId);

            return ApiResponse<DeleteAllClientsCommandResponse>.GetNoContentApiResponse();
        }
    }
}
