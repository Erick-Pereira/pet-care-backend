using Entities;
using FluentValidation;

namespace BLL.Validation
{
    public class MedicationValidator : AbstractValidator<Medication>
    {
        public MedicationValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("O nome do medicamento é obrigatório e deve ter no máximo 100 caracteres");

            RuleFor(m => m.Dosage)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("A dosagem é obrigatória e deve ter no máximo 50 caracteres");

            RuleFor(m => m.Frequency)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("A frequência é obrigatória e deve ter no máximo 50 caracteres");

            RuleFor(m => m.StartDate)
                .NotEmpty()
                .WithMessage("A data de início é obrigatória");

            RuleFor(m => m.EndDate)
                .Must((medication, endDate) => !endDate.HasValue || endDate.Value > medication.StartDate)
                .WithMessage("A data de término deve ser posterior à data de início");

            RuleFor(m => m.Notes)
                .MaximumLength(1000)
                .WithMessage("As notas devem ter no máximo 1000 caracteres");
        }
    }
}