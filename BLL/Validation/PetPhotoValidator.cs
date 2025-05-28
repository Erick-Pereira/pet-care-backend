using Entities;
using FluentValidation;

namespace BLL.Validation
{
    public class PetPhotoValidator : AbstractValidator<PetPhoto>
    {
        public PetPhotoValidator()
        {
            RuleFor(p => p.PhotoData)
                .NotEmpty()
                .WithMessage("A foto é obrigatória");

            RuleFor(p => p.Description)
                .MaximumLength(500)
                .WithMessage("A descrição deve ter no máximo 500 caracteres");

            RuleFor(p => p.Date)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("A data da foto não pode ser futura");

            RuleFor(p => p.PetId)
                .NotEmpty()
                .WithMessage("O ID do pet é obrigatório");
        }
    }
}