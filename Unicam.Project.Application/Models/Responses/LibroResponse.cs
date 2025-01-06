using Unicam.Project.Models.Entities;

namespace Unicam.Project.Application.Models.Responses
{
    public class LibroResponse
    {
        public int IdLibro { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Autore { get; set; } = string.Empty;
        public string Editore { get; set; } = string.Empty;
        public DateTime DataPubblicazione { get; set; }
        public List<string> CategorieNomi { get; set; } = new List<string>();

        public LibroResponse(Libro libro)
        {
            IdLibro = libro.IdLibro;
            Nome = libro.Nome;
            Autore = libro.Autore;
            Editore = libro.Editore;
            DataPubblicazione = libro.DataPubblicazione;
            CategorieNomi = libro.LibriCategorie?.Select(lc => lc.Categoria.Nome.Trim()).ToList() ?? new List<string>();
        }
    }

}
