namespace Entities
{
    public class Pet : Entity
    {
        public string Name { get; set; } = string.Empty;
        public Guid SpecieId { get; set; }
        public Specie Specie { get; set; }
        public Guid BreedId { get; set; }
        public Breed Breed { get; set; }
        public string Gender { get; set; } = string.Empty;
        public DateTime? ApproximateBirthDate { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Acquisition { get; set; } = string.Empty;
        public bool IsCastrated { get; set; }

        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
    }
}