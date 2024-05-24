using Gerenciamento_Conferencias.Data.Repository.Interfaces;
using Gerenciamento_Conferencias.Data.Table;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_Conferencias.Data.Repository
{
    public class TrilhaRepository : ITrilhaRepository
    {
        private readonly GerenciamentoConferenciasContext _context;

        public TrilhaRepository(GerenciamentoConferenciasContext context)
        {
            _context = context;
        }

        public async Task CriarTrilhaAsync(Trilha trilha)
        {
            await _context.Trilhas.AddAsync(trilha);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarTrilhaAsync(Trilha trilha)
        {
            _context.Trilhas.Update(trilha);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Trilha>> ListarTrilhaAsync()
            => await _context.Trilhas
            .Include(x => x.Palestras)
            .Include(p => p.NetworkingEvent)
            .ToListAsync();

        public async Task<Trilha> ObterTrilhaPorId(int id)
            => await _context.Trilhas.FirstOrDefaultAsync(p => p.Id == id);

        public async Task ExcluirTrilhaAsync(Trilha trilha)
        {
            _context.Remove(trilha);
            await _context.SaveChangesAsync();
        }
    }
}
