using Unicam.Project.Application.Models.Dtos;
using Unicam.Project.Application.Models.Responses;
using Unicam.Project.Application.Result;
using Unicam.Project.Models.Entities;

namespace Unicam.Project.Application.Abstractions.Services
{
    public interface IUtenteService
    {
        Task <RegisterResult> CreaUtenteAsync(Utente utente);
        Task<Utente?> AutenticaAsync(string email, string password);
        Task<bool> VerificaEmailEsistenteAsync(string email);
    }
}
