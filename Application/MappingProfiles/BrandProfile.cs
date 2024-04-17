using Application.Contracts.Identity;
using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Queries.GetUserBrand;
using AutoMapper;
using Domain.Entities;
using Issentialz.Application.Features.Visits.Queries.GetCheckedInClients;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Application.MappingProfiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<CreateBrandCommand, Brand>();

            CreateMap<Brand, GetUserBrandQueryResponse>();
        }
        private byte[] GetFileBytes(IFormFile image)
        {
            if (image != null)
            {
                byte[] fileByteArray;
                using (var item = new MemoryStream())
                {
                    image.CopyTo(item);
                    fileByteArray = item.ToArray();
                }
                return fileByteArray;
            }
            return null;
        }
    }
}
