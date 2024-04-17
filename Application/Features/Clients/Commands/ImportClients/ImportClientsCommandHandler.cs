using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Response;
using MediatR;

namespace Application.Features.Clients.Commands.ImportClients
{
    internal class ImportClientsCommandHandler : IRequestHandler<ImportClientsCommand, ApiResponse<ImportClientsCommandResponse>>
    {
        private readonly IFileService _fileService;
        private readonly IClientRepo _clientRepo;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public ImportClientsCommandHandler(IFileService fileService, IClientRepo clientRepo, IUserBrandInternalService userBrandInternalService)
        {
            _fileService = fileService;
            _clientRepo = clientRepo;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<ImportClientsCommandResponse>> Handle(ImportClientsCommand request, CancellationToken cancellationToken)
        {
            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != request.BrandId))
                return ApiResponse<ImportClientsCommandResponse>.GetNotFoundApiResponse(error: "User Brand Not Found");

            var importedClients = _fileService.ExportClientDataFromExcel(request.BrandId, request.File);
            if (!importedClients.Any())
                return ApiResponse<ImportClientsCommandResponse>
                    .GetBadRequestApiResponse(error: "No Data Extracted");

            var nonExistingPhoneNumbers = await _clientRepo.FilterExistingPhoneNumbers(request.BrandId, importedClients.Select(a => a.MobileNumber).ToList());

            var clients = importedClients
                .Where(a => nonExistingPhoneNumbers.Contains(a.MobileNumber))
                .ToList();

            if (clients.Any())
            {
                int batchSize = 100;
                int batches = (int)Math.Ceiling((double)clients.Count / batchSize);

                for (int i = 0; i < batches; i++)
                    await _clientRepo.AddRangeAsync(clients.Skip(i * batchSize).Take(batchSize).ToList());
            }

            return ApiResponse<ImportClientsCommandResponse>
                            .GetSuccessApiResponse(new ImportClientsCommandResponse
                            {
                                ImportedClientsCount = clients.Count,
                                DuplicatedClientsCount = importedClients.Count - nonExistingPhoneNumbers.Count
                            });
        }
    }
}
