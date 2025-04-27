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
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(pet => pet.Specie)
                .NotNull().WithMessage("Species is required.");

            RuleFor(pet => pet.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .Must(gender => gender.Equals(Gender.Male) || gender.Equals(Gender.Female)).WithMessage("Gender must be either 'Male' or 'Female'.");

            RuleFor(pet => pet.ApproximateBirthDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Approximate birth date cannot be in the future.")
                .When(pet => pet.ApproximateBirthDate.HasValue);

            RuleFor(pet => pet.Color)
                .NotEmpty().WithMessage("Color is required.")
                .MaximumLength(60).WithMessage("Color must not exceed 60 characters.");

            RuleFor(pet => pet.Acquisition)
                .NotEmpty().WithMessage("Acquisition method is required.");

            RuleFor(pet => pet.IsCastrated)
                .NotNull().WithMessage("Castration status is required.");
        }
    }
}