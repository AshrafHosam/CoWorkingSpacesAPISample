using Application.Contracts.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Implementation.Repos
{
    public class CustomServiceCategoryRepo : BaseRepo<CustomServiceCategory>, ICustomServiceCategoryRepo
    {
        public CustomServiceCategoryRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<List<CustomServiceCategory>> GetBrandServiceCategories(Guid brandId)
        {
            return await _context.CustomServiceCategories
                .Where(a => a.BrandId == brandId)
                .ToListAsync();
        }
    }
}
