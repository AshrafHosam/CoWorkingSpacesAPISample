using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Clients.Commands.ImportClients
{
    public class ImportClientsCommand : IRequest<ApiResponse<ImportClientsCommandResponse>>
    {
        [Required]
        public Guid BrandId { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
