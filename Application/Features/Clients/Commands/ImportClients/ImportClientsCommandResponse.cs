namespace Application.Features.Clients.Commands.ImportClients
{
    public class ImportClientsCommandResponse
    {
        public int ImportedClientsCount { get; set; } = 0;
        public int DuplicatedClientsCount { get; set; } = 0;
    }
}