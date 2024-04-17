using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    internal static class ClientConfiguration
    {
        public static void AddClientConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(c =>
            {
                c.HasIndex(u => u.Email);
                c.HasIndex(u => u.MobileNumber);
            });
        }
    }
}
