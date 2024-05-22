using FluentValidation;
using Gerenciamento_Conferencias.Models.Request;

namespace Gerenciamento_Conferencias.Validators
{

    public class AtualizarConferenciaRequestValidator : AbstractValidator<AtualizarConferenciaRequest>
    {
        public AtualizarConferenciaRequestValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("O Id da conferência é obrigatório.");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome da conferência é obrigatório.");

            RuleFor(x => x.Local)
                .NotEmpty().WithMessage("O local da conferência é obrigatório.");

            RuleFor(x => x.Trilhas)
                .NotEmpty().WithMessage("Pelo menos uma trilha é obrigatória.")
                .Must(trilhas => trilhas != null && trilhas.Count > 0).WithMessage("A conferência deve conter pelo menos uma trilha.")
                .ForEach(trilha => trilha.SetValidator(new AtualizarTrilhaRequestValidator()));
        }
    }

}
