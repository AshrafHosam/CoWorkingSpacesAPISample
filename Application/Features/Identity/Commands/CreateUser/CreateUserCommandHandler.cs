using Application.Contracts.Identity;
using Application.Response;
using MediatR;

namespace Application.Features.Identity.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<CreateUserCommandResponse>>
    {
        private readonly IAuthorizationService _authorizationService;

        public CreateUserCommandHandler(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<ApiResponse<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _authorizationService.CreateAsync(request);
        }
    }
}
