using API.Attributes;
using Application.Contracts.Identity;
using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Queries.GetUserBrand;
using Identity.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BrandsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IClaimService _claimService;
        public BrandsController(IMediator mediator, IClaimService claimService)
        {
            _mediator = mediator;
            _claimService = claimService;
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("CreateBrand")]
        [ProducesResponseType(typeof(CreateBrandCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateBrandCommandResponse>> CreateBrand(CreateBrandCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Receptionist, UserRolesEnum.BranchManager, UserRolesEnum.Owner)]
        [HttpGet("GetUserBrand")]
        [ProducesResponseType(typeof(GetUserBrandQueryResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetUserBrandQueryResponse>> GetUserBrand()
        {
            var result = await _mediator.Send(new GetUserBrandQuery(Guid.Parse(_claimService.GetUserId())));

            return GetApiResponse(result);
        }
    }
}
