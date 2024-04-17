using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.BrandCosts.Queries.GetBrandCostCategories
{
    public class GetBrandCostCategoriesQuery : IRequest<ApiResponse<List<GetBrandCostCategoriesQueryResponse>>>
    {
        [Required]
        public Guid BrandId { get; set; }
    }
}
