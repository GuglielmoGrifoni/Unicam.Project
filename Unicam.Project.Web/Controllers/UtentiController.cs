using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unicam.Project.Application.Abstractions.Services;
using Unicam.Project.Application.Factories;
using Unicam.Project.Application.Models.Dtos;
using Unicam.Project.Application.Models.Requests;
using Unicam.Project.Application.Models.Responses;
using Unicam.Project.Application.Services;
using Unicam.Project.Models.Entities;

namespace Unicam.Project.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UtentiController : ControllerBase
    {
        private readonly IUtenteService _utenteService;
        private readonly ITokenService _tokenService;

        public UtentiController(IUtenteService utenteService, ITokenService tokenService)
        {
            _utenteService = utenteService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(CreateUtenteRequest request)
        {
            var utente = request.ToEntity();
            var result = await _utenteService.CreaUtenteAsync(utente);

            if (!result.Success)
            {
                return Conflict(ResponseFactory.WithError(result.ErrorMessage));
            }

            var response = new CreateUtenteResponse(result.Utente);

            return Ok(ResponseFactory.WithSuccess(response));
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var utente = await _utenteService.AutenticaAsync(request.Email, request.Password);
            if (utente == null)
                return Unauthorized(ResponseFactory.WithError("Credenziali non valide."));

            // In questa parte si genera il token che servirà per accedere alle altre funzionalità del sistema
            var token = _tokenService.CreateToken(new CreateTokenRequest
            {
                IdUtente = utente.IdUtente,
                Email = utente.Email,
                Nome = utente.Nome,
                Cognome = utente.Cognome
            });

            // Creazione della response con il token
            var response = new CreateTokenResponse(token);

            return Ok(ResponseFactory.WithSuccess(response));
        }
    }
}
