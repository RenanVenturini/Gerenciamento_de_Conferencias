using Gerenciamento_Conferencias.Data.Table;

namespace Gerenciamento_Conferencias.Models.Request
{
    public class ConferenciaRequest
    {
        public string Nome { get; set; }
        public string Local { get; set; }
        public List<Trilha> Trilhas { get; set; }
    }
}
