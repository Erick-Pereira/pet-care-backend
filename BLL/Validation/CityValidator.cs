using Commons.Constants;
using Entities;
using FluentValidation;

namespace BLL.Validation
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(city => city.Name)
                .NotEmpty().WithMessage(ValidationMessages.CityNameEmpty)
                .MaximumLength(CityConstants.NameMaxLength)
                    .WithMessage(ValidationMessages.CityNameMaxLength);

            RuleFor(city => city.State)
                .NotNull().WithMessage(ValidationMessages.StateNull);
        }
    }
}