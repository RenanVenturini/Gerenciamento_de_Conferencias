using System;
using System.Collections.Generic;

namespace Gerenciamento_Conferencias.Data.Table
{
    public class Conferencia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public string Local { get; set; }
        public List<Trilha> Trilhas { get; set; }
    }
}
