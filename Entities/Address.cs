namespace Entities
{
    public class Address : Entity
    {
        public string Street { get; set; } = string.Empty;
        public string Complement { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;

        public Guid NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}