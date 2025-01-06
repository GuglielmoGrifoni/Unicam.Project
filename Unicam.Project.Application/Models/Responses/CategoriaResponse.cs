using Unicam.Project.Models.Entities;

namespace Unicam.Project.Application.Models.Responses
{
    public class CategoriaResponse
    {
        public int IdCategoria { get; set; }
        public string Nome { get; set; } = string.Empty;

        public CategoriaResponse(Categoria categoria)
        {
            IdCategoria = categoria.IdCategoria;
            Nome = categoria.Nome.Trim();
        }
    }

}
