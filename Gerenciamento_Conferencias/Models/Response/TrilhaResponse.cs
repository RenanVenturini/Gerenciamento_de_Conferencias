using System.Text.Json.Serialization;

namespace Gerenciamento_Conferencias.Models.Response
{
    public class TrilhaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<string> Palestras { get; set; }
        public List<string> HorariosDisponiveis { get; set; }
        public string InicioNetworkingEvent { get; set; }
    }
}
