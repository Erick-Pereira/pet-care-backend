using Entities;

namespace web_api.Models
{
    public class PetRegistrationDTO
    {
        public string Name { get; set; }
        public Guid SpecieId { get; set; }
        public Guid BreedId { get; set; }
        public int Gender { get; set; }
        public DateOnly? ApproximateBirthDate { get; set; }
        public string? Color { get; set; }
        public string? Acquisition { get; set; }
        public bool IsCastrated { get; set; }
        public bool IsChipped { get; set; }
        public string? ChipNumber { get; set; }
        public Guid OwnerId { get; set; }
    }
}