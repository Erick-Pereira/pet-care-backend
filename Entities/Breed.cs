namespace Entities
{
    public class Breed : Entity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Guid SpeciesId { get; set; }
        public required Specie Specie { get; set; }
    }
}