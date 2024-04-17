using API.Attributes;
using Application.Features.BrandCustomServices.Commands.AddEditCustomServiceCategory;
using Application.Features.BrandCustomServices.Queries.GetBrandServiceCategories;
using Identity.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CustomServicesController : BaseController
    {
        private readonly IMediator _mediator;

        public CustomServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpPost("AddEditServiceCategory")]
        [ProducesResponseType(typeof(AddEditCustomServiceCategoryCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<AddEditCustomServiceCategoryCommandResponse>> AddEditServiceCategory(AddEditCustomServiceCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("GetBrandServiceCategories")]
        [ProducesResponseType(typeof(List<GetBrandServiceCategoriesQueryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetBrandServiceCategoriesQueryResponse>>> GetBrandServiceCategories([FromQuery] GetBrandServiceCategoriesQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }
    }
}
