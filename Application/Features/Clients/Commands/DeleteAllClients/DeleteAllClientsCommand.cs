using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Clients.Commands.DeleteAllClients
{
    public class DeleteAllClientsCommand : IRequest<ApiResponse<DeleteAllClientsCommandResponse>>
    {
        [Required]
        public Guid BrandId { get; set; }
    }
}
