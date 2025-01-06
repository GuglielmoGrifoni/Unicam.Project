using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unicam.Project.Models.Context;
using Unicam.Project.Models.Entities;

namespace Unicam.Project.Models.Repository
{
    public class CategoriaRepository : GenericRepository<Categoria>
    {
        public CategoriaRepository(MyDbContext ctx) : base(ctx)
        {
        }

        public async Task<List<Categoria>> GetCategorieByNomiAsync(List<string> nomiCategorie, int idUtente)
        {
            var nomiCategoriePuliti = nomiCategorie
                .Select(n => n.Trim().ToLower())
                .ToList();

            return await _ctx.Categorie
                .Where(c =>
                    nomiCategoriePuliti.Contains(c.Nome.Trim().ToLower()) &&
                    c.IdUtente == idUtente)
                .ToListAsync();
        }

        public async Task<List<Categoria>> GetAllCategorieAsync(int idUtente)
        {
            return await _ctx.Categorie
                .Where(c => c.IdUtente == idUtente)
                .ToListAsync();
        }

        public async Task<bool> EsisteCategoriaAsync(string nome, int idUtente)
        {
            return await _ctx.Categorie.AnyAsync(c =>
                c.Nome.ToLower().Trim() == nome.ToLower().Trim() &&
                c.IdUtente == idUtente);
        }

        public async Task<bool> PuòEliminareAsync(string nome, int idUtente)
        {
            return !await _ctx.LibriCategorie
                .Include(lc => lc.Categoria)
                .AnyAsync(lc =>
                    lc.Categoria.Nome.ToLower().Trim() == nome.ToLower().Trim() &&
                    lc.Categoria.IdUtente == idUtente);
        }

        public async Task EliminaAsync(string nome, int idUtente)
        {
            var categoria = await _ctx.Categorie.FirstOrDefaultAsync(
                c => c.Nome.ToLower().Trim() == nome.ToLower().Trim() &&
                     c.IdUtente == idUtente);

            if (categoria != null)
            {
                _ctx.Categorie.Remove(categoria);
                _ctx.SaveChanges();
            }
        }
    }
}
