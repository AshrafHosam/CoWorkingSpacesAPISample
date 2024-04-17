using Application.Features.Clients.Commands.AddClient;
using Application.Features.Clients.Queries.GetClient;
using Application.Features.Visits.Queries.GetNonCheckedInClients;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, GetClientQueryResponse>();
            CreateMap<AddClientCommand, Client>();

            CreateMap<Client, GetNonCheckedInClientsQueryResponse>();

            CreateMap<Client, ClientDto>().ReverseMap();
        }
    }
}
