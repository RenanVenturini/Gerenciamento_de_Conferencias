using System.Collections.Generic;

namespace Gerenciamento_Conferencias.Data.Table
{
    public class Trilha
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Sessao> SessoesMatutinas { get; set; }
        public List<Sessao> SessoesVespertinas { get; set; }
        public int ConferenciaId { get; set; }
        public Conferencia Conferencia { get; set; }
    }
}
