using Application.Features.Branches.Commands.CreateBranch;
using Application.Features.Branches.Queries.GetBranches;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class BranchProfile : Profile
    {
        public BranchProfile()
        {
            CreateMap<CreateBranchCommand, Branch>();

            CreateMap<Branch, GetBranchesQueryResponse>();
        }
    }
}
