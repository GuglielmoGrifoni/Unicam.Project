using Unicam.Project.Models.Entities;

namespace Unicam.Project.Application.Models.Dtos
{
    public class CategoriaDto
    {
        public int IdCategoria { get; set; }
        public string Nome { get; set; } = string.Empty;

        public CategoriaDto(Categoria categoria)
        {
            IdCategoria = categoria.IdCategoria;
            Nome = categoria.Nome.Trim();
        }
    }
}
