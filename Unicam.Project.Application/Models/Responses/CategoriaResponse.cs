using Unicam.Project.Application.Models.Dtos;
using Unicam.Project.Models.Entities;

namespace Unicam.Project.Application.Models.Responses
{
    public class CategoriaResponse
    {
        public CategoriaDto Categoria { get; set; }

        public CategoriaResponse(Categoria categoria)
        {
            Categoria = new CategoriaDto(categoria);
        }
    }

}
