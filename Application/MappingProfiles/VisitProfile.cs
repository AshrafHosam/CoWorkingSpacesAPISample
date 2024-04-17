using AutoMapper;
using Domain.Entities;
using Issentialz.Application.Features.Visits.Queries.GetCheckedInClients;

namespace Application.MappingProfiles
{
    public class VisitProfile : Profile
    {
        public VisitProfile()
        {
            CreateMap<SharedAreaVisit, GetCheckedInClientsQueryResponse>()
                .ForMember(dst => dst.ClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dst => dst.ClientName, opt => opt.MapFrom(src => src.Client.Name))
                .ForMember(dst => dst.ClientEmail, opt => opt.MapFrom(src => src.Client.Email))
                .ForMember(dst => dst.ClientMobileNumber, opt => opt.MapFrom(src => src.Client.MobileNumber))
                .ForMember(dst => dst.CheckInStamp, opt => opt.MapFrom(src => src.CheckInStamp))
                .ForMember(dst => dst.VisitId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.AreaId, opt => opt.MapFrom(src => src.AreaId))
                .ForMember(dst => dst.AreaName, opt => opt.MapFrom(src => src.Area.Name))
                .ForMember(dst => dst.ConsumedServices, opt => opt.MapFrom(src => src.CustomServices))
                .ReverseMap();

            CreateMap<CustomService, ConsumedService>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.ServiceName, opt => opt.MapFrom(src => src.ServiceName))
                .ForMember(dst => dst.ServicePrice, opt => opt.MapFrom(src => src.ServicePrice))
                .ReverseMap();
        }
    }
}
