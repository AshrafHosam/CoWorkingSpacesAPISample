using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.BrandCustomServices.Queries.GetBrandServiceCategories
{
    public class GetBrandServiceCategoriesQuery : IRequest<ApiResponse<List<GetBrandServiceCategoriesQueryResponse>>>
    {
        [Required]
        public Guid BrandId { get; set; }
    }
}
