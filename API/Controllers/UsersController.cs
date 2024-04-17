using Application.Features.Identity.Commands.CreateUser;
using Application.Features.Identity.Commands.LoginUser;
using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(CreateUserCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateUserCommandResponse>> CreateUser(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(LoginUserCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<LoginUserCommandResponse>> LoginUser(LoginUserCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }
        //{
        //  "email": "string@string.com",
        //  "password": "@String1"
        //}
    }
}
