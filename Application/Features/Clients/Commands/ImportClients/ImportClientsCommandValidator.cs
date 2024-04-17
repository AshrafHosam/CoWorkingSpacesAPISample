using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Clients.Commands.ImportClients
{
    public class ImportClientsCommandValidator : AbstractValidator<ImportClientsCommand>
    {
        public ImportClientsCommandValidator()
        {
            RuleFor(a => a.File)
                .NotEmpty()
                .NotNull()
                .Must(MustBeExcelFile)
                .WithMessage("Wrong Extension");

            RuleFor(a => a.BrandId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
        }
        private bool MustBeExcelFile(IFormFile file)
        {
            var allowedExtensions = new[] { ".xlsx", ".xls", ".xlsm" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(fileExtension);
        }
    }
}
