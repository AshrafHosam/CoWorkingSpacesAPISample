using API.Attributes;
using Application.Features.Branches.Commands.CreateBranch;
using Application.Features.Branches.Commands.DeleteBranch;
using Application.Features.Branches.Commands.EditBranch;
using Application.Features.Branches.Queries.GetBranchAreas;
using Application.Features.Branches.Queries.GetBranches;
using Identity.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BranchesController : BaseController
    {
        private readonly IMediator _mediator;

        public BranchesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("CreateBranch")]
        [ProducesResponseType(typeof(CreateBranchCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateBranchCommandResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CreateBranchCommandResponse>> CreateBranch(CreateBranchCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetBranches")]
        [ProducesResponseType(typeof(List<GetBranchesQueryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetBranchesQueryResponse>>> GetBranches([FromQuery] GetBranchesQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetBranchAreas")]
        [ProducesResponseType(typeof(List<GetBranchAreasQueryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetBranchAreasQueryResponse>>> GetBranchAreas([FromQuery] GetBranchAreasQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPut("EditBranch")]
        [ProducesResponseType(typeof(EditBranchCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EditBranchCommandResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EditBranchCommandResponse>> EditBranch(EditBranchCommand query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpDelete("DeleteBranch")]
        [ProducesResponseType(typeof(DeleteBranchCommandResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(DeleteBranchCommandResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeleteBranchCommandResponse>> DeleteBranch(DeleteBranchCommand query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }
    }
}
