using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Clients.Commands.EditClient
{
    public class EditClientCommand : IRequest<ApiResponse<EditClientCommandResponse>>
    {
        [Required]
        public Guid BrandId { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string MobileNumber { get; set; }
        public string ProfessionalTitle { get; set; }
        public string Interests { get; set; }
        public string Source { get; set; }
    }
}
