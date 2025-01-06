    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Project.Models.Entities;

namespace Unicam.Project.Models.Data
{
    public class CercaLibriData
    {
        public List<Libro> Libri {  get; set; } = new List<Libro>();
        public int TotalNum { get; set; }
    }
}
