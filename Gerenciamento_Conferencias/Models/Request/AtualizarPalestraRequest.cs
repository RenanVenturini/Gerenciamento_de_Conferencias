namespace Gerenciamento_Conferencias.Models.Request
{
    public class AtualizarPalestraRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Inicio { get; set; }
        public string Duracao { get; set; }
        public int TrilhaId { get; set; }
    }
}
