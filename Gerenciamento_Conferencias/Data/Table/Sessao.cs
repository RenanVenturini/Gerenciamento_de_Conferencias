using System;
using System.Collections.Generic;

namespace Gerenciamento_Conferencias.Data.Table
{
    public class Sessao
    {
        public int Id { get; set; }
        public DateTime HorarioInicio { get; set; }
        public List<Palestra> Palestras { get; set; }
        public int TrilhaId { get; set; }
        public Trilha Trilha { get; set; }
    }
}
