using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Clients.Commands.DeleteClient
{
    public class DeleteClientCommand : IRequest<ApiResponse<DeleteClientCommandResponse>>
    {
        [Required]
        public Guid BrandId { get; set; }

        [Required]
        public Guid ClientId { get; set; }
    }
}
