namespace Gerenciamento_Conferencias.Models.Request
{
    public class PalestraRequest
    {
        public string Nome { get; set; }
        public string Inicio { get; set; }
        public int Duracao { get; set; }
        public int TrilhaId { get; set; }
    }
}
