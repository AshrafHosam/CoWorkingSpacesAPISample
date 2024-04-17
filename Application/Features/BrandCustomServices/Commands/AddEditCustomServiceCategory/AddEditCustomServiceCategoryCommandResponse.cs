using System.ComponentModel.DataAnnotations;

namespace Application.Features.BrandCustomServices.Commands.AddEditCustomServiceCategory
{
    public class AddEditCustomServiceCategoryCommandResponse
    {

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}