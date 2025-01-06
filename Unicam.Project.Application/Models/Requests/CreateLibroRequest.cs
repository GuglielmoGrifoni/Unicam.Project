using System.ComponentModel.DataAnnotations;
using Unicam.Project.Models.Entities;

namespace Unicam.Project.Application.Models.Requests
{
    public class CreateLibroRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Autore { get; set; } = string.Empty;
        public string Editore { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DataPubblicazione { get; set; }
        public List<string> CategorieNomi { get; set; } = new List<string>();

        public Libro ToEntity()
        {
            return new Libro
            {
                Nome = this.Nome,
                Autore = this.Autore,
                Editore = this.Editore,
                DataPubblicazione = this.DataPubblicazione
            };
        }
    }

}
