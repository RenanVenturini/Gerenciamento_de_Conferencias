namespace Gerenciamento_Conferencias.Models.Response
{
    public class PalestraResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Inicio { get; set; }
        public int Duracao { get; set; }
        public int TrilhaId { get; set; }
        public string Sessao { get; set; }
    }
}
