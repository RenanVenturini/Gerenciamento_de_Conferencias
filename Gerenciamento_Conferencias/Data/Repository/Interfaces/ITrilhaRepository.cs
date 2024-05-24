using Gerenciamento_Conferencias.Data.Table;

namespace Gerenciamento_Conferencias.Data.Repository.Interfaces
{
    public interface ITrilhaRepository
    {
        Task CriarTrilhaAsync(Trilha trilha);
        Task AtualizarTrilhaAsync(Trilha trilha);
        Task<List<Trilha>> ListarTrilhaAsync();
        Task<Trilha> ObterTrilhaPorId(int id);
        Task ExcluirTrilhaAsync(Trilha trilha);
    }
}
