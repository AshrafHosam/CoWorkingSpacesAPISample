using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand : IRequest<ApiResponse<CreateBrandCommandResponse>>
    {
        [Required, MaxLength(124)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public Guid? OwnerId { get; set; }
    }
}
