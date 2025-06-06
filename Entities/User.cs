namespace Entities
{
    public class User : Entity
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
        public required string CPF { get; set; }
        public string PermissionLevel { get; set; } = "User";
        public Guid AddressId { get; set; }
        public required Address Address { get; set; }
        public byte[]? ProfilePhoto { get; set; }
    }
}