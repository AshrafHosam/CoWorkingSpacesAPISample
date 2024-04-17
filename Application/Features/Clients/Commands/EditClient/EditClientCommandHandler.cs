using Application.Contracts.Repos;
using Application.Features.Clients.Commands.AddClient;
using Application.Response;
using MediatR;

namespace Application.Features.Clients.Commands.EditClient
{
    internal class EditClientCommandHandler : IRequestHandler<EditClientCommand, ApiResponse<EditClientCommandResponse>>
    {
        private readonly IClientRepo _clientRepo;

        public EditClientCommandHandler(IClientRepo clientRepo)
        {
            _clientRepo = clientRepo;
        }

        public async Task<ApiResponse<EditClientCommandResponse>> Handle(EditClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepo.GetBrandClient(request.BrandId, request.ClientId);
            if (client is null)
                return ApiResponse<EditClientCommandResponse>.GetNotFoundApiResponse(error: "Client not found");

            client.Name = request.Name;
            client.MobileNumber = request.MobileNumber;
            client.Email = request.Email;
            client.ProfessionalTitle = request.ProfessionalTitle;
            client.Source = request.Source;
            client.Interests = request.Interests;

            await _clientRepo.UpdateAsync(client);

            return ApiResponse<EditClientCommandResponse>
                .GetSuccessApiResponse(new EditClientCommandResponse
                {
                    Id = client.Id,
                    Name = client.Name,
                    MobileNumber = client.MobileNumber,
                    Email = client.Email
                });
        }
    }
}
