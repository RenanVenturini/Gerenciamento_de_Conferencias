namespace Gerenciamento_Conferencias.Models.Request
{
    public class AtualizarPalestraRequest
    {
        public string Nome { get; set; }
        public DateTime Inicio { get; set; }
        public int Duracao { get; set; }
        public int TrilhaId { get; set; }
    }
}
