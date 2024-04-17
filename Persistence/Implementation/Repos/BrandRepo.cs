using Application.Contracts.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Implementation.Repos
{
    public class BrandRepo : BaseRepo<Brand>, IBrandRepo
    {
        public BrandRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<Brand> GetBrandByOwnerId(Guid ownerId)
        {
            return await _context.Brands
                .AsNoTracking()
                .Where(a => a.OwnerId == ownerId)
                .Select(a =>
                new Brand
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description
                })
                .FirstOrDefaultAsync();
        }
    }
}
