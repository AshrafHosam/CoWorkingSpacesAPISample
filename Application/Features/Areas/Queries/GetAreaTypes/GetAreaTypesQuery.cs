using Application.Contracts.Helpers;
using Application.Response;
using MediatR;

namespace Application.Features.Areas.Queries.GetAreaTypes
{
    public class GetAreaTypesQuery : IRequest<ApiResponse<List<GetAreaTypesQueryResponse>>>, ICacheableRequest
    {
        public double GetCacheDurationInMinutes() => 60;

        public string GetCacheKey() => $"{nameof(GetAreaTypesQuery)}";
    }
}
