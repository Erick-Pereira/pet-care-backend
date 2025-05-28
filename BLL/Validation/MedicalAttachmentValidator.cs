using Entities;
using FluentValidation;

namespace BLL.Validation
{
    public class MedicalAttachmentValidator : AbstractValidator<MedicalAttachment>
    {
        public MedicalAttachmentValidator()
        {
            RuleFor(m => m.FileName)
                .NotEmpty()
                .MaximumLength(255)
                .WithMessage("O nome do arquivo é obrigatório e deve ter no máximo 255 caracteres");

            RuleFor(m => m.ContentType)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("O tipo de conteúdo é obrigatório e deve ter no máximo 100 caracteres");

            RuleFor(m => m.FileData)
                .NotEmpty()
                .WithMessage("O arquivo é obrigatório");

            RuleFor(m => m.MedicalEventId)
                .NotEmpty()
                .WithMessage("O ID do evento médico é obrigatório");
        }
    }
}