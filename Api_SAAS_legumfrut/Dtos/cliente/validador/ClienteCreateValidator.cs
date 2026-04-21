using Api_SAAS_legumfrut.Dtos.cliente.Commands;
using FluentValidation;

namespace Api_SAAS_legumfrut.Dtos.cliente.validador
{
    public class ClienteCreateValidator : AbstractValidator<ClienteCreateCommand>
    {
        public ClienteCreateValidator() { 
            /*
            RuleFor(x => x.IdEmpresa)
                .GreaterThan(0).WithMessage("el id de la empresa es obligatorio OJO");*/

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nomhbre es obligatorio")
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("el correo no es valido.");

            RuleFor(x => x.Telefono)
                .MaximumLength(10)
                .When(x => !string.IsNullOrEmpty(x.Telefono))
                .WithMessage("El teléfono es obligatorio");

            RuleFor(x => x.CedulaRuc)
                .Length(10, 13)
                .When(x => !string.IsNullOrEmpty(x.CedulaRuc))
                .WithMessage("La cédula o RUC debe tener 10 o 13 caracteres");

            RuleFor(x => x.Tipo)
                .MaximumLength(17)
                .When(x => !string.IsNullOrEmpty(x.Tipo))
                .WithMessage("El tipo es valido");
        }

        private bool BeValidEcuadorId(string? value) 
        {
            if (string.IsNullOrEmpty(value)) return true;
            // Implementación de la validación para el ID de Ecuador
            return value.Length == 10 || value.Length == 13;
        }
    }
}
