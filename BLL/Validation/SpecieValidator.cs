using Entities;
using FluentValidation;

namespace BLL.Validation
{
    public class SpecieValidator : AbstractValidator<Specie>
    {
        public SpecieValidator()
        {
            RuleFor(specie => specie.Name)
                .NotEmpty().WithMessage("Specie name cannot be empty.")
                .MaximumLength(60).WithMessage("Specie name cannot exceed 60 characters.");
        }
    }
}