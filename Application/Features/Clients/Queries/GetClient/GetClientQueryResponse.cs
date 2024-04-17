namespace Application.Features.Clients.Queries.GetClient
{
    public class GetClientQueryResponse
    {
        public int TotalCount { get; set; }
        public bool HasNextPage { get; set; } = false;
        public List<ClientDto> Clients { get; set; } = new List<ClientDto>();
    }

    public class ClientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string ProfessionalTitle { get; set; }
        public string Interests { get; set; }
        public string Source { get; set; }
        public double VisitedHours { get; set; }
        public int NumberOfReservations { get; set; }
        public int NumberOfVisits { get; set; }
    }
}