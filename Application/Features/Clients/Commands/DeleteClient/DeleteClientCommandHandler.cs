using Application.Contracts.Repos;
using Application.Response;
using MediatR;

namespace Application.Features.Clients.Commands.DeleteClient
{
    internal class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, ApiResponse<DeleteClientCommandResponse>>
    {
        private readonly IClientRepo _clientRepo;

        public DeleteClientCommandHandler(IClientRepo clientRepo)
        {
            _clientRepo = clientRepo;
        }

        public async Task<ApiResponse<DeleteClientCommandResponse>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepo.GetBrandClient(request.BrandId, request.ClientId);
            if (client is null)
                return ApiResponse<DeleteClientCommandResponse>.GetNotFoundApiResponse("Client not found");

            await _clientRepo.DeleteAsync(client);

            return ApiResponse<DeleteClientCommandResponse>.GetNoContentApiResponse();
        }
    }
}
