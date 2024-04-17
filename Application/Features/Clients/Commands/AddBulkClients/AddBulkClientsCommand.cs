using Application.Features.Clients.Commands.AddClient;
using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Clients.Commands.AddBulkClients
{
    public class AddBulkClientsCommand : IRequest<ApiResponse<AddBulkClientsCommandResponse>>
    {
        [Required]
        [MinLength(1)]
        public List<AddClientCommand> Clients { get; set; }
    }
}
