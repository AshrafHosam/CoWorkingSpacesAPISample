using Domain.Entities;

namespace Application.Contracts.Repos
{
    public interface IBrandCostCategoryRepo : IBaseRepo<BrandCostCategory>
    {
        Task<List<BrandCostCategory>> GetBrandCostCategories(Guid brandId);
    }
}
