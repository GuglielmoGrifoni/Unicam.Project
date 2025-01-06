using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unicam.Project.Application.Abstractions.Services;
using Unicam.Project.Application.Factories;
using Unicam.Project.Application.Models.Requests;
using Unicam.Project.Application.Models.Responses;

namespace Unicam.Project.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpPost]
        [Route("crea")]
        public async Task<IActionResult> CreaCategoriaAsync(CreateCategoriaRequest request)
        {
            var idUtente = int.Parse(User.FindFirst("id_utente")?.Value ?? "0");

            var risultato = await _categoriaService.CreaCategoriaAsync(request.Nome, idUtente);

            if (!risultato.Success)
                return Conflict(ResponseFactory.WithError(risultato.ErrorMessage));

            var response = new CategoriaResponse(risultato.Categoria);

            return Ok(ResponseFactory.WithSuccess(response));
        }

        [HttpDelete]
        [Route("elimina")]
        public async Task<IActionResult> EliminaCategoriaAsync(DeleteCategoriaRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nome))
                return BadRequest(ResponseFactory.WithError("Il nome della categoria è obbligatorio."));

            var idUtente = int.Parse(User.FindFirst("id_utente")?.Value ?? "0");

            var risultato = await _categoriaService.EliminaCategoriaAsync(request.Nome, idUtente);

            if (!risultato.Success)
                return BadRequest(ResponseFactory.WithError(risultato.ErrorMessage));

            return Ok(ResponseFactory.WithSuccess("Categoria eliminata con successo."));
        }


        [HttpGet]
        [Route("ottieni-tutte")]
        public async Task<IActionResult> OttieniTutteLeCategorieAsync()
        {
            var idUtente = int.Parse(User.FindFirst("id_utente")?.Value ?? "0");

            var categorie = await _categoriaService.GetAllCategorieAsync(idUtente);

            if (categorie == null || !categorie.Any())
                return NotFound(ResponseFactory.WithError("Nessuna categoria trovata."));

            var response = categorie.Select(c => new CategoriaResponse(c)).ToList();

            return Ok(ResponseFactory.WithSuccess(response));
        }

    }
}
