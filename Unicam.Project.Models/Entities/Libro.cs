using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Project.Models.Entities
{
    public class Libro
    {
        public int IdLibro { get; set; } // Primary Key
        public int IdUtente { get; set; } // Foreign Key verso Utente
        public string Nome { get; set; } = string.Empty;
        public string Autore { get; set; } = string.Empty;
        public DateTime DataPubblicazione { get; set; }
        public string Editore { get; set; } = string.Empty;

        // Relazioni
        public ICollection<LibroCategoria>? LibriCategorie { get; set; }
        public Utente Utente { get; set; } = null!;
    }

}
