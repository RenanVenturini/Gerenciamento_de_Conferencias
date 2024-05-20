using Gerenciamento_Conferencias.Models.Request;
using Gerenciamento_Conferencias.Models.Response;

namespace Gerenciamento_Conferencias.Services.Interfaces
{
    public interface IPalestraService
    {
        Task CriarPalestraAsync(PalestraRequest palestraRequest);
        Task AtualizarPalestraAsync(AtualizarPalestraRequest palestraRequest);
        Task<IEnumerable<PalestraResponse>> ListarPalestraAsync(int trilhaId);
        Task<PalestraResponse> ObterPalestraPorIdAsync(int id);
        Task ExcluirPalestraAsync(int id);
    }
}
