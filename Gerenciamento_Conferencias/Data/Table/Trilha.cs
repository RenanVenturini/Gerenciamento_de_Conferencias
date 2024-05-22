namespace Gerenciamento_Conferencias.Data.Table
{
    public class Trilha
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int ConferenciaId { get; set; }
        public List<Palestra> Palestras { get; set; }
        public NetworkingEvent NetworkingEvent { get; set; }
    }
}
