using Application.Features.Reservations.Commands.EditReservation;
using Application.Features.Reservations.Queries.GetAllReservations;
using Application.Features.Reservations.Queries.GetClientReservations;
using Application.Features.Reservations.Queries.GetReservation;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<EditReservationCommand, Reservation>();
            CreateMap<Reservation, GetAllReservationsQueryResponse>()
             .ForMember(dest => dest.IsAllDay, opt => opt.MapFrom(src => src.IsDailyReservation || src.IsMonthlyReservation));
            CreateMap<Reservation, GetClientReservationsQueryResponse>()
             .ForMember(dest => dest.IsAllDay, opt => opt.MapFrom(src => src.IsDailyReservation || src.IsMonthlyReservation));
            CreateMap<Reservation, GetReservationQueryResponse>()
              .ForMember(dest => dest.IsAllDay, opt => opt.MapFrom(src => src.IsDailyReservation || src.IsMonthlyReservation));
        }
    }
}
