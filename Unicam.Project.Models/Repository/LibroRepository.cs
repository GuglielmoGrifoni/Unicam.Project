using Microsoft.EntityFrameworkCore;
using Unicam.Project.Models.Context;
using Unicam.Project.Models.Data;
using Unicam.Project.Models.Entities;

namespace Unicam.Project.Models.Repository
{
    public class LibroRepository : GenericRepository<Libro>
    {
        public LibroRepository(MyDbContext ctx) : base(ctx)
        {
        }

        public async Task<Libro?> GetLibroPerProprietaAsync(string nome, string autore, DateTime dataPubblicazione, int idUtente)
        {
            return await _ctx.Libri
                .FirstOrDefaultAsync(l =>
                    l.Nome.ToLower() == nome.ToLower().Trim() &&
                    l.Autore.ToLower() == autore.ToLower().Trim() &&
                    l.DataPubblicazione.Date == dataPubblicazione.Date &&
                    l.IdUtente == idUtente);
        }

        public async Task<CercaLibriData> CercaLibriAsync(string? nome, string? autore, DateTime? dataPubblicazione, List<int>? IdCategorie, int idUtente, int from, int pageSize)
        {
            var query = _ctx.Libri
                .Include(l => l.LibriCategorie)
                .ThenInclude(lc => lc.Categoria)
                .Where(l => l.IdUtente == idUtente)
                .AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(l => l.Nome.ToLower().Contains(nome.ToLower()));
            }

            if (!string.IsNullOrEmpty(autore))
            {
                query = query.Where(l => l.Autore.ToLower().Contains(autore.ToLower()));
            }

            if (dataPubblicazione.HasValue)
            {
                query = query.Where(l => l.DataPubblicazione.Date == dataPubblicazione.Value.Date);
            }

            if (IdCategorie != null && IdCategorie.Any())
            {
                query = query.Where(l => l.LibriCategorie.Any(lc => IdCategorie.Contains(lc.IdCategoria)));
            }

            var totalNum = await query.CountAsync();

            var libri = await query
                .OrderBy(l => l.Nome)
                .Skip(from)
                .Take(pageSize)
                .ToListAsync();

            return new CercaLibriData
            {
                Libri = libri,
                TotalNum = totalNum
            };
        }

        public async Task <Libro?> GetLibroConCategorieAsync(int id, int idUtente)
        {
            return await _ctx.Libri
                .Include(l => l.LibriCategorie)
                .ThenInclude(lc => lc.Categoria)
                .FirstOrDefaultAsync(l => l.IdLibro == id && l.IdUtente == idUtente);
        }

        public async Task AddCategoriaALibroAsync(int libroId, int categoriaId, int idUtente)
        {
            var libro = await _ctx.Libri
                .Include(l => l.LibriCategorie)
                .FirstOrDefaultAsync(l => l.IdLibro == libroId && l.IdUtente == idUtente);

            if (libro != null && !libro.LibriCategorie.Any(lc => lc.IdCategoria == categoriaId))
            {
                libro.LibriCategorie.Add(new LibroCategoria { IdLibro = libroId, IdCategoria = categoriaId });
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task RemoveCategoriaDaLibroAsync(int libroId, int categoriaId, int idUtente)
        {
            var libro = await _ctx.Libri.Include
                (l => l.LibriCategorie).
                FirstOrDefaultAsync(l => l.IdLibro == libroId && l.IdUtente == idUtente);

            if (libro != null)
            {
                var associazione = libro.LibriCategorie.FirstOrDefault(lc => lc.IdCategoria == categoriaId);
                if (associazione != null)
                {
                    libro.LibriCategorie.Remove(associazione);
                    await _ctx.SaveChangesAsync();
                }
            }
        }

        public async Task RemoveCategorieDaLibroAsync(int IdLibro, int idUtente)
        {
            var associazioni = await _ctx.LibriCategorie
                .Where(lc => lc.IdLibro == IdLibro && lc.Libro.IdUtente == idUtente)
                .ToListAsync();

            _ctx.LibriCategorie.RemoveRange(associazioni);
        }

        public async Task<List<Categoria>> GetCategorieByLibroAsync(int IdLibro, int idUtente)
        {
            return await _ctx.Libri
                .Where(l => l.IdLibro == IdLibro && l.IdUtente == idUtente)
                .SelectMany(l => l.LibriCategorie.Select(lc => lc.Categoria))
                .ToListAsync();
        }
    }
}
