using API.Attributes;
using Application.Features.Visits.Commands.CheckOutClient;
using Application.Features.Visits.Commands.CheckOutClientsBatch;
using Application.Features.Visits.Queries.GetNonCheckedInClients;
using Identity.Enums;
using Issentialz.Application.Features.Visits.Commands.CheckInClient;
using Issentialz.Application.Features.Visits.Queries.GetCheckedInClients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class VisitsController : BaseController
    {
        private readonly IMediator _mediator;

        public VisitsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetCheckedInClients")]
        [ProducesResponseType(typeof(List<GetCheckedInClientsQueryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetCheckedInClientsQueryResponse>>> GetCheckedInClients([FromQuery] GetCheckedInClientsQuery query)
        {
            var result = await _mediator.Send(query);
            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("CheckInClient")]
        [ProducesResponseType(typeof(CheckInClientCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<CheckInClientCommandResponse>> CheckInClient(CheckInClientCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("CheckOutClient")]
        [ProducesResponseType(typeof(CheckOutClientCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<CheckOutClientCommandResponse>> CheckOutClient(CheckOutClientCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("CheckOutClientsBatch")]
        [ProducesResponseType(typeof(List<CheckOutClientsBatchCommandResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CheckOutClientsBatchCommandResponse>>> CheckOutClientsBatch(CheckOutClientsBatchCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetNonCheckedInClients")]
        [ProducesResponseType(typeof(List<GetNonCheckedInClientsQueryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetNonCheckedInClients([FromQuery] GetNonCheckedInClientsQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }
    }
}
