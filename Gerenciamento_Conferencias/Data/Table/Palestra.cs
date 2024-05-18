using System;

namespace Gerenciamento_Conferencias.Data.Table
{
    public class Palestra
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Duracao { get; set; }
        public int SessaoId { get; set; }
        public Sessao Sessao { get; set; }
    }
}
