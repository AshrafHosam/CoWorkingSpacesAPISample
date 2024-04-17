using API.Attributes;
using Application.Features.BrandCosts.Commands.AddEditBrandCostCategory;
using Application.Features.BrandCosts.Commands.AddOneTimeExpense;
using Application.Features.BrandCosts.Commands.AddRecurringExpense;
using Application.Features.BrandCosts.Commands.DeleteOneTimeExpense;
using Application.Features.BrandCosts.Commands.EditOneTimeExpense;
using Application.Features.BrandCosts.Queries.GetBrandCostCategories;
using Application.Features.BrandCosts.Queries.GetBrandCostCategoryOneTimeExpenses;
using Application.Features.BrandCosts.Queries.GetBrandCostCategoryRecurringExpenses;
using Identity.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AuthorizeRoles(UserRolesEnum.Owner)]
    public class BrandCostController : BaseController
    {
        private readonly IMediator _mediator;

        public BrandCostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddEditBrandCostCategory")]
        [ProducesResponseType(typeof(AddEditBrandCostCategoryCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<AddEditBrandCostCategoryCommandResponse>> AddEditBrandCostCategory(AddEditBrandCostCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [HttpGet("GetBrandCostCategories")]
        [ProducesResponseType(typeof(List<GetBrandCostCategoriesQueryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetBrandCostCategoriesQueryResponse>>> GetBrandCostCategories([FromQuery] GetBrandCostCategoriesQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }

        [HttpPost("AddOneTimeExpense")]
        [ProducesResponseType(typeof(AddOneTimeExpenseCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<AddOneTimeExpenseCommandResponse>> AddOneTimeExpense(AddOneTimeExpenseCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [HttpPut("EditOneTimeExpense")]
        [ProducesResponseType(typeof(EditOneTimeExpenseCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EditOneTimeExpenseCommandResponse>> EditOneTimeExpense(EditOneTimeExpenseCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [HttpDelete("DeleteOneTimeExpense")]
        [ProducesResponseType(typeof(DeleteOneTimeExpenseCommandResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeleteOneTimeExpenseCommandResponse>> DeleteOneTimeExpense(DeleteOneTimeExpenseCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [HttpGet("GetCategoryOneTimeExpenses")]
        [ProducesResponseType(typeof(GetBrandCostCategoryOneTimeExpensesQueryResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetBrandCostCategoryOneTimeExpensesQueryResponse>> GetCategoryOneTimeExpenses([FromQuery] GetBrandCostCategoryOneTimeExpensesQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }

        [HttpPost("AddRecurringExpense")]
        [ProducesResponseType(typeof(AddRecurringExpenseCommandResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<AddRecurringExpenseCommandResponse>> AddRecurringExpense(AddRecurringExpenseCommand command)
        {
            var result = await _mediator.Send(command);

            return GetApiResponse(result);
        }

        [HttpGet("GetCategoryRecurringExpenses")]
        [ProducesResponseType(typeof(GetBrandCostCategoryRecurringExpensesQueryResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetBrandCostCategoryOneTimeExpensesQueryResponse>> GetCategoryRecurringExpenses([FromQuery] GetBrandCostCategoryRecurringExpensesQuery query)
        {
            var result = await _mediator.Send(query);

            return GetApiResponse(result);
        }
    }
}
