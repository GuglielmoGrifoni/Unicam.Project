using Unicam.Project.Application.Models.Requests;
using Unicam.Project.Application.Result;
using Unicam.Project.Models.Entities;
using Unicam.Project.Models.Filtro;

namespace Unicam.Project.Application.Abstractions.Services
{
    public interface ILibroService
    {
        Task <CaricaLibroResult> CaricaLibroAsync(Libro libro, List<string> nomiCategorie);
        Task <ModificaLibroResult> ModificaLibroAsync(int id, UpdateLibroRequest request, int idUtente);
        Task <EliminaLibroResult> EliminaLibroAsync(int libroId, int idUtente);
        Task <CercaLibriResult> RicercaLibriAsync(LibroFiltri filtri, int idUtente, int pagina, int dimensionePagina);
    }
}
