using Entities;
using FluentValidation;

namespace BLL.Validation
{
    internal class StateValidator : AbstractValidator<State>
    {
        public StateValidator()
        {
            RuleFor(state => state.Name)
                .NotEmpty().WithMessage("State name cannot be empty.")
                .MaximumLength(60).WithMessage("State name cannot exceed 60 characters.");
        }
    }
}