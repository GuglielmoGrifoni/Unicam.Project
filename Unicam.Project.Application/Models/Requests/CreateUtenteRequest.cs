using Unicam.Project.Models.Entities;

namespace Unicam.Project.Application.Models.Requests
{
    public class CreateUtenteRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public Utente ToEntity()
        {
            return new Utente
            {
                Nome = this.Nome,
                Cognome = this.Cognome,
                Email = this.Email,
                Password = this.Password
            };
        }
    }
}
