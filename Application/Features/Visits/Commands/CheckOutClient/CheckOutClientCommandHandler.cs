using Application.Contracts.Repos;
using Application.Response;
using Domain.Entities;
using MediatR;

namespace Application.Features.Visits.Commands.CheckOutClient
{
    internal class CheckOutClientCommandHandler : IRequestHandler<CheckOutClientCommand, ApiResponse<CheckOutClientCommandResponse>>
    {
        private readonly ISharedAreaVisitRepo _sharedAreaVisitRepo;
        public CheckOutClientCommandHandler(ISharedAreaVisitRepo sharedAreaVisitRepo)
        {
            _sharedAreaVisitRepo = sharedAreaVisitRepo;
        }

        public async Task<ApiResponse<CheckOutClientCommandResponse>> Handle(CheckOutClientCommand request, CancellationToken cancellationToken)
        {
            var visit = await _sharedAreaVisitRepo.GetVisitClientAreaPricingIncluded(request.VisitId);

            if (visit is null)
                return ApiResponse<CheckOutClientCommandResponse>.GetNotFoundApiResponse(error: "Visit Not Found");

            if (visit.CheckOutStamp.HasValue)
                return ApiResponse<CheckOutClientCommandResponse>.GetBadRequestApiResponse(error: "This Visit is Checked Out Already");

            CalculateServies(request, visit);

            CalculateTotal(visit, visit.Area.SharedAreaPricingPlanModel);

            if (request.IsSubmitted)
                visit.CheckOutStamp = DateTimeOffset.UtcNow;

            await _sharedAreaVisitRepo.UpdateAsync(visit);

            return ApiResponse<CheckOutClientCommandResponse>
                .GetSuccessApiResponse(new CheckOutClientCommandResponse
                {
                    Total = visit.TotalAmount,
                    AreaId = visit.AreaId,
                    AreaName = visit.Area.Name,
                    ClientEmail = visit.Client.Email,
                    ClientId = visit.Client.Id,
                    ClientName = visit.Client.Name,
                    ClientPhoneNumber = visit.Client.MobileNumber,
                    ClientProfessionalTitle = visit.Client.ProfessionalTitle,
                    ClientNumberOfVisits = visit.Client.Visits.Count(a => a.CheckOutStamp.HasValue),
                });
        }

        private void CalculateTotal(SharedAreaVisit visit, SharedAreaPricingPlan areaPricingPlan)
        {
            TimeSpan timeSpan = DateTimeOffset.UtcNow.Subtract(visit.CheckInStamp);
            double hours = timeSpan.TotalHours;

            if (areaPricingPlan.IsFullDayApplicable && hours >= areaPricingPlan.FullDayHours)
            {
                visit.TotalAmount = (areaPricingPlan.PricePerHour * areaPricingPlan.FullDayHours.Value) + (visit.CustomServices?.Select(a => a.ServicePrice).Sum() ?? 0);
            }
            else
            {
                visit.TotalAmount = (areaPricingPlan.PricePerHour * (decimal)hours) + (visit.CustomServices?.Select(a => a.ServicePrice).Sum() ?? 0);
            }

            visit.TotalAmount = Math.Ceiling(Math.Round(visit.TotalAmount, 2));
        }

        private void CalculateServies(CheckOutClientCommand request, SharedAreaVisit visit)
        {
            foreach (var service in request.Services)
            {
                if (service.Id.HasValue)
                {
                    var visitService = visit.CustomServices.FirstOrDefault(a => a.Id == service.Id);
                    if (visitService != null)
                        visitService.ServicePrice = service.ServicePrice;
                    else
                        visit.CustomServices.Add(new Domain.Entities.CustomService
                        {
                            ServicePrice = service.ServicePrice,
                            ServiceName = service.ServiceName
                        });
                }
                else
                    visit.CustomServices.Add(new Domain.Entities.CustomService
                    {
                        ServicePrice = service.ServicePrice,
                        ServiceName = service.ServiceName
                    });
            }
        }
    }
}
