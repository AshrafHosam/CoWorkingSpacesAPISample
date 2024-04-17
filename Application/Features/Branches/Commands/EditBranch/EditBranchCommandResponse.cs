namespace Application.Features.Branches.Commands.EditBranch
{
    public class EditBranchCommandResponse
    {
        public Guid BranchId { get; set; }
        public string Name { get; set; }
        public Guid BrandId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string LocationUrl { get; set; }
    }
}