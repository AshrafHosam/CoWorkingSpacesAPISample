using Application.Contracts.Identity;
using Domain.Common;
using Domain.CustomEntities;
using Domain.Entities;
using Domain.Enums;
using Domain.LogEntities;
using Identity.Entities;
using Identity.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Configurations;
using System.Linq.Expressions;

namespace Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        private readonly IClaimService _claimService;
        public AppDbContext(DbContextOptions options, IClaimService claimService) : base(options)
        {
            _claimService = claimService;
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<AreaType> AreaTypes { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<SharedAreaPricingPlan> SharedPricingPlans { get; set; }
        public DbSet<BookableAreaPricingPlan> BookableAreaPricingPlans { get; set; }
        public DbSet<SharedAreaVisit> SharedAreaVisits { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<CustomServiceCategory> CustomServiceCategories { get; set; }
        public DbSet<BrandCostCategory> BrandCostCategories { get; set; }
        public DbSet<OneTimeExpense> OneTimeExpenses { get; set; }
        public DbSet<RecurringExpense> RecurringExpenses { get; set; }
        public DbSet<RecurringExpenseAmount> RecurringExpenseAmounts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ApiResponseLog> ApiResponseLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<AmountsByDateCustomEntity>().HasNoKey();

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetCustomAttributes(typeof(IAuditable), true).Length > 0)
                {
                    builder.Entity(entityType.Name).Property<bool>("IsDeleted");
                }

                var isActiveProperty = entityType.FindProperty("IsDeleted");
                if (isActiveProperty != null && isActiveProperty.ClrType == typeof(bool))
                {
                    var entityBuilder = builder.Entity(entityType.ClrType);
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var methodInfo = typeof(EF).GetMethod(nameof(EF.Property))!.MakeGenericMethod(typeof(bool))!;
                    var efPropertyCall = Expression.Call(null, methodInfo, parameter, Expression.Constant("IsDeleted"));
                    var body = Expression.MakeBinary(ExpressionType.Equal, efPropertyCall, Expression.Constant(false));
                    var expression = Expression.Lambda(body, parameter);
                    entityBuilder.HasQueryFilter(expression);
                }
            }

            Seed(builder);

            AddEntitiesConfigurations(builder);

            base.OnModelCreating(builder);
        }

        private void AddEntitiesConfigurations(ModelBuilder builder)
        {
            builder.AddClientConfiguration();
            builder.AddBrandConfiguration();
            builder.AddApiResponseLogConfiguration();
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            SeedRoles(modelBuilder);

            SeedAreaTypes(modelBuilder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "c7b013f0-5201-4317-abd8-c211f91b7330",
                    Name = nameof(UserRolesEnum.Owner),
                    NormalizedName = nameof(UserRolesEnum.Owner).ToUpper()
                },
                new IdentityRole()
                {
                    Id = "fab4fac1-c546-41de-aebc-a14da6895711",
                    Name = nameof(UserRolesEnum.BranchManager),
                    NormalizedName = nameof(UserRolesEnum.BranchManager).ToUpper()
                },
                new IdentityRole()
                {
                    Id = "fab4fac1-c546-41de-aebc-a14da6895712",
                    Name = nameof(UserRolesEnum.Receptionist),
                    NormalizedName = nameof(UserRolesEnum.Receptionist).ToUpper()
                },
                new IdentityRole()
                {
                    Id = "fab4fac1-c546-41de-aebc-a14da6895713",
                    Name = nameof(UserRolesEnum.BrandClient),
                    NormalizedName = nameof(UserRolesEnum.BrandClient).ToUpper()
                }
                );
        }

        private void SeedAreaTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaType>().HasData(
                            new AreaType
                            {
                                Id = Guid.Parse("2cc71507-07ec-49d5-a8a8-0d1c218b9b39"),
                                Name = AreaTypesEnum.Shared.ToString()
                            },
                            new AreaType
                            {
                                Id = Guid.Parse("4f1c928e-585d-4245-8889-18aa6ba24ccb"),
                                Name = AreaTypesEnum.Office.ToString()
                            },
                            new AreaType
                            {
                                Id = Guid.Parse("2a024be0-9e86-442a-9437-0cb700e638be"),
                                Name = AreaTypesEnum.Room.ToString()
                            },
                            new AreaType
                            {
                                Id = Guid.Parse("55e8a4ec-f516-48fe-aae1-771c4d7205b3"),
                                Name = AreaTypesEnum.Virtual.ToString()
                            },
                            new AreaType
                            {
                                Id = Guid.Parse("7cb8104e-05f0-4ca5-a0c8-160a35f77f9c"),
                                Name = AreaTypesEnum.Event.ToString()
                            }
                        );
        }

        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditable>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTimeOffset.UtcNow;
                        entry.Entity.CreatedBy = _claimService.GetUserId();
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTimeOffset.UtcNow;
                        entry.Entity.UpdatedBy = _claimService.GetUserId();
                        break;
                    case EntityState.Deleted:
                        entry.Entity.DeletedDate = DateTimeOffset.UtcNow;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.DeletedBy = _claimService.GetUserId();
                        entry.State = EntityState.Modified;
                        break;
                }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
