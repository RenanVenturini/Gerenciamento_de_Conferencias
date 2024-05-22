namespace Gerenciamento_Conferencias.Models.Request
{
    public class AtualizarPalestraRequest
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Inicio { get; set; }
        public int Duracao { get; set; }
        public int TrilhaId { get; set; }
    }
}
