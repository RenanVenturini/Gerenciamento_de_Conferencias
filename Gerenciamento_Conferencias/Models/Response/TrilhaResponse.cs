﻿namespace Gerenciamento_Conferencias.Models.Response
{
    public class TrilhaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<DateTime> HorariosDisponiveis { get; set; }
        public List<string> Palestras { get; set; }

    }
}
