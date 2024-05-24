using Gerenciamento_Conferencias.Models.Request;
using Gerenciamento_Conferencias.Models.Response;

namespace Gerenciamento_Conferencias.Services.Interfaces
{
    public interface IConferenciasService
    {
        Task CriarConferenciaAsync(ConferenciaRequest conferenciaRequest);
        Task AtualizarConferenciaAsync(AtualizarConferenciaRequest conferenciaRequest);
        Task<IEnumerable<ConferenciaResponse>> ListarConferenciaAsync();
        Task<ConferenciaResponse> ObterConferenciaPorIdAsync(int id);
        Task ExcluirConferenciaAsync(int id);
    }
}
