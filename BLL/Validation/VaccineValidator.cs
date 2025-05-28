using Entities;

using FluentValidation;

namespace BLL.Validation
{
    public class VaccineValidator : AbstractValidator<Vaccine>
    {
        public VaccineValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("O nome da vacina é obrigatório e deve ter no máximo 100 caracteres");

            RuleFor(v => v.Batch)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("O lote é obrigatório e deve ter no máximo 50 caracteres");

            RuleFor(v => v.ExpirationDate)
                .NotEmpty()
                .Must(date => date > DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("A data de validade deve ser futura");

            RuleFor(v => v.NextDose)
                .Must((vaccine, nextDose) => !nextDose.Equals(default(DateOnly)))
                .When(v => v.NextDose != default)
                .WithMessage("A data da próxima dose deve ser válida");
        }
    }
}