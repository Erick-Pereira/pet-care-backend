using Commons.Constants;
using Entities;
using Entities.Enums;
using FluentValidation;

namespace BLL.Validation
{
    public class PetValidator : AbstractValidator<Pet>
    {
        public PetValidator()
        {
            RuleFor(pet => pet.Name)
                .NotEmpty().WithMessage(ValidationMessages.PetNameRequired)
                .MinimumLength(PetConstants.NameMinLength).WithMessage(ValidationMessages.PetNameMinLength)
                .MaximumLength(PetConstants.NameMaxLength).WithMessage(ValidationMessages.PetNameMaxLength);

            RuleFor(pet => pet.Specie)
                .NotNull().WithMessage(ValidationMessages.SpecieRequired);

            RuleFor(pet => pet.Gender)
                .NotEmpty().WithMessage(ValidationMessages.GenderRequired)
                .Must(gender => gender.Equals(Gender.Male) || gender.Equals(Gender.Female))
                .WithMessage(ValidationMessages.GenderInvalid);

            RuleFor(pet => pet.ApproximateBirthDate)
                .Must(birthDate => !birthDate.HasValue || birthDate.Value <= DateOnly.FromDateTime(DateTime.Now))
                .WithMessage(ValidationMessages.BirthDateFuture)
                .When(pet => pet.ApproximateBirthDate.HasValue);

            RuleFor(pet => pet.Color)
                .NotEmpty().WithMessage(ValidationMessages.ColorRequired)
                .MaximumLength(PetConstants.ColorMaxLength).WithMessage(ValidationMessages.ColorMaxLength);

            RuleFor(pet => pet.Acquisition)
                .NotEmpty().WithMessage(ValidationMessages.AcquisitionRequired);

            RuleFor(pet => pet.IsCastrated)
                .NotNull().WithMessage(ValidationMessages.CastrationRequired);

            RuleFor(pet => pet.IsChipped)
                .NotNull().WithMessage(ValidationMessages.ChipRequired);

            RuleFor(pet => pet.ChipNumber)
                .MaximumLength(PetConstants.ChipNumberMaxLength).WithMessage(ValidationMessages.ChipNumberMaxLength);
        }
    }
}