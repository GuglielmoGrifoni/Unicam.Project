namespace Unicam.Project.Application.Models.Requests
{
    public class CreateTokenRequest
    {
        public int IdUtente { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
    }
}
