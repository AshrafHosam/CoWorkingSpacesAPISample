using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clients.Commands.AddClient
{
    public class AddClientCommand : IRequest<ApiResponse<AddClientCommandResponse>>
    {
        [Required]
        public Guid BrandId { get; set; }

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
