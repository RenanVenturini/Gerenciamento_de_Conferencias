using FluentValidation;
using Gerenciamento_Conferencias.Models.Request;

namespace Gerenciamento_Conferencias.Validators
{

    public class ConferenciaRequestValidator : AbstractValidator<ConferenciaRequest>
    {
        public ConferenciaRequestValidator()
        {
            RuleFor(x => x.Nome)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O nome da conferência é obrigatório.");

            RuleFor(x => x.Local)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O local da conferência é obrigatório.");
        }
    }

}
