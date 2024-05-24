using FluentValidation;
using Gerenciamento_Conferencias.Models.Request;

namespace Gerenciamento_Conferencias.Validators
{
    public class TrilhaRequestValidator : AbstractValidator<TrilhaRequest>
    {
        public TrilhaRequestValidator()
        {
            RuleFor(x => x.ConferenciaId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O Id da conferência é obrigatório.");

            RuleFor(x => x.Nome)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O nome da trilha é obrigatório.");

            RuleFor(x => x.NetworkingEvent)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("O evento de networking é obrigatório.")
                .SetValidator(new NetworkingEventRequestValidator());
        }
    }

}
