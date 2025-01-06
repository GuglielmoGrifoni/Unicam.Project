using Unicam.Project.Application.Result;
using Unicam.Project.Models.Entities;

namespace Unicam.Project.Application.Abstractions.Services
{
    public interface ICategoriaService
    {
        Task <List<Categoria>> GetCategorieByNomiAsync(List<string> nomiCategorie, int idUtente);
        Task<bool> EsisteCategoriaAsync(string nome, int idUtente);
        Task<bool> PuòEliminareAsync(string nome, int idUtente);
        Task<EliminaCategoriaResult> EliminaCategoriaAsync(string nome, int idUtente);
        Task<CreaCategoriaResult> CreaCategoriaAsync(string nome, int idUtente);
        Task <List<Categoria>> GetAllCategorieAsync(int idUtente);
    }
}
