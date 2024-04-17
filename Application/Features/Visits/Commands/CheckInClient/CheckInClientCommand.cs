using Application.Response;
using MediatR;

namespace Issentialz.Application.Features.Visits.Commands.CheckInClient
{
    public class CheckInClientCommand : IRequest<ApiResponse<CheckInClientCommandResponse>>
    {
        public Guid? ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhoneNumber { get; set; }
        public Guid BranchId { get; set; }
        public Guid AreaId { get; set; }
    }
}
