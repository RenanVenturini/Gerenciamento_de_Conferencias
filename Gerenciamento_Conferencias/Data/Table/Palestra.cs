using System;

namespace Gerenciamento_Conferencias.Data.Table
{
    public class Palestra
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Inicio { get; set; }
        public int Duracao { get; set; }
        public int TrilhaId { get; set; }
        public string Sessao { get; set; }
        public Trilha Trilha { get; set; }
    }
}
