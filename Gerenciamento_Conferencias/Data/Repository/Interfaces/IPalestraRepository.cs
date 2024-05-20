using Gerenciamento_Conferencias.Data.Table;

namespace Gerenciamento_Conferencias.Data.Repository.Interfaces
{
    public interface IPalestraRepository
    {
        Task CriarPalestraAsync(Palestra palestra);
        Task AtualizarPalestraAsync(Palestra palestra);
        Task<List<Palestra>> ListarPalestraAsync(int trilhaId);
        Task<Palestra> ObterPalestraPorId(int id);
        Task ExcluirPalestra(Palestra palestra);
    }
}
