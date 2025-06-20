namespace Entities
{
    public class Address : Entity
    {
        public string Street { get; set; }
        public string? Complement { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }

        public Guid NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}