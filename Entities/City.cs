namespace Entities
{
    public class City : Entity
    {
        public required string Name { get; set; }

        public Guid StateId { get; set; }
        public required State State { get; set; }
    }
}