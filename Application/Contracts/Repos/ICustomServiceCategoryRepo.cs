using Domain.Entities;

namespace Application.Contracts.Repos
{
    public interface ICustomServiceCategoryRepo : IBaseRepo<CustomServiceCategory>
    {
        Task<List<CustomServiceCategory>> GetBrandServiceCategories(Guid brandId);
    }
}
