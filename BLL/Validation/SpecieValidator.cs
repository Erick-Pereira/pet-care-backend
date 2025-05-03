using Commons.Constants;
using Entities;
using FluentValidation;

namespace BLL.Validation
{
    public class SpecieValidator : AbstractValidator<Specie>
    {
        public SpecieValidator()
        {
            RuleFor(specie => specie.Name)
                .NotEmpty().WithMessage(ValidationMessages.SpecieNameEmpty)
                .MaximumLength(SpecieConstants.NameMaxLength)
                    .WithMessage(String.Format(ValidationMessages.SpecieNameMaxLength, SpecieConstants.NameMaxLength));

            RuleFor(specie => specie.Description)
                .MaximumLength(SpecieConstants.DescriptionMaxLength)
                    .WithMessage(String.Format(ValidationMessages.SpecieDescriptionMaxLength, SpecieConstants.DescriptionMaxLength));
        }
    }
}