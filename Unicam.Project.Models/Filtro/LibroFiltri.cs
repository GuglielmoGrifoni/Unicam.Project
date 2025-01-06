using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Project.Models.Filtro
{
    public class LibroFiltri
    {
        public string? Nome { get; set; }
        public string? Autore { get; set; }
        public DateTime? DataPubblicazione { get; set; }
        public List<string>? CategorieNomi { get; set; }
    }
}
