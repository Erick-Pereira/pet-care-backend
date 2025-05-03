using Commons.Constants;
using Commons.Validators;
using Entities;
using FluentValidation;

namespace BLL.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.FullName)
                .NotEmpty().WithMessage(ValidationMessages.FullNameRequired)
                .MinimumLength(UserConstants.FullNameMinLength)
                    .WithMessage(string.Format(ValidationMessages.FullNameMinLength, UserConstants.FullNameMinLength))
                .MaximumLength(UserConstants.FullNameMaxLength)
                    .WithMessage(string.Format(ValidationMessages.FullNameMaxLength, UserConstants.FullNameMaxLength));

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage(ValidationMessages.EmailRequired)
                .MinimumLength(UserConstants.EmailMinLength)
                    .WithMessage(string.Format(ValidationMessages.EmailMinLength, UserConstants.EmailMinLength))
                .MaximumLength(UserConstants.EmailMaxLength)
                    .WithMessage(string.Format(ValidationMessages.EmailMaxLength, UserConstants.EmailMaxLength))
                .Must(CommonValidators.IsValidEmail).WithMessage(ValidationMessages.EmailInvalid);

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage(ValidationMessages.PasswordNotNull)
                .MinimumLength(UserConstants.PasswordMinLength)
                    .WithMessage(string.Format(ValidationMessages.PasswordMinLength, UserConstants.PasswordMinLength))
                .MaximumLength(UserConstants.PasswordMaxLength)
                    .WithMessage(string.Format(ValidationMessages.PasswordMaxLength, UserConstants.PasswordMaxLength))
                .Matches(UserConstants.UppercaseRegex).WithMessage(ValidationMessages.PasswordMissingUppercase)
                .Matches(UserConstants.LowercaseRegex).WithMessage(ValidationMessages.PasswordMissingLowercase)
                .Matches(UserConstants.DigitRegex).WithMessage(ValidationMessages.PasswordMissingDigit)
                .Matches(UserConstants.SymbolRegex).WithMessage(ValidationMessages.PasswordMissingSymbol);

            RuleFor(user => user.Address.ZipCode)
                .Must(CommonValidators.IsValidCep)
                .WithMessage(ValidationMessages.CepInvalid);

            RuleFor(user => user.PhoneNumber)
                .Must(phone => CommonValidators.ValidatePhoneNumber(phone).Success == true)
                .WithMessage(ValidationMessages.PhoneNumberInvalid);

            RuleFor(user => user.CPF)
                .NotEmpty().WithMessage(ValidationMessages.CpfRequired)
                .Length(UserConstants.CpfLength).WithMessage(string.Format(ValidationMessages.CpfLength, UserConstants.CpfLength))
                .Matches(UserConstants.NumbersOnlyRegex).WithMessage(ValidationMessages.CpfOnlyNumbers)
                .Must(CommonValidators.IsValidCpf).WithMessage(ValidationMessages.CpfInvalid);
            ;
        }
    }
}