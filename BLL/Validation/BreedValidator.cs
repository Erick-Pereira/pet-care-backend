using Commons.Constants;
using Entities;
using FluentValidation;

namespace BLL.Validation
{
    public class BreedValidator : AbstractValidator<Breed>
    {
        public BreedValidator()
        {
            RuleFor(breed => breed.Name)
                .NotEmpty().WithMessage(ValidationMessages.BreedNameEmpty)
                .MaximumLength(BreedConstants.NameMaxLength)
                    .WithMessage(ValidationMessages.BreedNameMaxLength);

            RuleFor(breed => breed.Description)
                .MaximumLength(BreedConstants.DescriptionMaxLength)
                .WithMessage(String.Format(ValidationMessages.BreedDescriptionMaxLength, BreedConstants.DescriptionMaxLength));
        }
    }
}