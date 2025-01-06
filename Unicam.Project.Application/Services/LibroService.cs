using System.Linq;
using Microsoft.Graph.Models;
using Unicam.Project.Application.Abstractions.Services;
using Unicam.Project.Application.Models.Requests;
using Unicam.Project.Application.Result;
using Unicam.Project.Models.Entities;
using Unicam.Project.Models.Filtro;
using Unicam.Project.Models.Repository;

namespace Unicam.Project.Application.Services
{
    public class LibroService : ILibroService
    {
        private readonly LibroRepository _libroRepository;
        private readonly ICategoriaService _categoriaService;

        public LibroService(LibroRepository libroRepository, ICategoriaService categoriaService)
        {
            _libroRepository = libroRepository;
            _categoriaService = categoriaService;
        }

        public async Task<CaricaLibroResult> CaricaLibroAsync(Libro libro, List<string> nomiCategorie)
        {
            // Qui si verifica se un libro con lo stesso nome, autore e data di pubblicazione esiste già
            var libroEsistente = await _libroRepository.GetLibroPerProprietaAsync(libro.Nome, libro.Autore, libro.DataPubblicazione, libro.IdUtente);
            if (libroEsistente != null)
            {
                return new CaricaLibroResult
                {
                    Success = false,
                    ErrorMessage = $"Il libro '{libro.Nome}' di '{libro.Autore}' pubblicato il '{libro.DataPubblicazione:dd/MM/yyyy}' esiste già."
                };
            }

            // In questa parte si verifica se tutte le categorie esistono
            var categorieEsistenti = await _categoriaService.GetCategorieByNomiAsync(nomiCategorie, libro.IdUtente);
            var categorieNonTrovate = nomiCategorie
                .Select(n => n.Trim().ToLower())
                .Except(categorieEsistenti.Select(c => c.Nome.Trim()), StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (categorieNonTrovate.Any())
            {
                return new CaricaLibroResult
                {
                    Success = false,
                    CategorieNonTrovate = categorieNonTrovate,
                    ErrorMessage = "Alcune categorie non esistono."
                };
            }

            libro.LibriCategorie = categorieEsistenti.Select(c => new LibroCategoria { Categoria = c }).ToList();

            await _libroRepository.AggiungiAsync(libro);
            await _libroRepository.SaveAsync();

            return new CaricaLibroResult { Success = true };
        }

        public async Task<ModificaLibroResult> ModificaLibroAsync(int id, UpdateLibroRequest request, int idUtente)
        {
            var libro = await _libroRepository.GetLibroConCategorieAsync(id, idUtente);
            if (libro == null || libro.IdUtente != idUtente)
            {
                return new ModificaLibroResult
                {
                    Success = false,
                    ErrorMessage = "Il libro specificato non esiste o non appartiene all'utente corrente."
                };
            }

            // Questi if servono per aggiornare i campi del libro qualora venissero forniti nella richiesta
            if (!string.IsNullOrWhiteSpace(request.Nome))
                libro.Nome = request.Nome.Trim();

            if (!string.IsNullOrWhiteSpace(request.Autore))
                libro.Autore = request.Autore.Trim();

            if (!string.IsNullOrWhiteSpace(request.Editore))
                libro.Editore = request.Editore.Trim();

            if (request.DataPubblicazione.HasValue)
                libro.DataPubblicazione = request.DataPubblicazione.Value;

            if (request.CategorieNomi != null)
            {
                // Se l'array è vuoto, si ignorano le modifiche alle categorie
                if (request.CategorieNomi.Any(c => !string.IsNullOrWhiteSpace(c)))
                {
                    var categorieEsistenti = await _categoriaService.GetCategorieByNomiAsync(request.CategorieNomi, idUtente);
                    var categorieNonTrovate = request.CategorieNomi
                        .Where(c => !string.IsNullOrWhiteSpace(c))
                        .Except(categorieEsistenti.Select(c => c.Nome.Trim()), StringComparer.OrdinalIgnoreCase)
                        .ToList();

                    if (categorieNonTrovate.Any())
                    {
                        return new ModificaLibroResult
                        {
                            Success = false,
                            ErrorMessage = $"Le seguenti categorie non esistono: {string.Join(", ", categorieNonTrovate)}"
                        };
                    }

                    // Da qui si aggiornano le varie categorie presenti nel libro scelto
                    var categorieDaAggiungere = categorieEsistenti
                        .Where(c => libro.LibriCategorie.All(lc => lc.Categoria.IdCategoria != c.IdCategoria))
                        .ToList();

                    var categorieDaRimuovere = libro.LibriCategorie
                        .Where(lc => categorieEsistenti.All(c => c.IdCategoria != lc.Categoria.IdCategoria))
                        .ToList();

                    foreach (var categoria in categorieDaAggiungere)
                    {
                        libro.LibriCategorie.Add(new LibroCategoria { Categoria = categoria });
                    }

                    foreach (var categoriaDaRimuovere in categorieDaRimuovere)
                    {
                        libro.LibriCategorie.Remove(categoriaDaRimuovere);
                    }
                }
            }

            _libroRepository.Modifica(libro);
            await _libroRepository.SaveAsync();

            return new ModificaLibroResult
            {
                Success = true,
                Libro = libro
            };
        }

        public async Task<EliminaLibroResult> EliminaLibroAsync(int libroId, int idUtente)
        {
            var libro = await _libroRepository.GetLibroConCategorieAsync(libroId, idUtente);
            if (libro == null)
            {
                return new EliminaLibroResult
                {
                    Success = false,
                    ErrorMessage = "Il libro specificato non esiste o non appartiene all'utente corrente."
                };
            }

            if (libro.LibriCategorie != null && libro.LibriCategorie.Any())
            {
                await _libroRepository.RemoveCategorieDaLibroAsync(libroId, idUtente);
            }

            await _libroRepository.EliminaAsync(libroId);
            await _libroRepository.SaveAsync();

            return new EliminaLibroResult { Success = true };
        }

        public async Task<CercaLibriResult> RicercaLibriAsync(LibroFiltri filtri, int idUtente, int pagina, int dimensionePagina)
        {
            List<int>? IdCategorie = null;
            if (filtri.CategorieNomi != null && filtri.CategorieNomi.Any())
            {
                var categorieEsistenti = await _categoriaService.GetCategorieByNomiAsync(filtri.CategorieNomi, idUtente);
                IdCategorie = categorieEsistenti.Select(c => c.IdCategoria).ToList();

                if (!IdCategorie.Any())
                {
                    return new CercaLibriResult
                    {
                        Libri = new List<Libro>(),
                        TotalNum = 0
                    };
                }
            }

            int offset = (pagina - 1) * dimensionePagina;

            var result = await _libroRepository.CercaLibriAsync(
                filtri.Nome,
                filtri.Autore,
                filtri.DataPubblicazione,
                IdCategorie,
                idUtente,
                offset,
                dimensionePagina);

            return new CercaLibriResult
            {
                Libri = result.Libri,
                TotalNum = result.TotalNum
            };
        }

    }

}
