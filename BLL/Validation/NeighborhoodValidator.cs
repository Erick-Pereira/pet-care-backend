using Entities;
using FluentValidation;

namespace BLL.Validation
{
    internal class NeighborhoodValidator : AbstractValidator<Neighborhood>
    {
        public NeighborhoodValidator()
        {
            RuleFor(neighborhood => neighborhood.Name)
                .NotEmpty().WithMessage("Neighborhood name cannot be empty.")
                .MaximumLength(60).WithMessage("Neighborhood name cannot exceed 60 characters.");

            RuleFor(neighborhood => neighborhood.City)
                .NotNull().WithMessage("City name cannot be null.");
        }
    }
}