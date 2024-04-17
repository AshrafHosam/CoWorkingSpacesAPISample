using Application.Contracts.Services;
using Application.Features.Clients.Commands.ImportClients;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace Persistence.Implementation.Services
{
    internal class FileService : IFileService
    {
        public List<Client> ExportClientDataFromExcel(Guid brandId, IFormFile file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var clients = new List<Client>();
            var excelPackage = new ExcelPackage(file.OpenReadStream());

            var worksheet = excelPackage.Workbook.Worksheets.First();

            var rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                clients.Add(new Client
                {
                    BrandId = brandId,
                    Name = worksheet.Cells[row, ((int)ExcelColumnsEnum.Name)].Value?.ToString(),
                    Email = worksheet.Cells[row, ((int)ExcelColumnsEnum.Email)].Value?.ToString().ToLower(),
                    MobileNumber = worksheet.Cells[row, ((int)ExcelColumnsEnum.MobileNumber)].Value?.ToString(),
                    ProfessionalTitle = worksheet.Cells[row, ((int)ExcelColumnsEnum.Title)].Value?.ToString(),
                    Interests = worksheet.Cells[row, ((int)ExcelColumnsEnum.Interests)].Value?.ToString()
                });
            }

            return clients
               .Where(a => !string.IsNullOrEmpty(a.Name)
               && (!string.IsNullOrEmpty(a.MobileNumber) || !string.IsNullOrEmpty(a.Email)))
               .GroupBy(c => new { c.MobileNumber, c.Email })
               .Select(group => group.First())
               .ToList();
        }
    }
}
