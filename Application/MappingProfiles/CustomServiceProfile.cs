using Application.Features.BrandCustomServices.Commands.AddEditCustomServiceCategory;
using Application.Features.BrandCustomServices.Queries.GetBrandServiceCategories;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class CustomServiceProfile : Profile
    {
        public CustomServiceProfile()
        {
            CreateMap<AddEditCustomServiceCategoryCommand, CustomServiceCategory>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName));

            CreateMap<AddEditCustomServiceCategoryCommandResponse, CustomServiceCategory>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName))
                   .ReverseMap();

            CreateMap<CustomServiceCategory, GetBrandServiceCategoriesQueryResponse>()
                .ForMember(dest => dest.BrandServicesCount, opt => opt.MapFrom(src => src.CustomServices.Count));

        }
    }
}
