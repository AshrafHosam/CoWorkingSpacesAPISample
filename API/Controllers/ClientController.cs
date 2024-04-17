using API.Attributes;
using Application.Features.Clients.Commands.AddBulkClients;
using Application.Features.Clients.Commands.AddClient;
using Application.Features.Clients.Commands.DeleteAllClients;
using Application.Features.Clients.Commands.DeleteClient;
using Application.Features.Clients.Commands.EditClient;
using Application.Features.Clients.Commands.ImportClients;
using Application.Features.Clients.Queries.GetBrandClients;
using Application.Features.Clients.Queries.GetClient;
using Identity.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("AddClient")]
        [ProducesResponseType(typeof(AddClientCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AddClientCommandResponse), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<AddClientCommandResponse>> AddClient([FromBody] AddClientCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("AddBulkClients")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddBulkClients([FromBody] AddBulkClientsCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetClient")]
        [ProducesResponseType(typeof(List<GetClientQueryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetClient([FromQuery] GetClientQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetBrandClients")]
        [ProducesResponseType(typeof(List<GetClientQueryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBrandClients([FromQuery] GetBrandClientsQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("ImportClients")]
        [ProducesResponseType(typeof(ImportClientsCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> ImportClients([FromForm] ImportClientsCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPut("EditClient")]
        [ProducesResponseType(typeof(EditClientCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EditClientCommandResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditClient([FromBody] EditClientCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpDelete("DeleteClient")]
        [ProducesResponseType(typeof(DeleteClientCommandResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(DeleteClientCommandResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteClient([FromBody] DeleteClientCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpDelete("DeleteAllClients")]
        [ProducesResponseType(typeof(DeleteAllClientsCommandResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(DeleteAllClientsCommandResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAllClients([FromBody] DeleteAllClientsCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }
    }
}
