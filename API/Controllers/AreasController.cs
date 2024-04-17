using API.Attributes;
using Application.Features.Areas.Commands.CreateArea;
using Application.Features.Areas.Commands.DeleteArea;
using Application.Features.Areas.Commands.EditArea;
using Application.Features.Areas.Queries.GetArea;
using Application.Features.Areas.Queries.GetAreaTypes;
using Identity.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AreasController : BaseController
    {
        private readonly IMediator _mediator;

        public AreasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("CreateArea")]
        [ProducesResponseType(typeof(CreateAreaCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateAreaCommandResponse>> CreateArea(CreateAreaCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("EditArea")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EditAreaCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<EditAreaCommandResponse>> EditArea(EditAreaCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetArea")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GetAreaQueryResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetAreaQueryResponse>> GetArea([FromQuery] GetAreaQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpDelete("DeleteArea")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteArea(DeleteAreaCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetAreaTypes")]
        [ProducesResponseType(typeof(List<GetAreaTypesQueryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetAreaTypesQueryResponse>>> GetAreaTypes()
        {
            var result = await _mediator.Send(new GetAreaTypesQuery());

            return GetApiResponse(result);
        }
    }
}
