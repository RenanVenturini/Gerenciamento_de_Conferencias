using Gerenciamento_Conferencias.Models.Request;
using Gerenciamento_Conferencias.Models.Response;

namespace Gerenciamento_Conferencias.Services.Interfaces
{
    public interface ITrilhaService
    {
        Task CriarTrilhaAsync(TrilhaRequest trilhaRequest);
        Task AtualizarTrilhaAsync(AtualizarTrilhaRequest atualizarTrilhaRequest);
        Task<IEnumerable<TrilhaResponse>> ListarTrilhaAsync();
        Task<TrilhaResponse> ObterTrilhaPorId(int id);
        Task ExcluirTrilhaAsync(int id);
    }
}
