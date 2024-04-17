using API.Attributes;
using Application.Features.Insights.Queries.DashboardSummary;
using Application.Features.Insights.Queries.ProfitAndLossSummary;
using Identity.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class InsightsController : BaseController
    {
        private readonly IMediator _mediator;

        public InsightsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("ProfitAndLossSummary")]
        [ProducesResponseType(typeof(ProfitAndLossSummaryQueryResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<ProfitAndLossSummaryQueryResponse>> GetBrandProfitAndLossSummary([FromQuery] ProfitAndLossSummaryQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }
        [AuthorizeRoles(UserRolesEnum.Owner)]
        [HttpGet("BrandSummary")]
        [ProducesResponseType(typeof(DashboardSummaryQueryResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<DashboardSummaryQueryResponse>> GetBrandSummary([FromQuery] DashboardSummaryQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }
    }
}
