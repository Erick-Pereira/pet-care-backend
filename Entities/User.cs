namespace Entities
{
    public class User : Entity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
        public string PermissionLevel { get; set; } = "User";
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
        public byte[] ProfilePhoto { get; set; }
    }
}