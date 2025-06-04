using Entities.Enums;

namespace Entities
{
    public class Pet : Entity
    {
        public string Name { get; set; }
        public Guid SpecieId { get; set; }
        public Specie Specie { get; set; }
        public Guid BreedId { get; set; }
        public Breed Breed { get; set; }
        public Gender Gender { get; set; }
        public DateOnly? ApproximateBirthDate { get; set; }
        public string Color { get; set; }
        public string? Acquisition { get; set; }
        public bool IsCastrated { get; set; }
        public bool IsChipped { get; set; }
        public string? ChipNumber { get; set; }
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
        public byte[]? ProfilePhoto { get; set; }
        public ICollection<PetPhoto>? Photos { get; set; } = new List<PetPhoto>();
        public ICollection<Document>? Documents { get; set; }
    }
}