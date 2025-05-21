using Entities;
using FluentValidation;

namespace BLL.Validation
{
  public class DocumentValidator : AbstractValidator<Document>
  {
    public DocumentValidator()
    {
      RuleFor(d => d.Name)
          .NotEmpty()
          .MaximumLength(100)
          .WithMessage("O nome do documento é obrigatório e deve ter no máximo 100 caracteres");

      RuleFor(d => d.Description)
          .MaximumLength(500)
          .WithMessage("A descrição deve ter no máximo 500 caracteres");

      RuleFor(d => d.IssueDate)
          .NotEmpty()
          .LessThanOrEqualTo(DateTime.UtcNow)
          .WithMessage("A data de emissão não pode ser futura");

      RuleFor(d => d.ExpirationDate)
          .Must((document, expirationDate) =>
              !expirationDate.HasValue || expirationDate.Value > document.IssueDate)
          .WithMessage("A data de expiração deve ser posterior à data de emissão")
          .When(d => d.ExpirationDate.HasValue);

      RuleFor(d => d.DocumentNumber)
          .MaximumLength(50)
          .WithMessage("O número do documento deve ter no máximo 50 caracteres");

      RuleFor(d => d.IssuingAgency)
          .MaximumLength(100)
          .WithMessage("O nome do órgão emissor deve ter no máximo 100 caracteres");

      RuleFor(d => d.PetId)
          .NotEmpty()
          .WithMessage("O ID do pet é obrigatório");
    }
  }
}