using Entities;
using FluentValidation;

namespace BLL.Validation
{
    public class DocumentAttachmentValidator : AbstractValidator<DocumentAttachment>
    {
        public DocumentAttachmentValidator()
        {
            RuleFor(d => d.DocumentId)
                .NotEmpty()
                .WithMessage("O ID do documento é obrigatório");

            RuleFor(d => d.FileName)
                .NotEmpty()
                .MaximumLength(255)
                .WithMessage("O nome do arquivo é obrigatório e deve ter no máximo 255 caracteres");

            RuleFor(d => d.ContentType)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("O tipo de conteúdo é obrigatório e deve ter no máximo 100 caracteres");

            RuleFor(d => d.FileData)
                .NotEmpty()
                .WithMessage("O arquivo é obrigatório");

            RuleFor(d => d.Date)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("A data não pode ser futura");
        }
    }
}