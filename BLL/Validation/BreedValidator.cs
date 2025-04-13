using Entities;
using FluentValidation;

namespace BLL.Validation
{
    internal class BreedValidator : AbstractValidator<Breed>
    {
        public BreedValidator()
        {
            RuleFor(breed => breed.Name)
                .NotEmpty().WithMessage("Breed name cannot be empty.")
                .MaximumLength(60).WithMessage("Breed name cannot exceed 60 characters.");
        }
    }
}