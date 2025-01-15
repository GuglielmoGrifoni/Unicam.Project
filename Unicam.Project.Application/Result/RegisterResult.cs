using Unicam.Project.Application.Models.Dtos;
using Unicam.Project.Models.Entities;

namespace Unicam.Project.Application.Result
{
    public class RegisterResult : BaseResult
    {
        public Utente? Utente { get; set; }
    }
}
