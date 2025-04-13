namespace Entities
{
    public class Neighborhood : Entity
    {
        public string Name { get; set; } = string.Empty;
        public City City { get; set; }
        public Guid CityId { get; set; }
    }
}