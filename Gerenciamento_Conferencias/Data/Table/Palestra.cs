﻿using System;

namespace Gerenciamento_Conferencias.Data.Table
{
    public class Palestra
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Inicio { get; set; }
        public int Duracao { get; set; }
        public int TrilhaId { get; set; }
    }
}
