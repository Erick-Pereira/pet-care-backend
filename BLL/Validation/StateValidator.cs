using Commons.Constants;
using Entities;
using FluentValidation;

namespace BLL.Validation
{
    public class StateValidator : AbstractValidator<State>
    {
        public StateValidator()
        {
            RuleFor(state => state.Name)
                .NotEmpty().WithMessage(ValidationMessages.StateNameEmpty)
                .MaximumLength(StateConstants.NameMaxLength)
                    .WithMessage(string.Format(ValidationMessages.StateNameMaxLength, StateConstants.NameMaxLength));

            RuleFor(state => state.Abreviation)
                .NotEmpty().WithMessage(ValidationMessages.StateAbreviationEmpty)
                .MaximumLength(StateConstants.AbreviationMaxLength)
                    .WithMessage(string.Format(ValidationMessages.StateAbrevivationLength, StateConstants.AbreviationMaxLength));
        }
    }
}