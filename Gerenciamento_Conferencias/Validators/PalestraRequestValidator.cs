using FluentValidation;
using Gerenciamento_Conferencias.Models.Request;
using System.Globalization;
using System.Collections.Generic;
using System;

namespace Gerenciamento_Conferencias.Validators
{
    public class PalestraRequestValidator : AbstractValidator<PalestraRequest>
    {
        private readonly List<string> _horariosDisponiveis;

        public PalestraRequestValidator(List<string> horariosDisponiveis)
        {
            _horariosDisponiveis = horariosDisponiveis;

            RuleFor(p => p.Nome)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O nome da palestra é obrigatório.");

            RuleFor(p => p.Duracao)
                .Cascade(CascadeMode.Stop)
                .Must(ValidaDuracao)
                .WithMessage("A duração da palestra deve ser válida e maior que zero.");

            RuleFor(p => p.Inicio)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O horário de início é obrigatório.")
                .Must(ValidarHorario)
                .WithMessage("O horário de início deve estar no formato HH:mm.")
                .Must((palestra, inicio) => HorarioDisponivel(palestra, inicio))
                .WithMessage("O horário escolhido não está disponível ou a duração ultrapassa o tempo máximo para o próximo evento.");
        }

        private bool ValidaDuracao(PalestraRequest novaPalestra, string duracao)
        {
            if (duracao == "relâmpago")
            {
                novaPalestra.Duracao = "5";
                return true;
            }

            bool teste = int.TryParse(novaPalestra.Duracao, out int duracaoMinutos) && duracaoMinutos > 0;
            return teste;
        }

        private bool HorarioDisponivel(PalestraRequest novaPalestra, string inicio)
        {
            if (!TimeSpan.TryParse(inicio, out var inicioNovaPalestra))
            {
                return false;
            }

            if (!int.TryParse(novaPalestra.Duracao, out int duracaoMinutos))
            {
                return false;
            }

            var duracao = TimeSpan.FromMinutes(duracaoMinutos);
            var fimNovaPalestra = inicioNovaPalestra.Add(duracao);

            return _horariosDisponiveis.Any(intervalo =>
            {
                var partes = intervalo.Split(" as ");
                if (partes.Length != 2 ||
                    !TimeSpan.TryParse(partes[0], out var inicioDisponivel) ||
                    !TimeSpan.TryParse(partes[1], out var fimDisponivel))
                {
                    return false;
                }

                return inicioNovaPalestra >= inicioDisponivel && fimNovaPalestra <= fimDisponivel;
            });
        }

        private bool ValidarHorario(string time)
        {
            return TimeSpan.TryParseExact(time, @"hh\:mm", CultureInfo.InvariantCulture, out _);
        }
    }
}
