using Unicam.Project.Application.Abstractions.Services;
using Unicam.Project.Application.Models.Dtos;
using Unicam.Project.Application.Models.Responses;
using Unicam.Project.Application.Result;
using Unicam.Project.Models.Entities;
using Unicam.Project.Models.Repository;

namespace Unicam.Project.Application.Services
{
    public class UtenteService : IUtenteService
    {
        private readonly UtenteRepository _utenteRepository;

        public UtenteService(UtenteRepository utenteRepository)
        {
            _utenteRepository = utenteRepository;
        }

        public async Task<RegisterResult> CreaUtenteAsync(Utente utente)
        {
            if (await _utenteRepository.EmailExistsAsync(utente.Email))
            {
                return new RegisterResult
                {
                    Success = false,
                    ErrorMessage = "Email già registrata."
                };
            }

            utente.Password = HashPassword(utente.Password);

            await _utenteRepository.AggiungiAsync(utente);
            await _utenteRepository.SaveAsync();

            return new RegisterResult
            {
                Success = true,
                Utente = utente
            };
        }

        public async Task<Utente?> AutenticaAsync(string email, string password)
        {
            var utente = await _utenteRepository.GetUtenteByEmailAsync(email);

            if (utente != null && VerificaPassword(password, utente.Password))
                return utente;

            return null;
        }

        public async Task<bool> VerificaEmailEsistenteAsync (string email)
        {
            return await _utenteRepository.EmailExistsAsync(email);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerificaPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash.Trim());
        }
    }
}
