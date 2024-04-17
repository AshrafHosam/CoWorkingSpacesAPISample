using Application.Features.Areas.Commands.CreateArea;
using Application.Features.Areas.Commands.EditArea;
using Application.Features.Areas.Queries.GetArea;
using Application.Features.Areas.Queries.GetAreaTypes;
using Application.Features.Branches.Queries.GetBranchAreas;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.MappingProfiles
{
    public class AreaProfile : Profile
    {
        public AreaProfile()
        {
            var areaTypes = new List<string>
            {
                nameof(AreaTypesEnum.Office),
                nameof(AreaTypesEnum.Room),
                nameof(AreaTypesEnum.Event)
            }
            .Select(a => a.ToLower());

            CreateMap<CreateAreaCommand, Area>();

            CreateMap<AreaType, GetAreaTypesQueryResponse>()
                .ForMember(dest => dest.IsShared, opt => opt.MapFrom(src => src.Name.ToLower() == nameof(AreaTypesEnum.Shared).ToLower()))
                .ForMember(dest => dest.IsBookable, opt => opt.MapFrom(src => areaTypes.Contains(src.Name.ToLower())));

            CreateMap<EditAreaCommand, Area>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AreaId));

            CreateMap<EditAreaCommandResponse, Area>()
                .ReverseMap();

            CreateMap<GetAreaQueryResponse, Area>()
                .ReverseMap();

            CreateMap<Area, GetBranchAreasQueryResponse>();
        }
    }
}
