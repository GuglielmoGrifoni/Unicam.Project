using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Project.Models.Entities
{
    public class LibroCategoria
    {
        public int IdLibro { get; set; } // Foreign Key verso Libro
        public Libro Libro { get; set; } = null!;

        public int IdCategoria { get; set; } // Foreign Key verso Categoria
        public  Categoria Categoria { get; set; } = null!;
    }

}
