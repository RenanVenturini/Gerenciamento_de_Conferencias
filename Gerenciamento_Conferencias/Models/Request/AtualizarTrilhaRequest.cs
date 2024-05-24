namespace Gerenciamento_Conferencias.Models.Request
{
    public class AtualizarTrilhaRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public AtualizarNetworkingEventRequest NetworkingEvent { get; set; }
    }
}
