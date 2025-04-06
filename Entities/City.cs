namespace Entities
{
    public class City : Entity
    {
        public string Name { get; set; } = string.Empty;

        public Guid StateId { get; set; }
        public State State { get; set; }
    }
}