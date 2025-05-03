using Commons.Constants;
using Entities;
using FluentValidation;

namespace BLL.Validation
{
    public class NeighborhoodValidator : AbstractValidator<Neighborhood>
    {
        public NeighborhoodValidator()
        {
            RuleFor(neighborhood => neighborhood.Name)
                .NotEmpty().WithMessage(ValidationMessages.NeighborhoodNameEmpty)
                .MaximumLength(NeighborhoodConstants.NameMaxLength)
                    .WithMessage(ValidationMessages.NeighborhoodNameMaxLength);

            RuleFor(neighborhood => neighborhood.City)
                .NotNull().WithMessage(ValidationMessages.CityNull);
        }
    }
}