using Commons.Constants;
using Commons.Validators;
using Entities;
using FluentValidation;

namespace BLL.Validation
{
    internal class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.FullName)
                .NotEmpty().WithMessage(ValidationMessages.FullNameRequired)
                .MinimumLength(UserConstants.FullNameMinLength).WithMessage(string.Format(ValidationMessages.FullNameMinLength, UserConstants.FullNameMinLength))
                .MaximumLength(UserConstants.FullNameMaxLength).WithMessage(string.Format(ValidationMessages.FullNameMaxLength, UserConstants.FullNameMaxLength));

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage(ValidationMessages.EmailRequired)
                .MinimumLength(UserConstants.EmailMinLength).WithMessage(string.Format(ValidationMessages.EmailMinLength, UserConstants.EmailMinLength))
                .MaximumLength(UserConstants.EmailMaxLength).WithMessage(string.Format(ValidationMessages.EmailMaxLength, UserConstants.EmailMaxLength))
                .Must(CommonValidators.IsValidEmail).WithMessage(ValidationMessages.EmailInvalid);

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage(ValidationMessages.PasswordNotNull)
                .MinimumLength(PasswordConstants.MinLength).WithMessage(string.Format(ValidationMessages.PasswordMinLength, PasswordConstants.MinLength))
                .MaximumLength(PasswordConstants.MaxLength).WithMessage(string.Format(ValidationMessages.PasswordMaxLength, PasswordConstants.MaxLength))
                .Matches("[A-Z]").WithMessage(ValidationMessages.PasswordMissingUppercase)
                .Matches("[a-z]").WithMessage(ValidationMessages.PasswordMissingLowercase)
                .Matches(@"\d").WithMessage(ValidationMessages.PasswordMissingDigit)
                .Matches(@"[\W_]").WithMessage(ValidationMessages.PasswordMissingSymbol);

            RuleFor(user => user.Address.ZipCode)
                .Must(CommonValidators.IsValidCep).WithMessage(ValidationMessages.CepInvalid);

            RuleFor(user => user.PhoneNumber)
                .Must(phone => CommonValidators.ValidatePhoneNumber(phone).Success == true)
                .WithMessage(ValidationMessages.PhoneNumberInvalid);

            RuleFor(user => user.CPF)
                .NotEmpty().WithMessage("CPF is required")
                .Length(11).WithMessage("CPF must be exactly 11 characters")
                .Matches("^[0-9]{11}$").WithMessage("CPF must contain only numbers");
        }
    }
}