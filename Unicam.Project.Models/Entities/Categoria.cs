using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Project.Models.Entities
{
    public class Categoria
    {
        public int IdCategoria { get; set; } // Primary Key
        public int IdUtente { get; set; } // Foreign Key verso Utente
        public string Nome { get; set; } = string.Empty;

        // Relazioni
        public virtual ICollection<LibroCategoria>? LibroCategorie { get; set; }
        public Utente Utente { get; set; } = null!;
    }

}
