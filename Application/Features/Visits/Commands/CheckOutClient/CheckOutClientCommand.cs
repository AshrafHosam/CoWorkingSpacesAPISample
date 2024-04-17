using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Visits.Commands.CheckOutClient
{
    public class CheckOutClientCommand : IRequest<ApiResponse<CheckOutClientCommandResponse>>
    {
        [Required]
        public Guid VisitId { get; set; }
        public bool IsSubmitted { get; set; } = false;
        public List<CheckOutCommandService> Services { get; set; } = new List<CheckOutCommandService>();
    }

    public class CheckOutCommandService
    {
        public Guid? Id { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
    }
}
