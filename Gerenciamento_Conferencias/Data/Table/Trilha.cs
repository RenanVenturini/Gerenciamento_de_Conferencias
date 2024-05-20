using System.Collections.Generic;

namespace Gerenciamento_Conferencias.Data.Table
{
    public class Trilha
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<DateTime> HorariosDisponiveis { get; set; }
        public List<Palestra> Palestras { get; set; }
        public int ConferenciaId { get; set; }
    }
}
