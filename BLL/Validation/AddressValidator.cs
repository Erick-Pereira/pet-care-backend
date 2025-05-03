using Commons.Constants;
using Entities;
using FluentValidation;

namespace BLL.Validation
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address)
                .NotNull().WithMessage(ValidationMessages.AddressNotNull);

            RuleFor(address => address.Street)
                .NotEmpty().WithMessage(ValidationMessages.StreetEmpty)
                .MaximumLength(AddressConstants.StreetMaxLength)
                .WithMessage(String.Format(ValidationMessages.StreetMaxLength, AddressConstants.StreetMaxLength));

            RuleFor(address => address.ZipCode)
               .NotEmpty().WithMessage(ValidationMessages.ZipCodeEmpty)
               .Matches(AddressConstants.ZipCodeRegex).WithMessage(ValidationMessages.ZipCodeFormat);

            RuleFor(address => address.Complement)
               .MaximumLength(AddressConstants.ComplementMaxLength)
                .WithMessage(String.Format(ValidationMessages.ComplementMaxLength, AddressConstants.ComplementMaxLength));

            RuleFor(address => address.Number)
                .MaximumLength(AddressConstants.NumberMaxLength)
                .WithMessage(String.Format(ValidationMessages.NumberMaxLength, AddressConstants.NumberMaxLength));
            RuleFor(address => address.Neighborhood)
                .NotNull().WithMessage(ValidationMessages.NeighborhoodNotNull);
        }
    }
}