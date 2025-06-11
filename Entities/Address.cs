namespace Entities
{
    public class Address : Entity
    {
        public required string Street { get; set; }
        public string? Complement { get; set; }
        public required string Number { get; set; }
        public required string ZipCode { get; set; }

        public Guid NeighborhoodId { get; set; }
        public required Neighborhood Neighborhood { get; set; }
    }
}