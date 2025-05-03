namespace Entities
{
    public class City : Entity
    {
        public string Name { get; set; }

        public Guid StateId { get; set; }
        public State State { get; set; }
    }
}