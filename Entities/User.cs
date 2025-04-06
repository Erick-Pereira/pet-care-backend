namespace Entities
{
    public class User : Entity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string BuildingNumber { get; set; }
        public string AddressComplement { get; set; }
        public string Neighborhood { get; set; }
        public string StateCode { get; set; }
    }
}