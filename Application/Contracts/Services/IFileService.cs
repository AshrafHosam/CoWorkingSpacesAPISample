using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Contracts.Services
{
    public interface IFileService
    {
        List<Client> ExportClientDataFromExcel(Guid brandId, IFormFile file);
    }
}
