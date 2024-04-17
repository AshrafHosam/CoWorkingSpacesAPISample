using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.BrandCosts.Queries.GetBrandCostCategoryOneTimeExpenses
{
    public class GetBrandCostCategoryOneTimeExpensesQuery : IRequest<ApiResponse<GetBrandCostCategoryOneTimeExpensesQueryResponse>>
    {
        public Guid? CostCategoryId { get; set; }
        [Required]
        public Guid BrandId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset? ToDate { get; set; }
        public Guid? BranchId { get; set; }
    }
}
