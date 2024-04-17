namespace Application.Features.Visits.Queries.GetNonCheckedInClients
{
    public class GetNonCheckedInClientsQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string ProfessionalTitle { get; set; }
        public string Interests { get; set; }
        public string Source { get; set; }
    }
}