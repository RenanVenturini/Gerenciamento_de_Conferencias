using FluentValidation;
using Gerenciamento_Conferencias.Models.Request;

namespace Gerenciamento_Conferencias.Validators
{

    public class AtualizarConferenciaRequestValidator : AbstractValidator<AtualizarConferenciaRequest>
    {
        public AtualizarConferenciaRequestValidator()
        {
            RuleFor(p => p.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O Id da conferência é obrigatório.");

            RuleFor(x => x.Nome)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O nome da conferência é obrigatório.");

            RuleFor(x => x.Local)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O local da conferência é obrigatório.");
        }
    }

}
