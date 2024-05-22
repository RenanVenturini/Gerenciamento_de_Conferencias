using Gerenciamento_Conferencias.Data.Table;

namespace Gerenciamento_Conferencias.Models.Request
{
    public class TrilhaRequest
    {
        public string Nome { get; set; }
        public NetworkingEventRequest NetworkingEvent { get; set; }
    }
}
