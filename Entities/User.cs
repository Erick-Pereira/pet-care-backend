namespace Entities
{
    public class User : Entity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string PermissionLevel { get; set; } = "User";
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
    }
}