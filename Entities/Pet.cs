using Entities.Enums;

namespace Entities
{
    public class Pet : Entity
    {
        public required string Name { get; set; }
        public Guid SpecieId { get; set; }
        public required Specie Specie { get; set; }
        public Guid BreedId { get; set; }
        public required Breed Breed { get; set; }
        public required Gender Gender { get; set; }
        public DateOnly? ApproximateBirthDate { get; set; }
        public string? Color { get; set; }
        public string? Acquisition { get; set; }
        public bool IsCastrated { get; set; }
        public bool IsChipped { get; set; }
        public string? ChipNumber { get; set; }
        public Guid OwnerId { get; set; }
        public required User Owner { get; set; }
        public byte[]? ProfilePhoto { get; set; }
        public ICollection<PetPhoto>? Photos { get; set; } = new List<PetPhoto>();
        public ICollection<Document>? Documents { get; set; }
    }
}