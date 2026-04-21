using Api_SAAS_legumfrut.Dtos.cliente.Commands;
using FluentValidation;

namespace Api_SAAS_legumfrut.Dtos.cliente.validador
{
    public class ClienteUpdateValidator : AbstractValidator<ClienteUpdateCommand>
    {
        public ClienteUpdateValidator() {
            RuleFor(x => x.IdCliente)
                .GreaterThan(0).WithMessage("El id del cliente es obligatorio");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(50);

            RuleFor(x => x.Telefono)
                .MaximumLength(10)
                .When(x => !string.IsNullOrEmpty(x.Telefono))
                .WithMessage("El teléfono es obligatorio");

            RuleFor(x => x.CedulaRuc)
                .MaximumLength(13)
                .When(x => !string.IsNullOrEmpty(x.CedulaRuc))
                .WithMessage("La cédula/RUC es obligatoria");

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("el correo no es valido.");

            RuleFor(x => x.Tipo)
                .MaximumLength(4)
                .When(x => !string.IsNullOrEmpty(x.Tipo))
                .WithMessage("El tipo es valido");

            RuleFor(x => x.Estado)
                .NotNull()
                .WithMessage("El estado es obligatorio");

            RuleFor(x => x.LimiteCredito)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El límite de crédito debe ser mayor o igual a 0");

            RuleFor(x => x.DiasCredito)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Los días de crédito deben ser mayores o iguales a 0");

        }
    }
}
