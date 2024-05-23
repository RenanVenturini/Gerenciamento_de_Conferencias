using FluentValidation;
using Gerenciamento_Conferencias.Models.Request;
using System;
using System.Globalization;

namespace Gerenciamento_Conferencias.Validators
{
    public class NetworkingEventRequestValidator : AbstractValidator<NetworkingEventRequest>
    {
        public NetworkingEventRequestValidator()
        {
            RuleFor(x => x.Inicio)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O horário de início é obrigatório.")
                .Must(ValidarHorario).WithMessage("O horário de início deve estar no formato HH:mm.")
                .Must(ValidarInicio).WithMessage("O horário de início deve ser entre 16:00 e 17:00.");
        }

        private bool ValidarHorario(string time)
        {
            return TimeSpan.TryParseExact(time, "hh\\:mm", CultureInfo.InvariantCulture, out _);
        }

        private bool ValidarInicio(string time)
        {
            if (TimeSpan.TryParseExact(time, "hh\\:mm", CultureInfo.InvariantCulture, out var parsedTime))
            {
                var inicio = new TimeSpan(16, 0, 0);
                var fim = new TimeSpan(17, 0, 0);
                return parsedTime >= inicio && parsedTime <= fim;
            }
            return false;
        }
    }

}
