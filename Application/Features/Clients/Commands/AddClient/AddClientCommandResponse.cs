namespace Application.Features.Clients.Commands.AddClient
{
    public class AddClientCommandResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
    }
}
