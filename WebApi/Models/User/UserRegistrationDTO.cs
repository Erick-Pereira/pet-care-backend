using System.ComponentModel.DataAnnotations;
using web_api.Models.Address;

namespace web_api.Models.User
{
    public class UserRegistrationDTO
    {
        [Required(ErrorMessage = "")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "")]
        public string Email { get; set; }

        [Required(ErrorMessage = "")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "")]
        public string Password { get; set; }

        [Required(ErrorMessage = "")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "")]
        public AddressRegistrationDTO Address { get; set; }
    }
}