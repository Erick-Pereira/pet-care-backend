using System.ComponentModel.DataAnnotations;
using Entities.Enums;

namespace web_api.Models.Pet
{
    public class PetWithoutOwnerRegistrationDTO
    {
        [Required(ErrorMessage = "")]
        [Display(Name = "Pet Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select a species")]
        [Display(Name = "Species")]
        public Guid SpecieId { get; set; }

        [Required(ErrorMessage = "Please select a breed")]
        [Display(Name = "Breed")]
        public Guid BreedId { get; set; }

        [Required(ErrorMessage = "Please")]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Approximate Birth Date")]
        [DataType(DataType.Date)]
        [Range(typeof(DateOnly), "1900-01-01", "2100-12-31", ErrorMessage = "Please enter a valid date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly? ApproximateBirthDate { get; set; }

        [Display(Name = "Color")]
        public string? Color { get; set; }

        [Display(Name = "Acquisition")]
        public string? Acquisition { get; set; }

        [Display(Name = "Castrated")]
        [Required(ErrorMessage = "Please indicate if the pet is castrated")]
        public bool IsCastrated { get; set; }

        [Display(Name = "Chipped")]
        [Required(ErrorMessage = "Please indicate if the pet is chipped")]
        public bool IsChipped { get; set; }

        [Display(Name = "Chip Number")]
        public string? ChipNumber { get; set; }
    }
}