using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unicam.Project.Application.Abstractions.Services;
using Unicam.Project.Application.Factories;
using Unicam.Project.Application.Models.Requests;
using Unicam.Project.Application.Models.Responses;
using Unicam.Project.Models.Entities;
using Unicam.Project.Models.Filtro;

namespace Unicam.Project.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LibroController : ControllerBase
    {
        private readonly ILibroService _libroService;

        public LibroController(ILibroService libroService)
        {
            _libroService = libroService;
        }


        [HttpPost]
        [Route("aggiungi")]
        public async Task<IActionResult> CaricaLibroAsync(CreateLibroRequest request)
        {
            var idUtente = int.Parse(User.FindFirst("id_utente")?.Value ?? "0");

            var libro = request.ToEntity();

            libro.IdUtente = idUtente;

            var risultato = await _libroService.CaricaLibroAsync(libro, request.CategorieNomi);

            if (!risultato.Success)
            {
                if (risultato.CategorieNonTrovate != null)
                {
                    return BadRequest(ResponseFactory.WithError(
                        $"Le seguenti categorie non esistono: {string.Join(", ", risultato.CategorieNonTrovate)}"));
                }

                return Conflict(ResponseFactory.WithError(risultato.ErrorMessage));
            }

            var response = new LibroResponse(libro);

            return Ok(ResponseFactory.WithSuccess(response));
        }


        [HttpPut]
        [Route("modifica/{id}")]
        public async Task<IActionResult> ModificaLibroAsync(int id, UpdateLibroRequest request)
        {
            var idUtente = int.Parse(User.FindFirst("id_utente")?.Value ?? "0");

            var risultato = await _libroService.ModificaLibroAsync(id, request, idUtente);

            if (!risultato.Success)
            {
                return BadRequest(ResponseFactory.WithError(risultato.ErrorMessage));
            }

            var response = new LibroResponse(risultato.Libro);

            return Ok(ResponseFactory.WithSuccess(response));
        }

        [HttpDelete]
        [Route("elimina/{id}")]
        public async Task<IActionResult> EliminaLibroAsync(int id)
        {
            var idUtente = int.Parse(User.FindFirst("id_utente")?.Value ?? "0");

            var risultato = await _libroService.EliminaLibroAsync(id, idUtente);

            if (!risultato.Success)
                return BadRequest(ResponseFactory.WithError(risultato.ErrorMessage));

            return Ok(ResponseFactory.WithSuccess("Libro eliminato con successo."));
        }

        [HttpGet]
        [Route("ricerca")]
        public async Task<IActionResult> RicercaLibriAsync([FromQuery] LibroFiltri filtri, int pagina = 1, int dimensionePagina = 10)
        {
            // Con questo if si verifica che almeno un filtro sia stato fornito
            if (string.IsNullOrWhiteSpace(filtri.Nome) &&
                string.IsNullOrWhiteSpace(filtri.Autore) &&
                !filtri.DataPubblicazione.HasValue &&
                (filtri.CategorieNomi == null || !filtri.CategorieNomi.Any()))
            {
                return BadRequest(ResponseFactory.WithError("Devi specificare almeno un filtro per la ricerca."));
            }

            // Con questa condizione si verifica che pagina e dimensionePagina siano validi
            if (pagina <= 0 || dimensionePagina <= 0)
            {
                return BadRequest(ResponseFactory.WithError("I parametri 'pagina' e 'dimensionePagina' devono essere maggiori di zero."));
            }

            var idUtente = int.Parse(User.FindFirst("id_utente")?.Value ?? "0");

            var risultato = await _libroService.RicercaLibriAsync(filtri, idUtente, pagina, dimensionePagina);

            var response = risultato.Libri.Select(libro => new LibroResponse(libro)).ToList();

            return Ok(ResponseFactory.WithSuccess(new { totaleRisultati = risultato.TotalNum, response }));
        }
    }
}
