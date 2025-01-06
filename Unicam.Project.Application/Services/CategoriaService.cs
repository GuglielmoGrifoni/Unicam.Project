using Unicam.Project.Application.Abstractions.Services;
using Unicam.Project.Application.Result;
using Unicam.Project.Models.Entities;
using Unicam.Project.Models.Repository;

namespace Unicam.Project.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriaService(CategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<List<Categoria>> GetCategorieByNomiAsync(List<string> nomiCategorie, int idUtente)
        {
            return await _categoriaRepository.GetCategorieByNomiAsync(nomiCategorie, idUtente);
        }

        public async Task<List<Categoria>> GetAllCategorieAsync(int idUtente)
        {
            return await _categoriaRepository.GetAllCategorieAsync(idUtente);
        }

        public async Task<bool> EsisteCategoriaAsync(string nome, int idUtente)
        {
            return await _categoriaRepository.EsisteCategoriaAsync(nome, idUtente);
        }

        public async Task<bool> PuòEliminareAsync(string nome, int idUtente)
        {
            return await _categoriaRepository.PuòEliminareAsync(nome, idUtente);
        }

        public async Task <CreaCategoriaResult> CreaCategoriaAsync(string nome, int idUtente)
        {
            if (await _categoriaRepository.EsisteCategoriaAsync(nome, idUtente))
            {
                return new CreaCategoriaResult
                {
                    Success = false,
                    ErrorMessage = $"La categoria '{nome}' esiste già per l'utente corrente."
                };
            }

            var nuovaCategoria = new Categoria
            {
                Nome = nome,
                IdUtente = idUtente
            };

            await _categoriaRepository.AggiungiAsync(nuovaCategoria);
            await _categoriaRepository.SaveAsync();

            return new CreaCategoriaResult
            {
                Success = true,
                Categoria = nuovaCategoria
            };
        }

        public async Task<EliminaCategoriaResult> EliminaCategoriaAsync(string nome, int idUtente)
        {
            if (!await _categoriaRepository.EsisteCategoriaAsync(nome, idUtente))
            {
                return new EliminaCategoriaResult
                {
                    Success = false,
                    ErrorMessage = $"La categoria '{nome}' non esiste per l'utente corrente."
                };
            }

            if (!await _categoriaRepository.PuòEliminareAsync(nome, idUtente))
            {
                return new EliminaCategoriaResult
                {
                    Success = false,
                    ErrorMessage = $"La categoria '{nome}' non può essere eliminata poiché è associata a uno o più libri."
                };
            }

            await _categoriaRepository.EliminaAsync(nome, idUtente);
            return new EliminaCategoriaResult { Success = true };
        }
    }
}
