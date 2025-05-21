using Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class DiagnosisValidator : AbstractValidator<Diagnosis>
    {
        public DiagnosisValidator()
        {
            RuleFor(d => d.Condition)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("A condição é obrigatória e deve ter no máximo 100 caracteres");

            RuleFor(d => d.Severity)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("A gravidade é obrigatória e deve ter no máximo 50 caracteres");

            RuleFor(d => d.recommendedTreatment)
                .MaximumLength(500)
                .WithMessage("O tratamento recomendado deve ter no máximo 500 caracteres");

            RuleFor(d => d.Notes)
                .MaximumLength(1000)
                .WithMessage("As notas devem ter no máximo 1000 caracteres");

            RuleFor(d => d.MedicalEventId)
                .NotEmpty()
                .WithMessage("O ID do evento médico é obrigatório");
        }
    }
}