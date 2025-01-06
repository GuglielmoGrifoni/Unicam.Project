using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Project.Models.Entities
{
    public class Utente
    {
        public int IdUtente { get; set; } // Primary Key
        public string Email { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Relazioni
        public ICollection<Categoria>? Categorie { get; set; }
        public virtual ICollection<Libro>? Libri { get; set; }
    }

}
