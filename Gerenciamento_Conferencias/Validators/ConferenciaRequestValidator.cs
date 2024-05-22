using FluentValidation;
using Gerenciamento_Conferencias.Models.Request;

namespace Gerenciamento_Conferencias.Validators
{

    public class ConferenciaRequestValidator : AbstractValidator<ConferenciaRequest>
    {
        public ConferenciaRequestValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome da conferência é obrigatório.");

            RuleFor(x => x.Local)
                .NotEmpty().WithMessage("O local da conferência é obrigatório.");

            RuleFor(x => x.Trilhas)
                .NotEmpty().WithMessage("Pelo menos uma trilha é obrigatória.")
                .Must(trilhas => trilhas != null && trilhas.Count > 0).WithMessage("A conferência deve conter pelo menos uma trilha.")
                .ForEach(trilha => trilha.SetValidator(new TrilhaRequestValidator()));
        }
    }

}
