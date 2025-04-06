namespace Entities
{
    public class Neighborhood : Entity
    {
        public string Name { get; set; }
        public City City { get; set; }
        public Guid CityId { get; set; }
    }
}