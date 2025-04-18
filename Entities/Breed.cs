namespace Entities
{
    public class Breed : Entity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid SpeciesId { get; set; }
        public Specie Specie { get; set; }
    }
}