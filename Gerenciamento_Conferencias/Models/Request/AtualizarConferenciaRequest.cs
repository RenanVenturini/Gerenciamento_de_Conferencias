using Gerenciamento_Conferencias.Data.Table;

namespace Gerenciamento_Conferencias.Models.Request
{
    public class AtualizarConferenciaRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Local { get; set; }
    }
}
