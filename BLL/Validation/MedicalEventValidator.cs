using Entities;
using FluentValidation;

namespace BLL.Validation
{
    public class MedicalEventValidator : AbstractValidator<MedicalEvent>
    {
        public MedicalEventValidator()
        {
            RuleFor(m => m.PetId)
                .NotEmpty()
                .WithMessage("O ID do pet é obrigatório");

            RuleFor(m => m.Type)
                .IsInEnum()
                .WithMessage("O tipo do evento médico é inválido");

            RuleFor(m => m.EventDate)
                .NotEmpty()
                .Must(date => date <= DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("A data do evento não pode ser futura");

            RuleFor(m => m.ProfessionalName)
                .MaximumLength(100)
                .WithMessage("O nome do profissional deve ter no máximo 100 caracteres");

            RuleFor(m => m.Description)
                .MaximumLength(500)
                .WithMessage("A descrição deve ter no máximo 500 caracteres");

            RuleFor(m => m.Notes)
                .MaximumLength(1000)
                .WithMessage("As notas devem ter no máximo 1000 caracteres");
        }
    }
}