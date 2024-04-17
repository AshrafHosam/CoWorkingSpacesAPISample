namespace Application.Features.Branches.Queries.GetBranches
{
    public class GetBranchesQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string LocationUrl { get; set; }
    }
}