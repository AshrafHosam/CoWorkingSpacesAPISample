using Application.Features.Identity.Commands.CreateUser;
using Application.Features.Identity.Commands.LoginUser;
using Application.Response;
using System.Data;

namespace Application.Contracts.Identity
{
    public interface IAuthorizationService
    {
        //Task<Guid> ChangePasswordAsync(Guid userId, ChangePasswordModel changePasswordModel);

        //Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel);

        Task<ApiResponse<CreateUserCommandResponse>> CreateAsync(CreateUserCommand createUserModel);

        Task<ApiResponse<LoginUserCommandResponse>> LoginAsync(LoginUserCommand loginUserModel);

        string GenerateJwtToken(string userId, string userName, string email, List<string> roles);
    }
}
