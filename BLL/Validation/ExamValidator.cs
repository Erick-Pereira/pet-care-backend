using Entities;
using FluentValidation;

namespace BLL.Validation
{
    public class ExamValidator : AbstractValidator<Exam>
    {
        public ExamValidator()
        {
            RuleFor(e => e.ExamType)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("O tipo do exame é obrigatório e deve ter no máximo 100 caracteres");

            RuleFor(e => e.Result)
                .MaximumLength(500)
                .WithMessage("O resultado deve ter no máximo 500 caracteres");

            RuleFor(e => e.ExamDate)
                .NotEmpty()
                .Must(date => date <= DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("A data do exame não pode ser futura");

            RuleFor(e => e.Notes)
                .MaximumLength(1000)
                .WithMessage("As notas devem ter no máximo 1000 caracteres");
        }
    }
}