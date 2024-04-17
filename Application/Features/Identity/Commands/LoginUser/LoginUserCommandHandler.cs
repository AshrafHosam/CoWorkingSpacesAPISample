using Application.Contracts.Identity;
using Application.Response;
using MediatR;

namespace Application.Features.Identity.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ApiResponse<LoginUserCommandResponse>>
    {
        private readonly IAuthorizationService _authorizationService;

        public LoginUserCommandHandler(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<ApiResponse<LoginUserCommandResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await _authorizationService.LoginAsync(request);
        }
    }
}
