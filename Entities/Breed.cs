namespace Entities
{
    public class Breed : Entity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid SpeciesId { get; set; }
        public Specie Specie { get; set; }
    }
}