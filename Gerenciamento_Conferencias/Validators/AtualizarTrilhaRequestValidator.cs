using FluentValidation;
using Gerenciamento_Conferencias.Models.Request;

namespace Gerenciamento_Conferencias.Validators
{
    public class AtualizarTrilhaRequestValidator : AbstractValidator<AtualizarTrilhaRequest>
    {
        public AtualizarTrilhaRequestValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("O Id da trilha é obrigatório.");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome da trilha é obrigatório.");

            RuleFor(x => x.NetworkingEvent)
                .NotNull().WithMessage("O evento de networking é obrigatório.")
                .SetValidator(new AtualizarNetworkingEventRequestValidator());
        }
    }

}
