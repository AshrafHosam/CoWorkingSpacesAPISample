using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.BrandCosts.Commands.AddEditBrandCostCategory
{
    public class AddEditBrandCostCategoryCommand : IRequest<ApiResponse<AddEditBrandCostCategoryCommandResponse>>
    {
        public Guid? Id { get; set; }
        [Required]
        public Guid BrandId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
