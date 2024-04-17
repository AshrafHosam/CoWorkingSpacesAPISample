using Application.Features.Brands.Queries.GetUserBrand;
using Application.Response;

namespace Application.Contracts.Services
{
    public interface IUserBrandInternalService
    {
        Task<ApiResponse<GetUserBrandQueryResponse>> GetLoggedInUserBrand();
    }
}
