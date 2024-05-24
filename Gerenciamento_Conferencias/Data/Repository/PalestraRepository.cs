using Gerenciamento_Conferencias.Data.Repository.Interfaces;
using Gerenciamento_Conferencias.Data.Table;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_Conferencias.Data.Repository
{
    public class PalestraRepository : IPalestraRepository
    {
        private readonly GerenciamentoConferenciasContext _context;

        public PalestraRepository(GerenciamentoConferenciasContext context)
        {
            _context = context;
        }

        public async Task CriarPalestraAsync(Palestra palestra)
        {
            await _context.Palestras.AddAsync(palestra);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarPalestraAsync(Palestra palestra)
        {
            _context.Palestras.Update(palestra);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Palestra>> ListarPalestraAsync(int trilhaId)
            => await _context.Palestras
            .Where(t => t.TrilhaId == trilhaId)
            .AsNoTracking()
            .ToListAsync();

        public async Task<Palestra> ObterPalestraPorId(int id)
            => await _context.Palestras
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        public async Task ExcluirPalestra(Palestra palestra)
        {
            _context.Remove(palestra);
            await _context.SaveChangesAsync();
        }

        public async Task<string> ObterNetwork(int trilhaId)
        {
            return await _context.Trilhas
                .Where(x => x.Id == trilhaId)
                .Select(x => x.NetworkingEvent)
                .Select(x => x.Inicio)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
