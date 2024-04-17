using Application.Features.BrandCosts.Commands.AddEditBrandCostCategory;
using Application.Features.BrandCosts.Queries.GetBrandCostCategories;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class BrandCostProfile : Profile
    {
        public BrandCostProfile()
        {
            CreateMap<AddEditBrandCostCategoryCommand, BrandCostCategory>()
                .ReverseMap();

            CreateMap<BrandCostCategory, AddEditBrandCostCategoryCommandResponse>();

            CreateMap<BrandCostCategory, GetBrandCostCategoriesQueryResponse>();
        }
    }
}
