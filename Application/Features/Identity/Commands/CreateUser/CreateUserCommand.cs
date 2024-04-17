using Application.Response;
using MediatR;

namespace Application.Features.Identity.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<ApiResponse<CreateUserCommandResponse>>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
