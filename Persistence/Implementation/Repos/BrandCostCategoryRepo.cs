using Application.Contracts.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Implementation.Repos
{
    public class BrandCostCategoryRepo : BaseRepo<BrandCostCategory>, IBrandCostCategoryRepo
    {
        public BrandCostCategoryRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<List<BrandCostCategory>> GetBrandCostCategories(Guid brandId)
        {
            return await _context.BrandCostCategories
                .Where(a => a.BrandId == brandId)
                .ToListAsync();
        }
    }
}
