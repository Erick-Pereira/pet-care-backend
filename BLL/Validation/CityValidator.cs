using Entities;
using FluentValidation;

namespace BLL.Validation
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(city => city.Name)
                .NotEmpty().WithMessage("City name cannot be empty.")
                .MaximumLength(60).WithMessage("City name cannot exceed 60 characters.");

            RuleFor(city => city.State)
                .NotNull().WithMessage("State cannot be null.");
        }
    }
}