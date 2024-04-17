using Application.Contracts.Helpers;
using Application.Response;
using MediatR;

namespace Application.Features.Brands.Queries.GetUserBrand
{
    public class GetUserBrandQuery : IRequest<ApiResponse<GetUserBrandQueryResponse>>, ICacheableRequest
    {
        public GetUserBrandQuery(Guid userId)
        {
            UserId = userId;
        }
        public Guid UserId { get; set; }
        public double GetCacheDurationInMinutes() => 60;

        public string GetCacheKey() => $"{nameof(GetUserBrandQuery)}_{UserId}";
    }
}
