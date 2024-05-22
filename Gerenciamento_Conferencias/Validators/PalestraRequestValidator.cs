using FluentValidation;
using Gerenciamento_Conferencias.Models.Request;
using System.Globalization;

namespace Gerenciamento_Conferencias.Validators
{
    public class PalestraRequestValidator : AbstractValidator<PalestraRequest>
    {
        private readonly List<string> _horariosDisponiveis;

        public PalestraRequestValidator(List<string> horariosDisponiveis)
        {
            _horariosDisponiveis = horariosDisponiveis;

            RuleFor(p => p.Nome)
                .NotEmpty()
                .WithMessage("O nome da palestra é obrigatório.");

            RuleFor(p => p.Inicio)
                .NotEmpty()
                .WithMessage("O horário de início é obrigatório.");

            RuleFor(p => p.Inicio)
                .Must(HorarioDisponivel)
                .WithMessage("O horário escolhido não está disponível ou a duração ultrapassa o tempo máximo para o próximo evento.");

            RuleFor(p => p.Duracao)
                .GreaterThan(0)
                .WithMessage("A duração da palestra deve ser maior que zero.");
        }

        private bool HorarioDisponivel(PalestraRequest novaPalestra, string inicio)
        {
            var formato = ValidarHorario(inicio);

            if (!formato)
                return formato;

            var inicioNovaPalestra = TimeSpan.Parse(inicio);
            var fimNovaPalestra = inicioNovaPalestra.Add(TimeSpan.FromMinutes(novaPalestra.Duracao));

            return _horariosDisponiveis.Any(intervalo =>
            {
                var partes = intervalo.Split(" as ");
                var inicioDisponivel = TimeSpan.Parse(partes[0]);
                var fimDisponivel = TimeSpan.Parse(partes[1]);

                return inicioNovaPalestra >= inicioDisponivel && fimNovaPalestra <= fimDisponivel;
            });
        }

        private bool ValidarHorario(string time)
        {

            var teste = TimeSpan.TryParseExact(time, "hh\\:mm", CultureInfo.InvariantCulture, out _);

            return teste;
        }
    }

}
