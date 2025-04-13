using Entities;
using FluentValidation;

namespace BLL.Validation
{
    internal class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address)
                .NotNull().WithMessage("Address cannot be null.");

            RuleFor(address => address.ZipCode)
               .NotEmpty().WithMessage("Zip code cannot be empty.")
               .Matches("^\\d{9}$").WithMessage("Zip code must contain exactly 9 numeric characters.");

            RuleFor(address => address.Complement)
               .MaximumLength(60).WithMessage("Complement cannot exceed 60 characters.");

            RuleFor(address => address.Number)
                .MaximumLength(6).WithMessage("Number cannot exceed 6 characters.");

            RuleFor(address => address.Neighborhood)
                .NotNull().WithMessage("Neighborhood cannot be null.");
        }
    }
}