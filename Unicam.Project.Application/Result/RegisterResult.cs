using Unicam.Project.Application.Models.Responses;

namespace Unicam.Project.Application.Result
{
    public class RegisterResult : BaseResult
    {
        public CreateUtenteResponse? Utente { get; set; }
    }
}
