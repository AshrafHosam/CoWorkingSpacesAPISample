using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.BrandCustomServices.Commands.AddEditCustomServiceCategory
{
    public class AddEditCustomServiceCategoryCommand : IRequest<ApiResponse<AddEditCustomServiceCategoryCommandResponse>>
    {
        [Required]
        public Guid BrandId { get; set; }
        public Guid? CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}
