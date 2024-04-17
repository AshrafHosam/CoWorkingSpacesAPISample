using Application.MappingProfiles;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace Application.Tests
{
    [ExcludeFromCodeCoverage]
    internal class MapperConfig
    {
        private MapperConfig()
        {
        }

        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<VisitProfile>();
                c.AddProfile<BrandProfile>();
                c.AddProfile<BranchProfile>();
                c.AddProfile<AreaProfile>();
                c.AddProfile<CustomServiceProfile>();
                c.AddProfile<BrandCostProfile>();
                c.AddProfile<ClientProfile>();
            });
            return config.CreateMapper();
        }
    }
}
