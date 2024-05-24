using Gerenciamento_Conferencias.Data.Table;

namespace Gerenciamento_Conferencias.Data.Repository.Interfaces
{
    public interface IConferenciaRepository
    {
        Task CriarConferenciaAsync(Conferencia conferencia);
        Task AtualizarConferenciaAsync(Conferencia conferencia);
        Task<List<Conferencia>> ListarConferenciaAsync();
        Task<Conferencia> ObterConferenciaPorIdAsync(int id);
        Task ExcluirConferenciaAsync(Conferencia conferencia);
    }
}
