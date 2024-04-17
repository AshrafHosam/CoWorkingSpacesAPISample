using API.Attributes;
using Application.Features.Reservations.Commands.AddReservation;
using Application.Features.Reservations.Commands.DeleteReservation;
using Application.Features.Reservations.Commands.EditReservation;
using Application.Features.Reservations.Queries.GetAllReservations;
using Application.Features.Reservations.Queries.GetClientReservations;
using Application.Features.Reservations.Queries.GetReservation;
using Identity.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("CreateReservation")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CreateReservationCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateReservationCommandResponse>> CreateReservation(CreateReservationCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("EditReservation")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EditReservationCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<EditReservationCommandResponse>> EditReservation(EditReservationCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }
        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpDelete("DeleteReservation")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DeleteReservationCommand), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteReservation(DeleteReservationCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetAllReservations")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GetAllReservationsQueryResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllReservations([FromQuery] GetAllReservationsQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetReservation")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GetReservationQuery), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetReservationQueryResponse>> GetReservation([FromQuery] GetReservationQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }


        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetClientReservations")]
        [ProducesResponseType(typeof(List<GetClientReservationsQueryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetClientReservationsQueryResponse>>> GetClientReservations([FromQuery] GetClientReservationsQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }
    }
}
