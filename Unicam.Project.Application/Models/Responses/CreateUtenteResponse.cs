using Unicam.Project.Models.Entities;

namespace Unicam.Project.Application.Models.Responses
{
    public class CreateUtenteResponse
    {
        public int IdUtente { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public CreateUtenteResponse(Utente utente)
        {
            IdUtente = utente.IdUtente;
            Nome = utente.Nome.Trim();
            Cognome = utente.Cognome.Trim();
            Email = utente.Email.Trim();
        }
    }
}
