namespace Application.Features.BrandCustomServices.Queries.GetBrandServiceCategories
{
    public class GetBrandServiceCategoriesQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int BrandServicesCount { get; set; } = 0;
    }
}
