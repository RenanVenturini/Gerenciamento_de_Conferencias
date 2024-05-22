using FluentValidation;
using Gerenciamento_Conferencias.Models.Request;

namespace Gerenciamento_Conferencias.Validators
{
    public class AtualizarPalestraRequestValidator : AbstractValidator<AtualizarPalestraRequest>
    {
        private readonly List<string> _horariosDisponiveis;

        public AtualizarPalestraRequestValidator(List<string> horariosDisponiveis)
        {
            _horariosDisponiveis = horariosDisponiveis;

            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("O Id da palestra é obrigatório.");

            RuleFor(p => p.Nome)
                .NotEmpty()
                .WithMessage("O nome da palestra é obrigatório.");

            RuleFor(p => p.Inicio)
                .NotEmpty()
                .WithMessage("O horário de início é obrigatório.")
                .Must(HorarioDisponivel)
                .WithMessage("O horário escolhido não está disponível.");

            RuleFor(p => p.Duracao)
                .GreaterThan(0)
                .WithMessage("A duração da palestra deve ser maior que zero.");
        }

        private bool HorarioDisponivel(AtualizarPalestraRequest novaPalestra, string inicio)
        {
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
    }

}
