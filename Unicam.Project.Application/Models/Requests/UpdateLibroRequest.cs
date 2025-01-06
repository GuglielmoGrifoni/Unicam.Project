using System.ComponentModel.DataAnnotations;

namespace Unicam.Project.Application.Models.Requests
{
    public class UpdateLibroRequest
    {
        public string? Nome { get; set; }
        public string? Autore { get; set; }
        public string? Editore { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataPubblicazione { get; set; } = null;
        public List<string>? CategorieNomi { get; set; } = new List<string>();
    }

}
