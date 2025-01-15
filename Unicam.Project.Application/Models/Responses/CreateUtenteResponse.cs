using Unicam.Project.Application.Models.Dtos;
using Unicam.Project.Models.Entities;

namespace Unicam.Project.Application.Models.Responses
{
    public class CreateUtenteResponse
    {
        public UtenteDto Utente { get; set; }

        public CreateUtenteResponse(Utente utente)
        {
            Utente = new UtenteDto(utente);
        }
    }
}
