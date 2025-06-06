namespace Entities
{
    public class Neighborhood : Entity
    {
        public required string Name { get; set; }
        public required City City { get; set; }
        public Guid CityId { get; set; }
    }
}