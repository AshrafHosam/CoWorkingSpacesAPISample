namespace Application.Features.Areas.Queries.GetAreaTypes
{
    public class GetAreaTypesQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsShared { get; set; }
        public bool IsBookable { get; set; }
    }
}
