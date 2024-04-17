using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.BrandCosts.Queries.GetBrandCostCategoryRecurringExpenses
{
    public class GetBrandCostCategoryRecurringExpensesQuery : IRequest<ApiResponse<GetBrandCostCategoryRecurringExpensesQueryResponse>>
    {
        [Required]
        public Guid CostCategoryId { get; set; }
        [Required]
        public Guid BrandId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
