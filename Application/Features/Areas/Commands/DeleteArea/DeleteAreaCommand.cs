using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Areas.Commands.DeleteArea
{
    public class DeleteAreaCommand : IRequest<ApiResponse<DeleteAreaCommandResponse>>
    {
        [Required]
        public Guid AreaId { get; set; }
    }
}
