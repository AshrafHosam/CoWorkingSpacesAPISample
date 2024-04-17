using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Clients.Commands.AddClient
{
    public class AddClientCommandHandler : IRequestHandler<AddClientCommand, ApiResponse<AddClientCommandResponse>>
    {
        private readonly IClientRepo _clientRepo;
        private readonly IMapper _mapper;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public AddClientCommandHandler(IClientRepo clientRepo, IMapper mapper, IUserBrandInternalService userBrandInternalService)
        {
            _clientRepo = clientRepo;
            _mapper = mapper;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<AddClientCommandResponse>> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != request.BrandId))
                return ApiResponse<AddClientCommandResponse>.GetNotFoundApiResponse(error: "User Brand Not Found");


            var clientValidation = await _clientRepo.SearchAsync(request.BrandId, request.Email, request.MobileNumber);
            if (clientValidation != null)
                return ApiResponse<AddClientCommandResponse>
                    .GetConflictApiResponse(new AddClientCommandResponse
                    {
                        Id = clientValidation.Id,
                        Email = clientValidation.Email,
                        MobileNumber = clientValidation.MobileNumber,
                        Name = clientValidation.Name,
                    });

            var createdClient = await _clientRepo.AddAsync(_mapper.Map<Domain.Entities.Client>(request));

            return ApiResponse<AddClientCommandResponse>
                .GetSuccessApiResponse(new AddClientCommandResponse
                {
                    Id = createdClient.Id,
                    Name = createdClient.Name,
                    MobileNumber = createdClient.MobileNumber,
                    Email = createdClient.Email
                });
        }
    }
}
