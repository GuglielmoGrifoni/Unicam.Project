using Unicam.Project.Models.Entities;

namespace Unicam.Project.Application.Result
{
    public class CercaLibriResult : BaseResult
    {
        public List<Libro> Libri { get; set; } = new List<Libro>();
        public int TotalNum { get; set; }
    }
}
