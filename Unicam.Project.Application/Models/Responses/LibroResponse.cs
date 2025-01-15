using Unicam.Project.Application.Models.Dtos;
using Unicam.Project.Models.Entities;

namespace Unicam.Project.Application.Models.Responses
{
    public class LibroResponse
    {
        public LibroDto Libro { get; set; }

        public LibroResponse(Libro libro)
        {
            Libro = new LibroDto(libro);
        }
    }

}
