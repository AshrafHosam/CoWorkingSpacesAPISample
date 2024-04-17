namespace Application.Features.Visits.Commands.CheckOutClient
{
    public class CheckOutClientCommandResponse
    {
        public Guid AreaId { get; set; }
        public string AreaName { get; set; }
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientEmail { get; set; }
        public string ClientProfessionalTitle { get; set; }
        public decimal Total { get; set; }
        public int ClientNumberOfVisits { get; set; }
    }
}