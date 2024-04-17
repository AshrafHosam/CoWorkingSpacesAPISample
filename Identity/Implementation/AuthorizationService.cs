using Application.Contracts.Identity;
using Application.Features.Identity.Commands.CreateUser;
using Application.Features.Identity.Commands.LoginUser;
using Application.Response;
using Identity.Entities;
using Identity.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Implementation
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthorizationService(IConfiguration configuration, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApiResponse<CreateUserCommandResponse>> CreateAsync(CreateUserCommand createUserModel)
        {
            var user = new AppUser
            {
                Email = createUserModel.Email,
                EmailConfirmed = true,
                PhoneNumber = createUserModel.PhoneNumber,
                UserName = createUserModel.Username
            };

            var result = await _userManager.CreateAsync(user, createUserModel.Password);

            if (!result.Succeeded)
                return ApiResponse<CreateUserCommandResponse>
                    .GetBadRequestApiResponse(result.Errors.Select(a => a.Description).ToList());

            await _userManager.AddToRolesAsync(user,
                new List<string>()
                {
                    nameof(UserRolesEnum.Owner),
                    nameof(UserRolesEnum.BranchManager),
                    nameof(UserRolesEnum.Receptionist)
                });

            return ApiResponse<CreateUserCommandResponse>
                .GetSuccessApiResponse(new CreateUserCommandResponse
                {
                    UserId = Guid.Parse((await _userManager.FindByNameAsync(user.UserName)).Id)
                });
        }

        public string GenerateJwtToken(string userId, string userName, string email, List<string> roles)
        {
            var secretKey = _configuration.GetSection("JwtConfiguration:SecretKey").Value;

            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenHandler = new JwtSecurityTokenHandler();

            var userClaims = roles.Select(a => new Claim(ClaimTypes.Role, a)).ToList();

            userClaims.AddRange(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Email,email),
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<ApiResponse<LoginUserCommandResponse>> LoginAsync(LoginUserCommand loginUserModel)
        {
            var user = await _userManager.FindByEmailAsync(loginUserModel.Email);

            if (user == null)
                return ApiResponse<LoginUserCommandResponse>
                    .GetNotFoundApiResponse(error: "Username or password is incorrect");

            var roles = (await _userManager.GetRolesAsync(user)).ToList();

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginUserModel.Password, false, false);

            if (!signInResult.Succeeded)
                return ApiResponse<LoginUserCommandResponse>
                    .GetNotFoundApiResponse(error: "Username or password is incorrect");


            return ApiResponse<LoginUserCommandResponse>
                .GetSuccessApiResponse(new LoginUserCommandResponse
                {
                    Email = user.Email,
                    Username = user.UserName,
                    UserId = Guid.Parse(user.Id),
                    Token = GenerateJwtToken(user.Id, user.UserName, user.Email, roles)
                });
        }
    }
}
