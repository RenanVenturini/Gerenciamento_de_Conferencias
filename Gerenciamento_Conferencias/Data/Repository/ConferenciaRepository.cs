using Gerenciamento_Conferencias.Data.Repository.Interfaces;
using Gerenciamento_Conferencias.Data.Table;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_Conferencias.Data.Repository
{
    public class ConferenciaRepository : IConferenciaRepository
    {
        private readonly GerenciamentoConferenciasContext _context;

        public ConferenciaRepository(GerenciamentoConferenciasContext context)
        {
            _context = context;
        }

        public async Task CriarConferenciaAsync( Conferencia conferencia)
        {
            await _context.Conferencias.AddAsync(conferencia);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarConferenciaAsync(Conferencia conferencia)
        {
            _context.Update(conferencia);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Conferencia>> ListarConferenciaAsync()
            => await _context.Conferencias
            .Include(t => t.Trilhas)
            .ThenInclude(p => p.Palestras)
            .ToListAsync();
        public async Task<Conferencia> ObterConferenciaPorIdAsync(int id)
            => await _context.Conferencias
            .Include(t => t.Trilhas)
            .ThenInclude(p => p.Palestras)
            .FirstOrDefaultAsync(c => c.Id == id);

        public async Task ExcluirConferenciaAsync(Conferencia conferencia)
        {
            _context.Remove(conferencia);
            await _context.SaveChangesAsync();
        }
    }
}
