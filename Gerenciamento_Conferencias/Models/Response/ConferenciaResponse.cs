using Gerenciamento_Conferencias.Data.Table;

namespace Gerenciamento_Conferencias.Models.Response
{
    public class ConferenciaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Local { get; set; }
        public List<TrilhaResponse> Trilhas { get; set; }
    }
}
