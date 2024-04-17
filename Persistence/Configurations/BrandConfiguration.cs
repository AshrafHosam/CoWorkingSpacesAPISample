using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    internal static class BrandConfiguration
    {
        public static void AddBrandConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(c =>
            {
                c.HasIndex(u => u.OwnerId);
            });
        }
    }
}
