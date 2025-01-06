using Microsoft.EntityFrameworkCore;
using Unicam.Project.Models.Context;
using Unicam.Project.Models.Entities;

namespace Unicam.Project.Models.Repository
{
    public class UtenteRepository : GenericRepository<Utente>
    {
        public UtenteRepository(MyDbContext ctx) : base(ctx)
        {
        }

        public async Task<Utente?> GetUtenteByEmailAsync(string email)
        {
            return await _ctx.Utenti
                .FirstOrDefaultAsync(u => u.Email.ToLower().Trim() == email.ToLower().Trim());
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _ctx.Utenti.AnyAsync(u => u.Email.ToLower().Trim() == email.ToLower().Trim());
        }
    }

}
